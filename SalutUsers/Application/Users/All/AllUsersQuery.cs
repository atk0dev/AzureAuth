using Domain.Users;
using MediatR;

namespace Application.Users.All;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserResponse>>;

public record UserResponse(
    string Id,
    string Name,
    string Email,
    string Youtube,
    string Linkedin,
    bool Active);
