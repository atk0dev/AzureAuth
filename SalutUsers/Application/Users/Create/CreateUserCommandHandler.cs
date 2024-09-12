using Application.Data;
using Domain.Users;
using Identity.Services;
using MediatR;

namespace Application.Users.Create;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IGraphServiceClientProvider _graphService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IGraphServiceClientProvider graphService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _graphService = graphService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var graphUser = await _graphService.CreateUser(request.Email, request.Name, request.Password);

        if (graphUser != null && !string.IsNullOrEmpty(graphUser.Id))
        {
            var user = new User(
                new UserId(Guid.Parse(graphUser.Id!)),
                Name.Create(request.Name)!,
                Email.Create(request.Email)!,
                Youtube.Create(request.Youtube)!,
                Linkedin.Create(request.Linkedin)!);

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
