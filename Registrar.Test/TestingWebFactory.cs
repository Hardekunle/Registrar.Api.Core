using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrar.Test
{
   public class TestingWebFactory : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection>? _overrideDependencies;

        public TestingWebFactory(Action<IServiceCollection>? overrideDependencies = null)
        {
            _overrideDependencies = overrideDependencies;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => _overrideDependencies?.Invoke(services));
        }
    }

    public static class MockExtensions
    {
        public static void Mock<TService>(this IServiceCollection @this, Action<Mock<TService>> customize) where TService : class
        {
            var serviceType = typeof(TService);
            if (@this.FirstOrDefault(x => x.ServiceType == serviceType) is { } existingServiceDescriptor)
            {
                @this.Replace(new ServiceDescriptor(serviceType, _ =>
                {
                    var mock = new Mock<TService>();
                    customize(mock);
                    return mock.Object;
                }, existingServiceDescriptor.Lifetime));
                return;
            }

            throw new InvalidOperationException($"'{serviceType}' was not registered in DI Container");
        }
    }


}
