using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SalutDemoConn.Extensions;
using SalutDemoConn.Models;

namespace SalutDemoConn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectController : ControllerBase
    {
        private readonly ILogger<ConnectController> _logger;

        public ConnectController(ILogger<ConnectController> logger)
        {
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("Create");

            var rawRequestBody = await Request.GetRawBodyAsync();
            _logger.LogInformation("Create request: {rawRequestBody}", rawRequestBody);

            if (string.IsNullOrEmpty(rawRequestBody))
            {
                return BadRequest("Invalid request data provided");
            }

            var info = JsonSerializer.Deserialize<UserCreateInfo>(rawRequestBody);
            if (info == null)
            {
                return BadRequest("Invalid request data provided");
            }

            if (info.Age < 21)
            {
                return BadRequest();
            }

            return Ok();

        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth()
        {
            _logger.LogInformation("Auth");

            var rawRequestBody = await Request.GetRawBodyAsync();
            _logger.LogInformation("Auth request: {rawRequestBody}", rawRequestBody);

            if (string.IsNullOrEmpty(rawRequestBody))
            {
                return BadRequest("Invalid request data provided");
            }

            var info = JsonSerializer.Deserialize<UserAuthInfo>(rawRequestBody);
            if (info == null)
            {
                return BadRequest("Invalid request data provided");
            }

            var result = new
            {
                action = "Continue",
                extension_2f81fe04720f466bb320e0a080bf6670_ExtraData = "Some data from external system"
            };

            return Ok(result);

        }
    }
}
