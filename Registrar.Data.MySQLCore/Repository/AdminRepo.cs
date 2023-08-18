using Registrar.Api.Data.Entities;
using Registrar.Api.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.MySQLCore.Repository
{
    public class AdminRepo : IAdminRepo
    {
        public AdminAccount GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AdminAccount> GetById(string adminId)
        {
            throw new NotImplementedException();
        }
    }
}
