using Registrar.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Account
{
    public class ForgotPassword
    {
        public string Email { get; set; }
        public UserType UserType { get; set; }
    }
}
