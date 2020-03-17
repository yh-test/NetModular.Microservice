using ConsulDemo.Utility.LoadBalancer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsulDemo.Utility.ConsulDiscovery;
using System;

namespace ConsulDemo.Utility.ConsulBuilder
{
    public class ServiceBuilder : IServiceBuilder
    {
        public ConsulDemo.Utility.ConsulDiscovery.IServiceProvider ServiceProvider { get; set; }
        public string ServiceName { get; set; }
        public string UriScheme { get; set; }
        public ILoadBalancer LoadBalancer { get; set; }
        public ServiceBuilder(ConsulDiscovery.IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task<Uri> BuildAsync(string path)
        {
            var serviceList = await ServiceProvider.GetServicesAsync(ServiceName);
            var service = LoadBalancer.Resolve(serviceList);
            var baseUri = new Uri($"{UriScheme}://{service}");
            var uri = new Uri(baseUri, path);
            return uri;
        }
    }
}
