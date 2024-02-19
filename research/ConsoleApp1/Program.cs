using System;
using System.Threading;
using System.Threading.Tasks;
using System.Buffers;
using System.Text;
using System.ComponentModel;
using ConsoleApp1.Classes;
using System.Collections.Generic;


namespace ConsoleApp1
{
    class Program
    {

        static void Main(params string[] args)
        {
            string full = "1:ItemID[1]\n2:ItemID[2]\n3:ItemID[1]\n";

            //var sep = full.Split("\n");
            var sep = full.Split('\n');


            string newfull = string.Empty;


            int delid = 2;

            foreach (var item in sep)
            {
                if (0 < item.Length)
                {
                    int idx = item.IndexOf(':');
                     var num = item.Substring(0, idx);

                    if (delid != int.Parse(num))
                    {
                        newfull += (item + '\n');
                    }
                }
            }

            Console.WriteLine(newfull);
            return;

        }
    }
}
