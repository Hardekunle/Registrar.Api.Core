using Registrar.Api.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.MySQLCore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccountRepo RegularAccounts => throw new NotImplementedException();

        public IAdminRepo AdminAccounts => throw new NotImplementedException();

        public IAccommodationRepo Hostels => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int SaveToDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
