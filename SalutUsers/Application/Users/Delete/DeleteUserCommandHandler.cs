using Application.Data;
using Domain.Users;
using Identity.Services;
using MediatR;

namespace Application.Users.Delete;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGraphServiceClientProvider _graphService;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IGraphServiceClientProvider graphService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _graphService = graphService;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _graphService.DeleteUser(request.UserId.Value.ToString());

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        _userRepository.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
