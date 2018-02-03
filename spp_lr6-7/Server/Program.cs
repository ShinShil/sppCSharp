using System;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(WcfService.MessageService)))
            {
                host.Open();
                Console.WriteLine("Server started");
                Console.ReadLine();
            }
        }
    }
}
