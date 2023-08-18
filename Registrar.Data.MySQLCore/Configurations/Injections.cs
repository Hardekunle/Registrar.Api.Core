using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registrar.Api.Data.MySQLCore.Repository;
using Registrar.Api.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Api.Data.MySQLCore.Configurations
{
    public static class Injections
    {
        public static string connectionString;
        public static IServiceCollection AddEFConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdminRepo, AdminRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            connectionString = config.GetSection("ConnectionStrings").GetSection("").Value;
            services.AddDbContext<ReadWriteContext>(config => config.UseMySql(connectionString, default));
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<ReadWriteContext>();

            return services;
        }
    }
}
