using ConsulDemo.Utility.ConsulDiscovery;
using ConsulDemo.Utility.LoadBalancer;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceCustomerWithPolly
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ConsulServiceProvider(new Uri("http://127.0.0.1:8500"));
            var myServiceA = serviceProvider.CreateServiceBuilder(builder =>
            {
                builder.ServiceName = "MyServiceA";
                // 指定负载均衡器
                builder.LoadBalancer = TypeLoadBalancer.RoundRobin;
                // 指定了Uri方案
                builder.UriScheme = Uri.UriSchemeHttp;
            });

            var httpClient = new HttpClient();
            var policy = ServiceCustomerWithPolly.PolicyBuilder.CreatePolly();

            // 重试轮询+熔断降级
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"-------------第{i}次请求-------------");
                policy.Execute(() =>
                {
                    try
                    {
                        // 根据负载均衡获取具体服务地址
                        // consul崩了，仍然会被我们的polly接住
                        // 服务和服务间调用也一样
                        // 服务间传消息？直接调用API,MQ
                        // 每个有服务调用的服务应用，都用POLLY，服务挂了
                        var uri = myServiceA.BuildAsync("/api/order").Result;
                        Console.WriteLine($"{DateTime.Now} - 正在调用：{uri}");
                        var content = httpClient.GetStringAsync(uri).Result;
                        Console.WriteLine($"调用结果：{content}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"调用异常：{e.GetType()}");
                        throw;
                    }

                });
                Task.Delay(1000).Wait();
            }
        }
    }
}
