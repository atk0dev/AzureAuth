using Domain.Users;
using MediatR;

namespace Application.Users.Update;

public record UpdateUserCommand(
    UserId UserId,
    string Name,
    string Email,
    string Youtube,
    string Linkedin) : IRequest;

public record UpdateUserRequest(
    string Name,
    string Email,
    string Youtube, 
    string Linkedin);
