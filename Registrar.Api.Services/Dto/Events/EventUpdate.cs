﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Dto.Events
{
    public class EventUpdate
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}
