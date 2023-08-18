using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Registrar.Api.Data.Entities;
using Registrar.Api.Data.Repository;
using Registrar.Api.Services.Dto.Account;
using Registrar.Api.Services.Dto.Attendees;
using Registrar.Api.Services.Interfaces;
using Registrar.Api.Services.MapProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Logic
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<IdentityUser> _userHelper;
        private const string accountNotFound = "Unable to retrieve account details";
        private const string invalidPassword = "Password provided is invalid";
        private const string accountAlreadyExist = "Account already exist";
        public AccountService(IUnitOfWork unit,UserManager<IdentityUser> userHelper)
        {
            _unit = unit;
            _userHelper = userHelper;
        }
        public async Task<string> ChangeAdminPassword(string userId, AccountPasswordUpdate passwordUpdate)
        {
            AdminAccount account = await _unit.AdminAccounts.GetById(userId);
            if(account == null)
            {
                throw new BadRequestException(accountNotFound);
            }
            IdentityUser identityUser = AuthService.ConvertAppUserToIdentity(account.SecurityKey);

            var hashedOldPassword = _userHelper.PasswordHasher.HashPassword(identityUser, passwordUpdate.OldPassword);

            if(hashedOldPassword != account.PasswordHash)
            {
                throw new BadRequestException(invalidPassword);
            }

            var hashedNewPassword = _userHelper.PasswordHasher.HashPassword(identityUser, passwordUpdate.NewPassword);

            account.PasswordHash = hashedNewPassword;

            _unit.SaveToDatabase();

            return userId;

        }

        public async Task<string> ChangePassword(string userId, AccountPasswordUpdate passwordUpdate)
        {
            RegularAccount account = await _unit.RegularAccounts.GetById(userId);
            if (account == null)
            {
                throw new BadRequestException(accountNotFound);
            }
            IdentityUser identityUser = AuthService.ConvertAppUserToIdentity(account.SecurityKey);

            var hashedOldPassword = _userHelper.PasswordHasher.HashPassword(identityUser, passwordUpdate.OldPassword);

            if (hashedOldPassword != account.PasswordHash)
            {
                throw new BadRequestException(invalidPassword);
            }

            var hashedNewPassword = _userHelper.PasswordHasher.HashPassword(identityUser, passwordUpdate.NewPassword);

            account.PasswordHash = hashedNewPassword;
            account.IsPasswordUpdated = true;
            account.DateOfPasswordUpdate = DateTime.Now;

            _unit.SaveToDatabase();

            return userId;
        }

        public async Task<AccountCreate> CreateAccountProfile(AccountCreate attendee)
        {
            RegularAccount account = await _unit.RegularAccounts.GetByEmail(attendee.Email);
            if(account != null)
            {
                throw new BadRequestException(accountAlreadyExist);
            }
            RegularAccount newAccount = AccountMappings.Map(attendee, new RegularAccount());
            await _unit.RegularAccounts.AddAsync(newAccount);
            _unit.SaveToDatabase();

            attendee.Id = newAccount.Id.ToString();
            return attendee;
        }

        public async Task<string> DeleteAccount(string attendeeId)
        {
            RegularAccount account = await _unit.RegularAccounts.GetById(attendeeId);
            if (account == null)
            {
                throw new BadRequestException(accountNotFound);
            }

            account.IsDeprecated = true;
            _unit.SaveToDatabase();

            return attendeeId;
        }

        public async Task<AccountDetails> GetAccountProfile(string attendeeId)
        {
            RegularAccount account = await _unit.RegularAccounts.GetById(attendeeId);
            if (account == null)
            {
                throw new BadRequestException(accountNotFound);
            }

            AccountDetails accountDetails = AccountMappings.Map(account, new AccountDetails());
            return accountDetails;
        }

        public async Task<List<AccountList>> GetAccounts(AccountFilterModel filter)
        {
            List<RegularAccount> accounts = await _unit.RegularAccounts.GetAll(filter.ActiveOnly);
            var accountList = new List<AccountList>();
            foreach(var account in accounts)
            {
                accountList.Add(AccountMappings.Map(account, new AccountList()));
            }
            return accountList;
        }

        public async Task<AccountUpdate> UpdateAccountProfile(AccountUpdate update)
        {
            RegularAccount account = await _unit.RegularAccounts.GetById(update.Id);
            if (account == null)
            {
                throw new BadRequestException(accountNotFound);
            }

            RegularAccount accountDetails = AccountMappings.Map(update, account);
            _unit.SaveToDatabase();
            return update;
        }
    }
}
