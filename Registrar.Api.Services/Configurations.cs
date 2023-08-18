using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registrar.Api.Data.Entities;
using Registrar.Api.Data.MySQLCore;
using Registrar.Api.Data.MySQLCore.Configurations;
using Registrar.Api.Services.Interfaces;
using Registrar.Api.Services.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Services
{
    public static class Configurations
    {
        public static IServiceCollection AddInternalServiceConfig(this IServiceCollection collection, IConfiguration config)
        {

            collection.AddScoped<IAccountService, AccountService>();
            collection.AddScoped<IAuthService, AuthService>();
            collection.AddScoped<IEventService, EventService>();
            collection.AddScoped<IAccommodationService, AccommodationService>();
                

            return collection;
        }
    }
}
