using System;
using System.Collections.Generic;
using System.Diagnostics;

using RM.Net;
using RM.Common;

namespace ArenaServer.Net
{

    public class MatchingPacketHandlers
    {
        public MatchingPacketHandlers()
        {
            InitHandler();
        }

        public void InitHandler()
        {
        }

        public IPacketHandler<RMInternalClient> this[RMProtocols pID]
        {
            get
            {
                if (m_PacketHandlers.ContainsKey(pID))
                {
                    return m_PacketHandlers[pID];
                }
                return null;
            }
        }

        Dictionary<RMProtocols, IPacketHandler<RMInternalClient>> m_PacketHandlers = new Dictionary<RMProtocols, IPacketHandler<RMInternalClient>>();
    }




}
