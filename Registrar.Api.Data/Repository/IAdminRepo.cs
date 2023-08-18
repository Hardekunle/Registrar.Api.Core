using Registrar.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Repository
{
    public interface IAdminRepo
    {
        AdminAccount GetByEmail(string email);
        Task<AdminAccount> GetById(string adminId);
    }
}
