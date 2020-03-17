using ConsulDemo.Utility.ConsulBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulDemo.Utility.ConsulDiscovery
{
    public static class ServiceProviderExtension
    {
        public static IServiceBuilder CreateServiceBuilder(this IServiceProvider serviceProvider, Action<IServiceBuilder> config)
        {

            var builder = new ServiceBuilder(serviceProvider);
            config(builder);
            return builder;
        }
    }
}
