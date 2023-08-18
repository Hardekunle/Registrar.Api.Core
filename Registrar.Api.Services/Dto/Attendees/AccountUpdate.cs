using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Attendees
{
    public class AccountUpdate : AccountCreate
    {
        public string Id { get; set; }
    }
}
