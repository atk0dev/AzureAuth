using Domain.Users;
using MediatR;

namespace Application.Users.Delete;

public record DeleteUserCommand(UserId UserId) : IRequest;
