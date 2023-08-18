using Registrar.Api.Services.Dto.Account;
using Registrar.Api.Services.Dto.Attendees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountCreate> CreateAccountProfile(AccountCreate attendee);
        Task<AccountDetails> GetAccountProfile(string attendeeId);
        Task<AccountUpdate> UpdateAccountProfile(AccountUpdate attendee);
        Task<string> ChangePassword(string userId, AccountPasswordUpdate passwordUpdate);
        Task<string> ChangeAdminPassword(string adminId, AccountPasswordUpdate passwordUpdate);
        Task<List<AccountList>> GetAccounts(AccountFilterModel filter);
        Task<string> DeleteAccount(string attendeeId);
    }
}
