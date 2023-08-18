using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Registrar.Api.Core.Data;
using Registrar.Api.Data.Entities;
using Registrar.Api.Services.Dto;
using Registrar.Api.Services.Dto.Account;
using Registrar.Api.Services.Interfaces;
using System.Security.Principal;

namespace Registrar.Api.Core.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        private const string _loginSuccessMsg = "Login successful";
        private const string _refreshSuccessMsg = "Refreshed token successfully";
        private const string _forgotSuccessMsg = "Reset email has been forwarded to your email";
        private const string _resetSuccessMsg = "Password have been successfully reset";
        public AuthController(IAuthService authService, ILogger<AuthController> logger) : base(logger)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AccountLogin account)
        {

            if (account.UserType == UserType.Admin)
            {
                return await LoginAsAdmin(account.UserName, account.Password);
            }
            else
            {
                return await LoginAsRegularUser(account.UserName, account.Password);
            }

        }

        //[HttpPost]
        //[Route("refresh")]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshAccount refreshToken)
        //{

        //    RefreshTokenInfo tokenInfo = default;
        //    try
        //    {
        //        tokenInfo= await _authService.GetRefreshTokenInfo(refreshToken.Token);
        //    }
        //    catch(Exception ex)
        //    {
        //        return HandleExceptionError(ex);
        //    }
                
        //    if (tokenInfo.UserType == UserType.Admin)
        //    {
        //        return await RefreshAdminToken(tokenInfo.Id);
        //    }
        //    else
        //    {
        //        return await RefreshRegularToken(tokenInfo.Id);
        //    }

        //}

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPassword forgotPassword)
        {
            if (forgotPassword.UserType == UserType.Admin)
            {
                return await ForgotAdminPassword(forgotPassword.Email);
            }
            else
            {
                return await ForgotRegularPassword(forgotPassword.Email);
            }
        }


        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            DecryptResetPasswordInfo decryptedInfo = await _authService.DecryptResetInfo(resetPassword.EncryptedInfo);

            if (decryptedInfo.UserType == UserType.Admin)
            {
                return await ResetAdminPassword(decryptedInfo.UserId, resetPassword.Password);
            }
            else
            {
                return await ResetRegularPassword(decryptedInfo.UserId, resetPassword.Password);
            }
        }


        private async Task<IActionResult> ResetAdminPassword(string userId, string password)
        {
            try
            {
                await _authService.ResetAdminPassword(userId, password);
                return Ok(new ApiObjResponse<string>(message: _refreshSuccessMsg, data: null));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        private async Task<IActionResult> ResetRegularPassword(string userId, string password)
        {
            try
            {
                await _authService.ResetRegularPassword(userId, password);
                return Ok(new ApiObjResponse<string>(message: _refreshSuccessMsg, data: null));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        private async Task<IActionResult> ForgotAdminPassword(string email)
        {
            try
            {
                await _authService.SendAdminPasswordResetMail(email, "");
                return Ok(new ApiObjResponse<string>(message: _refreshSuccessMsg, data: null));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        private async Task<IActionResult> ForgotRegularPassword(string email)
        {
            try
            {
                await _authService.SendRegularPasswordResetMail(email, "");
                return Ok(new ApiObjResponse<string>(message: _refreshSuccessMsg, data: null));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }


        private async Task<IActionResult> RefreshAdminToken(string adminId)
        {
            try
            {
                LoginToken authtoken = await _authService.GetAdminAuthenticationToken(adminId);
                return Ok(new ApiObjResponse<LoginToken>(message: _refreshSuccessMsg, data: authtoken));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        private async Task<IActionResult> RefreshRegularToken(string regularId)
        {
            try
            {
                LoginToken authtoken = await _authService.GetRegularAuhenticationToken(regularId);
                return Ok(new ApiObjResponse<LoginToken>(message: _refreshSuccessMsg, data: authtoken));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        private async Task<IActionResult> LoginAsAdmin(string userName, string password)
        {
            try
            {
                LoginToken authtoken = await _authService.ValidateAdminUser(userName, password);
                return Ok(new ApiObjResponse<LoginToken>(message: _loginSuccessMsg, data: authtoken));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        private async Task<IActionResult> LoginAsRegularUser(string userName, string password)
        {
            try
            {
                LoginToken authtoken = await _authService.ValidateRegularUser(userName, password);
                return Ok(new ApiObjResponse<LoginToken>(message: _loginSuccessMsg, data:    authtoken));
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }
    }
}
