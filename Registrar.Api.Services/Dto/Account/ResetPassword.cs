using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Account
{
    public class ResetPassword
    {
        public string EncryptedInfo { get; set; }
        public string Password { get; set; }
    }
}
