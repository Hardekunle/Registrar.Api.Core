using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Registrar.Api.Core.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : BaseApiController
    {

        public RegistrationController(ILogger logger) : base(logger)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterForEvent()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpPost]
        [Route("space")]
        public async Task<IActionResult> AttachAttendeeToHostel()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }


    }
}
