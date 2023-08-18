using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto
{
    public class LoginToken
    {
        public string AccessToken { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
