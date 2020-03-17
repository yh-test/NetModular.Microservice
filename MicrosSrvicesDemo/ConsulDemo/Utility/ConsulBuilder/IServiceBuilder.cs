using ConsulDemo.Utility.LoadBalancer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulDemo.Utility.ConsulBuilder
{
    public interface IServiceBuilder
    {
        // 服务提供者！
        ConsulDemo.Utility.ConsulDiscovery.IServiceProvider ServiceProvider { get; set; }

        // 服务名称!
        string ServiceName { get; set; }

        // Uri方案
        string UriScheme { get; set; }

        // 你用哪种策略？
        ILoadBalancer LoadBalancer { get; set; }

        Task<Uri> BuildAsync(string path);
    }
}
