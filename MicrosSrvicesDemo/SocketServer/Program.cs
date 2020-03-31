using System;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SimpleSocketServer.process();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
