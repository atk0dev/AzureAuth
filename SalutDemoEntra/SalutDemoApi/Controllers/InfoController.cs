using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SalutDemoApi.Controllers
{
    //[Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [HttpGet(Name = "About")]
        public IActionResult Get()
        {
            return Ok();
        }


    }
}
