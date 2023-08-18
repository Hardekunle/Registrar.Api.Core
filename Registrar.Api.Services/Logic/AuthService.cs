using crypto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Registrar.Api.Data.Entities;
using Registrar.Api.Data.Repository;
using Registrar.Api.Services.Dto;
using Registrar.Api.Services.Dto.Account;
using Registrar.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Logic
{
    public class AuthService : IAuthService
    {
        private string _forgotPasswordEncrytionKey;
        private const string wrongEncryption = "Invaid security details";
        private const string accountNotFound = "Unable to retrieve account details";
        private const int accessValidityPeriod = 100;
        private readonly UserManager<IdentityUser> _userHelper;
        private readonly string _passwordTokenProvider;
        private const string _passwordTokenPurpose = "PasswordToken";
        private readonly IEmailService _emailService;
        private readonly IEmailMessages _emailMessages;

        private readonly IUnitOfWork _unit;
        public AuthService(IUnitOfWork unit, UserManager<IdentityUser> iUserHelper)
        {
            _unit = unit;
            _userHelper = iUserHelper;
            _passwordTokenProvider = iUserHelper.Options.Tokens.PasswordResetTokenProvider;
        }
        public async Task<DecryptResetPasswordInfo> DecryptResetInfo(string encryptedInfo)
        {
            var decryptedString = Security.Decrypt(encryptedInfo, _forgotPasswordEncrytionKey,"_");

            var splitDecryptInfo = decryptedString.Split("~");

            if(splitDecryptInfo.Length != 0 ) 
            {
                throw new BadRequestException(wrongEncryption);
            }

            var decryptedInfo = new DecryptResetPasswordInfo
            {
                UserId = splitDecryptInfo[0],
                UserType = (UserType)Int32.Parse(splitDecryptInfo[1])
            };

            return decryptedInfo;
        }

        public async Task<LoginToken> GetAdminAuthenticationToken(string adminId)
        {
            AdminAccount adminInfo = await _unit.AdminAccounts.GetById(adminId);
            if (adminInfo == null)
            {
                throw new BadRequestException(accountNotFound);
            }

            return GenerateAdminToken(adminInfo);
        }

        private LoginToken GenerateAdminToken(AdminAccount adminInfo)
        {
            var adminClaims = new List<Claim>();
            adminClaims.Add(new Claim("Id", adminInfo.Id.ToString()));
            adminClaims.Add(new Claim(ClaimTypes.Role, AppSecurity.AdminRole));

            JwtSecurityToken token = GenerateAuthenticationToken(adminClaims, accessValidityPeriod);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var tokenResponse = new LoginToken
            {
                ExpireAt = DateTime.UtcNow,
                AccessToken = tokenString
            };

            return tokenResponse;
        }

        public async Task<LoginToken> GetRegularAuhenticationToken(string regularId)
        {
            RegularAccount regularInfo = await _unit.RegularAccounts.GetById(regularId);
            if (regularInfo == null)
            {
                throw new BadRequestException(accountNotFound);
            }

            return GenerateRegularToken(regularInfo);
        }

        private LoginToken GenerateRegularToken(RegularAccount regularInfo)
        {
            var adminClaims = new List<Claim>();
            adminClaims.Add(new Claim("Id", regularInfo.Id.ToString()));
            adminClaims.Add(new Claim(ClaimTypes.Role, AppSecurity.RegularRole));

            JwtSecurityToken token = GenerateAuthenticationToken(adminClaims, accessValidityPeriod);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var tokenResponse = new LoginToken
            {
                ExpireAt = DateTime.UtcNow,
                AccessToken = tokenString
            };

            return tokenResponse;
        }

        private JwtSecurityToken GenerateAuthenticationToken(List<Claim> userClaims, int validityPeriod)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret"));
            var token = new JwtSecurityToken(
            issuer: "issuer",
            audience: "audience",
            expires: DateTime.Now.AddHours(validityPeriod),
            claims: userClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }



        public async Task ResetAdminPassword(string userId, string password)
        {
            AdminAccount adminInfo = await _unit.AdminAccounts.GetById(userId);
            if (adminInfo == null)
            {
                throw new BadRequestException(accountNotFound);
            }

            IdentityUser identityUser = ConvertAppUserToIdentity(adminInfo.SecurityKey);

            adminInfo.PasswordHash = _userHelper.PasswordHasher.HashPassword(identityUser, password);
            _unit.SaveToDatabase();
        }

        public async Task ResetRegularPassword(string userId, string password)
        {
            RegularAccount regularInfo = await _unit.RegularAccounts.GetById(userId);
            if (regularInfo == null)
            {
                throw new BadRequestException(accountNotFound);
            }

            IdentityUser identityUser = ConvertAppUserToIdentity(regularInfo.SecurityKey);


            regularInfo.PasswordHash = _userHelper.PasswordHasher.HashPassword(identityUser, password);
            regularInfo.IsPasswordUpdated = true;
            regularInfo.DateOfPasswordUpdate = DateTime.Now;
            _unit.SaveToDatabase();
        }

        public async Task SendAdminPasswordResetMail(string email, string resetLink)
        {
            AdminAccount account = _unit.AdminAccounts.GetByEmail(email);
            if (account == null)
            {
                return;
            }

            var identityUser = ConvertAppUserToIdentity(account.SecurityKey);

            var generatedToken = await _userHelper.GenerateUserTokenAsync(identityUser, _passwordTokenProvider, _passwordTokenPurpose);

            //encode the token to be sent
            var encodedUrl = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(generatedToken));
            var concatedString = account.Id + "~" + UserType.Admin;
            var encryptedInfo = Security.Encrypt(concatedString, _forgotPasswordEncrytionKey, "_");
            var resetPasswordUrl = resetLink + "?Token=" + encodedUrl + "&Email=" + encryptedInfo;

            var mailContent = _emailMessages.ForgotPassword(resetPasswordUrl);
            await _emailService.SendEmail(email, "Password Reset", mailContent);
        }

        public async Task SendRegularPasswordResetMail(string email, string resetLink)
        {
            RegularAccount account = await _unit.RegularAccounts.GetByEmail(email);
            if (account == null)
            {
                return;
            }

            var identityUser = new IdentityUser();
            identityUser.Id = account.SecurityKey;
            identityUser.Email = account.SecurityKey;
            identityUser.UserName = account.SecurityKey;
            identityUser.SecurityStamp = account.SecurityKey;
            var generatedToken = await _userHelper.GenerateUserTokenAsync(identityUser, _passwordTokenProvider, _passwordTokenPurpose);

            //encode the token to be sent
            var encodedUrl = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(generatedToken));
            var concatedString = account.Id + "~" + UserType.Attendee;
            var encryptedInfo = Security.Encrypt(concatedString, _forgotPasswordEncrytionKey, "_");
            var resetPasswordUrl = resetLink + "?Token=" + encodedUrl + "&Email=" + encryptedInfo;

            var mailContent = _emailMessages.ForgotPassword(resetPasswordUrl);
            await _emailService.SendEmail(email, "Password Reset", mailContent);
        }

        public async Task<LoginToken> ValidateAdminUser(string userName, string password)
        {
            AdminAccount account = _unit.AdminAccounts.GetByEmail(userName);
            if (account == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var identityUser = ConvertAppUserToIdentity(account.SecurityKey);

            var hashedPassword = _userHelper.PasswordHasher.HashPassword(identityUser, password);

            if(account.PasswordHash!= hashedPassword)
            {
                throw new BadRequestException("Invalid username or password");
            }

            return GenerateAdminToken(account);

        }

        public async Task<LoginToken> ValidateRegularUser(string userName, string password)
        {
            RegularAccount account = await _unit.RegularAccounts.GetByEmail(userName);
            if (account == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var identityUser = ConvertAppUserToIdentity(account.SecurityKey);

            var hashedPassword = _userHelper.PasswordHasher.HashPassword(identityUser, password);

            if (account.PasswordHash != hashedPassword)
            {
                throw new BadRequestException("Invalid username or password");
            }

            return GenerateRegularToken(account);
        }

        public static IdentityUser ConvertAppUserToIdentity(string securityKey)
        {
            var identityUser = new IdentityUser();
            identityUser.Id = securityKey;
            identityUser.Email = securityKey;
            identityUser.UserName = securityKey;
            identityUser.SecurityStamp = securityKey;
            return identityUser;
        }
    }
}
