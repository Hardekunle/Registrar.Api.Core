using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepo RegularAccounts { get; }
        IAdminRepo AdminAccounts { get; }
        IAccommodationRepo Hostels { get;}
        int SaveToDatabase();
    }
}
