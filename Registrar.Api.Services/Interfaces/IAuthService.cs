using Registrar.Api.Services.Dto;
using Registrar.Api.Services.Dto.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<DecryptResetPasswordInfo> DecryptResetInfo(string encryptedInfo);
        Task<LoginToken> GetAdminAuthenticationToken(string adminId);
        Task<LoginToken> GetRegularAuhenticationToken(string regularId);
        Task ResetAdminPassword(string userId, string password);
        Task ResetRegularPassword(string userId, string password);
        Task SendAdminPasswordResetMail(string email, string resetLink);
        Task SendRegularPasswordResetMail(string email, string resetLink);
        Task<LoginToken> ValidateAdminUser(string userName, string password);
        Task<LoginToken> ValidateRegularUser(string userName, string password);
    }
}
