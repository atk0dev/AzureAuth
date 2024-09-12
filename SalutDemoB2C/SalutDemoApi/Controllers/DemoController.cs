using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace SalutDemoApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(new[] {"Data.Read"})]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;

    public DemoController(ILogger<DemoController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public double Get(int id)
    {
        
        var user = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
        Console.WriteLine($"Logged in user: {user}");
        
        return 42;
    }
}
