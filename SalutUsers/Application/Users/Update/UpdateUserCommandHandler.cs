using Application.Data;
using Domain.Users;
using Identity.Services;
using MediatR;

namespace Application.Users.Update;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGraphServiceClientProvider _graphService;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IGraphServiceClientProvider graphService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _graphService = graphService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        user.Update(
            Name.Create(request.Name)!,
            Email.Create(request.Email)!,
            Youtube.Create(request.Youtube)!,
            Linkedin.Create(request.Linkedin)!);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _graphService.UpdateUser(request.UserId.Value.ToString(), request.Youtube, request.Linkedin);
    }
}
