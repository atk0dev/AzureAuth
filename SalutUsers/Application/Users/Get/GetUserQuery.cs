using Domain.Users;
using MediatR;

namespace Application.Users.Get;

public record GetUserQuery(UserId UserId) : IRequest<UserResponse>;

public record UserResponse(
    string Id,
    string Name,
    string Email,
    string Youtube, 
    string Linkedin,
    bool Active);
