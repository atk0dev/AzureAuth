using Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using System.Net.Mail;

namespace Identity.Services
{
    public class GraphServiceClientProvider : IGraphServiceClientProvider
    {
        private readonly AzureConfiguration _azureConfig;
        private readonly string[] _scopes;
        
        public GraphServiceClientProvider(AzureConfiguration azureConfig)
        {
            _azureConfig = azureConfig;
            _scopes = new string[]
            {
                _azureConfig.GraphScope
            };
        }

        public GraphServiceClient GetGraphServiceClient()
        {
            var credential = new ClientSecretCredential(
                _azureConfig.TenantId,
                _azureConfig.ClientId,
                _azureConfig.ClientSecret
            );

            return new GraphServiceClient(credential, _scopes);
        }

        public async Task<UserCollectionResponse> GetAllUsersList()
        {
            var graphClient = GetGraphServiceClient();
            UserCollectionResponse result = await graphClient.Users.GetAsync();
            return result;
        }

        public async Task<AppRoleAssignmentCollectionResponse> GetUserByServicePrincipleId(string objectId)
        {
            var graphClient = GetGraphServiceClient();
            var result = await graphClient.ServicePrincipals["objectId"].AppRoleAssignedTo.GetAsync();
            return result;
        }

        public async Task<User?> GetUser(string userId)
        {
            var graphClient = GetGraphServiceClient();

            //var result = await graphClient.Users[$"{userId}"].GetAsync();

            var result = await graphClient.Users.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Count = true;
                requestConfiguration.QueryParameters.Filter = $"id eq '{userId}'";
                requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
            });

            if (result.Value != null && result.Value.Any())
            {
                return result.Value.First();
            }

            return null;
        }

        public async Task<User?> CreateUser(string email, string name, string password)
        {
            var graphClient = GetGraphServiceClient();

            User requestBody = new User
            {
                AccountEnabled = true,
                DisplayName = name,
                GivenName = name,
                PasswordPolicies = "DisablePasswordExpiration",
                PasswordProfile = new PasswordProfile
                {
                    Password = password,
                    ForceChangePasswordNextSignIn = false,
                },
                Identities = new List<ObjectIdentity>
                {
                    new ObjectIdentity { SignInType = "emailAddress", Issuer = _azureConfig.Issuer, IssuerAssignedId = email }
                }
            };

            var result = await graphClient.Users.PostAsync(requestBody);
            return result;
        }

        public async Task UpdateUser(string userId, string youtube, string linkedin)
        {
            var graphClient = GetGraphServiceClient();
            User requestBody = new User
            {
                Department = youtube,
                OfficeLocation = linkedin
            };
            await graphClient.Users[$"{userId}"].PatchAsync(requestBody);
        }

        public async Task DeleteUser(string userId)
        {
            var graphClient = GetGraphServiceClient();
            await graphClient.Users[$"{userId}"].DeleteAsync();
        }

        public async Task<int?> GetCountOfGuestUsers()
        {
            var graphClient = GetGraphServiceClient();

            int? result = await graphClient.Users.Count.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Filter = "userType eq 'guest'";
            });

            return result;
        }

        public async Task<UserCollectionResponse> SearchUser()
        {
            var graphClient = GetGraphServiceClient();
            var result = await graphClient.Users.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Count = true;
                requestConfiguration.QueryParameters.Search = "\"displayName:room\"";
                requestConfiguration.QueryParameters.Filter = "endsWith(mail,'microsoft.com')";
                requestConfiguration.QueryParameters.Orderby = new string[] { "displayName" };
                requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "mail" };
                requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
            });
            return result;
        }

        public async Task<UserResponseDto> SendInvitationAsync(string email, string displayName)
        {
            var graphClient = GetGraphServiceClient();

            UserCollectionResponse existingUsers = await graphClient.Users.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Count = true;
                requestConfiguration.QueryParameters.Filter = $"(userPrincipalName eq '{email}' or mail eq '{email}')";
                requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");
            });

            if (existingUsers.OdataCount > 1)
            {
                return new UserResponseDto { Success = false, Message = "User already exists.", UserInvitation = null };

            }

            var invitationRequestBody = new Invitation
            {
                InvitedUserDisplayName = displayName,
                InvitedUserEmailAddress = email,
                InviteRedirectUrl = _azureConfig.InvitationRedirectUrl,
                SendInvitationMessage = true,
                InvitedUserType = "Guest",
                Status = "PendingAcceptance"
            };

            try
            {
                var invitationResponse = await graphClient.Invitations.PostAsync(invitationRequestBody);
                return new UserResponseDto { Success = true, Message = "User Invitation sent successfully", UserInvitation = invitationResponse };
            }
            catch (Exception ex)
            {
                return new UserResponseDto
                {
                    Success = false,
                    Message = $"Failed to invite user: {ex.Message}",
                    UserInvitation = null
                };
            }
        }
    }
}
