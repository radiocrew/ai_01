using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using Level9.Expedition.Mobile.Util;

using RM.Common;
using RM.Net;
using RM.Server.Common;

using ArenaServer.Game;

namespace ArenaServer.Tester
{
    public class VirtualDB
    {
    }

    public class VirtualDBServer : Singleton<VirtualDBServer>
    {
        public bool Initialize()
        {
            return true;
        }

        private VirtualDBServer()
        {
        }
    }
}
