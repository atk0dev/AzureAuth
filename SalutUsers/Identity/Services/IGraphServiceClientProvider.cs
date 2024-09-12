using Identity.Models;
using Microsoft.Graph.Models;

namespace Identity.Services
{
    public interface IGraphServiceClientProvider
    {
        Task<UserCollectionResponse> GetAllUsersList();
        Task<AppRoleAssignmentCollectionResponse> GetUserByServicePrincipleId(string objectId);
        Task<User?> GetUser(string userId);
        Task<User?> CreateUser(string email, string name, string password);
        Task UpdateUser(string userId, string youtube, string linkedin);
        Task DeleteUser(string userId);
        Task<int?> GetCountOfGuestUsers();
        Task<UserCollectionResponse> SearchUser();
        Task<UserResponseDto> SendInvitationAsync(string email, string displayName);
    }
}