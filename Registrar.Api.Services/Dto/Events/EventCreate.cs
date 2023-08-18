using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Events
{
    public class EventCreate
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}
