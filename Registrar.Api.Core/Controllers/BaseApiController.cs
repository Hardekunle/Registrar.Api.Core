using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Registrar.Api.Core.Data;

namespace Registrar.Api.Core.Controllers
{

    public abstract class BaseApiController : ControllerBase
    {
        private readonly ILogger _logger;
        public BaseApiController(ILogger logger)
        {
            _logger= logger;
        }
        protected IActionResult HandleExceptionError(Exception ex)
        {
            var errors= new List<string>() { ex.Message };
            return BadRequest(new ApiErrorResponse(errors));
        }

        protected LoggedInUserInfo GetLoginUser()
        {
            return default;
        }
    }
}
