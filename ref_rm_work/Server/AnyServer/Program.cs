using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnyServer.Net;    

namespace AnyServer.Net
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            AnyServer server = new AnyServer();

            Console.WriteLine("go");

            if (false == server.Begin(server, "AnyServer.exe.config"))
            {
                //-mission failed.
                Console.WriteLine("false");
            }
            else
            {
                Console.WriteLine("waiting");
                server.Wait();
            }

            server.End();
            server = null;

        }
    }
}
