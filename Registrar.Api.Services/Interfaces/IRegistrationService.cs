﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<(string, string)> RegisterForEvent();
        Task<(string, string)> GetAccommodation();
    } 
}
