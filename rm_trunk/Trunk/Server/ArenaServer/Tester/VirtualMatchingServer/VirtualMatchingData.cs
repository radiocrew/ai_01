using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Net;

namespace ArenaServer.Tester
{
    public class Waiter
    {
        public Waiter()
        {
        }

        public Waiter(ARENA_DEPATURE depature)
        {
            Depature = depature;
        }

        public ARENA_DEPATURE Depature { get; set; }

        public int ArenaID { get; set; }
    }
}
