using Application.Data;
using Domain.Users;
using Identity.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.All;

internal sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IGraphServiceClientProvider _graphService;

    public GetAllUsersQueryHandler(IApplicationDbContext context, IGraphServiceClientProvider graphService)
    {
        _context = context;
        _graphService = graphService;
    }

    public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var response = new List<UserResponse>();
        var graphUsersResponse = await _graphService.GetAllUsersList();
        var graphUsers = graphUsersResponse.Value;
        
        if (graphUsers != null)
        {
            var users = await _context
                .Users
                .ToListAsync(cancellationToken);

            foreach (var u in users)
            {
                response.Add(new UserResponse(
                    u.Id.Value.ToString(),
                    u.Name.Value,
                    u.Email.Value,
                    u.Youtube.Value,
                    u.Linkedin.Value,
                    graphUsers.FirstOrDefault(gu => gu.Id == u.Id.Value.ToString()) is not null));
            }
        }

        return response;
    }
}
