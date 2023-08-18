using Registrar.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Repository
{
    public interface IAccountRepo
    {
        Task AddAsync(RegularAccount newAccount);
        Task<List<RegularAccount>> GetAll(bool activeOnly);
        Task<RegularAccount> GetByEmail(string email);
        Task<RegularAccount> GetById(string regularId);
    }
}
