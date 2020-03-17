using ConsulDemo.Utility.LoadBalancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulDemo.Utility.ConsulDiscovery
{
    public interface IServiceProvider
    {
        // 服务提供者！
        Task<IList<string>> GetServicesAsync(string serviceName);
    }
}
