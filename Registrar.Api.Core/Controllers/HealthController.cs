using Microsoft.AspNetCore.Mvc;
using Registrar.Api.Core.Data;

namespace Registrar.Api.Core.Controllers
{
    [ApiController]
    [Route("api/healthcheck")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var objResponse = new ApiObjResponse<string>(message: "Working optimally", data: "checking");
            return Ok(objResponse);
        }
    }
}