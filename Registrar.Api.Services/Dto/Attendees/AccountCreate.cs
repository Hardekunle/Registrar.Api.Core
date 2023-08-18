using Registrar.Api.Data.Entities;
using Registrar.Api.Services.Dto.Accomodations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Attendees
{

    public class AccountCreate
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }
}
