using Consul;
using ConsulDemo.Utility.LoadBalancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulDemo.Utility.ConsulDiscovery
{
    public class ConsulServiceProvider : ConsulDiscovery.IServiceProvider
    {
        private readonly ConsulClient _consuleClient;

        public ConsulServiceProvider(Uri uri)
        {
            _consuleClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = uri;
            });
        }

        public async Task<IList<string>> GetServicesAsync(string serviceName)
        {
            // Health 当前consul里已注册的服务，健康检查的信息也拿过来
            // HTTP API
            var queryResult = await _consuleClient.Health.Service(serviceName, "", true);
            var result = new List<string>();
            foreach (var service in queryResult.Response)
            {
                result.Add(service.Service.Address + ":" + service.Service.Port);
            }
            return result;
        }
    }
}
