using Microsoft.EntityFrameworkCore;
using Registrar.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Registrar.Api.Data.MySQLCore.Configurations
{
    public class ReadWriteContext : DbContext
    {
        public ReadWriteContext(DbContextOptions<ReadWriteContext> configOpt) : base(configOpt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration<RegularAccount>();
            //builder.ApplyConfiguration<AdminAccount>();
            base.OnModelCreating(builder);
        }
        public DbSet<RegularAccount> Attendees { get; set; }
        public DbSet<AdminAccount> AdminAccount { get; set; }
    }
}
