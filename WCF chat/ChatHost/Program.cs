using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace ChatHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
               var host = new ServiceHost(typeof(WCF_chat.Service1));
                using (host)
                {
                host.Open();
                Console.WriteLine("Хост стартовал!");
                Console.ReadKey();
                }
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.R)
                {
                    host.Close();
                    host = new ServiceHost(typeof(WCF_chat.Service1));
                    using (host)
                    {
                        host.Open();
                        Console.WriteLine("Хост стартовал!");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
