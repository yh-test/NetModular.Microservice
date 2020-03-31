using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SocketServer
{
   public class SimpleSocketServer
    {
       
        protected static byte[] ObjectToByteArray(object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            ms.Close();
            return ms.ToArray();
        }
        protected static object ByteAraayToObject(byte[] b)
        {
            MemoryStream ms = new MemoryStream(b,0,b.Length);
            BinaryFormatter bf = new BinaryFormatter();
             return bf.Deserialize(ms);
        }
        public static void process()
        {
            int port = 2020;
            string host = "192.168.11.222";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);

            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            socket.Bind(ipe);
            socket.Listen(0);
            Console.WriteLine("监听已经打开，请等待");

            Socket serverSocket = socket.Accept(); 
            Console.WriteLine("监听已经建立");
            while(true)
            {
                string recStr = "";
                byte[] recByte=new byte[1024 * 1024];
                int bytes = serverSocket.Receive(recByte, recByte.Length, 0);
                recStr = Encoding.ASCII.GetString(recByte, 0, bytes);
                Console.WriteLine("服务器端获得信息:{0}", recStr);
                if (recStr.Equals("stop"))
                {
                    serverSocket.Close();//关闭该socket对象
                    Console.WriteLine("关闭链接。。。。");
                    break;
                }
                Console.WriteLine("输入回发消息");
                string sendStr = Console.ReadLine();
                byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
                serverSocket.Send(sendByte);
            }
            serverSocket.Close();
        }
    }
}
