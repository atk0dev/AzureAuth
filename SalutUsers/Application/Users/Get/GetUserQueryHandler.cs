using Application.Data;
using Domain.Users;
using Identity.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Get;

internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IGraphServiceClientProvider _graphService;

    public GetUserQueryHandler(IApplicationDbContext context, IGraphServiceClientProvider graphService)
    {
        _context = context;
        _graphService = graphService;
    }

    public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var graphUser = await _graphService.GetUser(request.UserId.Value.ToString());
        
        var user = await _context
            .Users
            .Where(u => u.Id == request.UserId)
            .Select(u => new UserResponse(
                u.Id.Value.ToString(),
                u.Name.Value,
                u.Email.Value,
                u.Youtube.Value,
                u.Linkedin.Value,
                graphUser == null))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return user;
    }
}
