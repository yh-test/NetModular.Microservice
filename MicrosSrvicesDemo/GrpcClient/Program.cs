using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ///单词调用
            ////1、开始调用连接
            //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //// 2、创建客户端
            //var client = new GrpcService.GrpcServiceClient(channel);
            ////开始请求
            //var reply = client.Student(new RmRequest { Name = "小明" });

            //Console.WriteLine(reply.Message);
            //Console.ReadKey();


            ///流式调用
            Grpc();

            Console.ReadKey();
        }
        private static async void Grpc()
        {
            //1、开始调用连接
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            // 2、创建客户端
            var client = new GrpcService.GrpcServiceClient(channel);

            #region 服务器流式处理 (服务器可以响应多个函数)
            var responsesStream = client.StudentServer(
                 new RmRequest { Name = "小强" }
            );

            var readStream = responsesStream.ResponseStream;

            // 1、获取一次输出（for 循环每一次写出）
            while (await readStream.MoveNext())
            {
                RmResponse rmResponse = readStream.Current;

                Console.WriteLine($"客户端收到请求:{rmResponse.Message}");
            }

            Console.WriteLine("Press any key to exit...");

            #endregion
        }
    }
}
