using Registrar.Api.Data.Entities;
using Registrar.Api.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.MySQLCore.Repository
{
    public class AccountRepo : IAccountRepo
    {
        public Task AddAsync(RegularAccount newAccount)
        {
            throw new NotImplementedException();
        }

        public Task<List<RegularAccount>> GetAll(bool activeOnly)
        {
            throw new NotImplementedException();
        }

        public Task<RegularAccount> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<RegularAccount> GetById(string regularId)
        {
            throw new NotImplementedException();
        }
    }
}
