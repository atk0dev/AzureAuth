using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace SalutDemoWeb.Pages;

public class DemoModel : PageModel
{
    private readonly ILogger<DemoModel> _logger;
    private ITokenAcquisition _tokenAcquisition;

    private string _apiPrefix = "https://atk0b2c.onmicrosoft.com/salut-demo-api";

    public DemoModel(ILogger<DemoModel> logger, ITokenAcquisition tokenAcquisition)
    {
        _logger = logger;
        _tokenAcquisition = tokenAcquisition;
    }

    public async Task OnGet()
    {
        var client = new HttpClient();

        var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { $"{_apiPrefix}/Data.Read" });         
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var data = await client.GetStringAsync("http://localhost:7362/api/demo?id=5");
        Console.WriteLine($"Data: {data}");
        ViewData["Data"] = data;

    }  
}
