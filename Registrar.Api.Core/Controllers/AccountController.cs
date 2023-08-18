using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registrar.Api.Core.Data;
using Registrar.Api.Data.Entities;
using Registrar.Api.Services.Dto;
using Registrar.Api.Services.Dto.Account;
using Registrar.Api.Services.Dto.Attendees;
using Registrar.Api.Services.Interfaces;

namespace Registrar.Api.Core.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private const string accountCreateSuccess = "Account profile created successfully";
        private const string accountUpdateSuccess = "Account profile updated successfully";
        private const string accountGetSuccess = "Account profile retrieved successfully";
        private const string accountsGetSuccess = "Account profile list retrieved successfully";
        private const string passwordUpdateSuccess = "Account password updated successfully";
        public AccountController(IAccountService accountService, ILogger logger) : base(logger)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> RegisterAccount([FromBody]AccountCreate account)
        {
            try
            {
                var accountDetail = await _accountService.CreateAccountProfile(account);
                var objId = new ObjectId { Id = accountDetail.Id };
                var response = new ApiObjResponse<ObjectId>(message: accountCreateSuccess, data: objId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpPut]
        [Route("profile")]
        public async Task<IActionResult> UpdateAccountProfile([FromBody] AccountUpdate account)
        {
            try
            {
                var loggedUserInfo = GetLoginUser();
                var accountDetail = await _accountService.UpdateAccountProfile(account);
                var objId = new ObjectId { Id = accountDetail.Id };
                var response = new ApiObjResponse<ObjectId>(message: accountUpdateSuccess, data: objId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendeeProfile([FromQuery]string? accountId = null)
        {
            try
            {
                var loggedUserInfo = GetLoginUser();
                string userId = "";
                var accountDetail = await _accountService.GetAccountProfile(userId);
                var response = new ApiObjResponse<AccountDetails>(message: accountGetSuccess, data: accountDetail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }


        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAttendees([FromQuery] AccountFilterModel filter)
        {
            try
            {
                var accountList = await _accountService.GetAccounts(filter);
                var response = new ApiObjResponse<List<AccountList>>(message: accountsGetSuccess, data: accountList);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpPost]
        [Route("{accountId}/archive")]
        public async Task<IActionResult> ArchiveAccountInfo(string accountId)
        {
            try
            {
                var accountDetail = await _accountService.DeleteAccount(accountId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpPost]
        [Route("{accountId}/change-password")]
        public async Task<IActionResult> ChangePassword(string accountId, [FromBody] AccountPasswordUpdate passwordUpdate)
        {
            try
            {
                var accountDetail = await _accountService.ChangePassword(accountId, passwordUpdate);
                var objId = new ObjectId { Id = accountId };
                var response = new ApiObjResponse<ObjectId>(message: passwordUpdateSuccess, data: objId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }

        [HttpPost]
        [Route("{accountId}/change-admin-password")]
        public async Task<IActionResult> ChangeAdminPassword(string accountId, [FromBody] AccountPasswordUpdate passwordUpdate)
        {
            try
            {
                var accountDetail = await _accountService.ChangeAdminPassword(accountId, passwordUpdate);
                var objId = new ObjectId { Id = accountId };
                var response = new ApiObjResponse<ObjectId>(message: passwordUpdateSuccess, data: objId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleExceptionError(ex);
            }
        }
    }
}
