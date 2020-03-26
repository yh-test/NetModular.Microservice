using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class GrpcDemo: GrpcService.GrpcServiceBase
    {
          public override Task<RmResponse> Student(RmRequest request, ServerCallContext context)
        {
            // 1、打印客户端信息
            Console.WriteLine($"request学生姓名:{request.Name}");
            // 2、返回
            return Task.FromResult(new RmResponse
            {
                Message=$"{request.Name}请求了一次服务器，已经收到"
            }
                );
        }
        public override async Task StudentServer(RmRequest request, IServerStreamWriter<RmResponse> responseStream, ServerCallContext context)
        {

            // 1、打印客户端信息
            Console.WriteLine($"request学生姓名:{request.Name}");
            // 2、流式处理(函数流)
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
                RmResponse rmResponse = new RmResponse()
                {
                    Message = "函数流处理，结果：" + i
                };
                await responseStream.WriteAsync(rmResponse);
            }
        }

        }
}
