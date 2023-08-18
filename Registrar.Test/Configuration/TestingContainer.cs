using Microsoft.EntityFrameworkCore;
using Registrar.Api.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Test.Configuration
{
    public class TestingContainer
    {
        private ReadWriteContext _dbContext;

        public TestingContainer() { }

    }
}
