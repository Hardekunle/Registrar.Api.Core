using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Entities
{
    public class RegularAccount
    {
        public RegularAccount()
        {
            Id = Guid.NewGuid();
            SecurityKey = Guid.NewGuid().ToString();
        }
        public Guid Id { get; set; }
        public string SecurityKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsPasswordUpdated { get; set; }
        public DateTime? DateOfPasswordUpdate { get; set; }
        public bool IsDeprecated { get; set; }
    }
}
