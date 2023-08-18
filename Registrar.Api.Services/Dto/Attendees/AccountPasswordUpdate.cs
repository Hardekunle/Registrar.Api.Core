using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Attendees
{
    public class AccountPasswordUpdate
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; } 
    }
}
