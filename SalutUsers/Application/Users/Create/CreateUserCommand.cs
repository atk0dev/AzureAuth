using MediatR;

namespace Application.Users.Create;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    string Youtube,
    string Linkedin) : IRequest;
