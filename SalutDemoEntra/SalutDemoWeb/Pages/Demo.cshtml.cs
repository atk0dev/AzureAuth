using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace SalutDemoWeb.Pages;

public class DemoModel : PageModel
{
    private readonly ILogger<DemoModel> _logger;
    private ITokenAcquisition _tokenAcquisition;

    private string _apiPrefix = "api://salut-demo-api";

    public DemoModel(ILogger<DemoModel> logger, ITokenAcquisition tokenAcquisition)
    {
        _logger = logger;
        _tokenAcquisition = tokenAcquisition;
    }

    public void OnGet()
    {
        var client = new HttpClient();

        var accessToken = _tokenAcquisition.GetAccessTokenForUserAsync(new[] { $"{_apiPrefix}/Data.Read" }).Result;         
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var data = client.GetStringAsync("http://localhost:7362/api/demo?id=5").Result;
        Console.WriteLine($"Data: {data}");
        ViewData["Data"] = data;

    }  
}
