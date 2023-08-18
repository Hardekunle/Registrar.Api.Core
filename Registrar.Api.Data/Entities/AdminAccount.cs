using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Entities
{
    public class AdminAccount
    {
        public AdminAccount()
        {
            Id = Guid.NewGuid();
            SecurityKey = Guid.NewGuid().ToString();
        }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string SecurityKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
    }
}
