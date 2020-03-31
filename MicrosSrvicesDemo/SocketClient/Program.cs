using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("启动一个socketer链接");
            int port = 2020;
            string host="192.168.11.222";
            IPAddress ip =  IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(ipe);

            while(true)
            {
                Console.WriteLine("请发送信息到服务端");
                string result = Console.ReadLine();
                if (result == "exit")
                    break;
                byte[] sendBtyes = Encoding.ASCII.GetBytes(result);
                socketClient.Send(sendBtyes);


                //收到的消息
                string recStr = "";
                byte[] recBytes = new byte[1024 * 1024];
                int bytes = socketClient.Receive(recBytes, recBytes.Length, 0);
                recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
                Console.WriteLine($"服务器回发:{recStr}");
            }
            socketClient.Close();
            Console.ReadKey();
        }
    }
}
