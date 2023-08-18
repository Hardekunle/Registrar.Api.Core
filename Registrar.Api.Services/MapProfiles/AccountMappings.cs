using Registrar.Api.Data.Entities;
using Registrar.Api.Services.Dto.Attendees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.MapProfiles
{
    internal class AccountMappings
    {
        internal static RegularAccount Map(AccountCreate attendee, RegularAccount regularAccount)
        {
            throw new NotImplementedException();
        }

        internal static AccountDetails Map(RegularAccount account, AccountDetails accountDetails)
        {
            throw new NotImplementedException();
        }

        internal static AccountList Map(RegularAccount account, AccountList accountList)
        {
            throw new NotImplementedException();
        }
    }
}
