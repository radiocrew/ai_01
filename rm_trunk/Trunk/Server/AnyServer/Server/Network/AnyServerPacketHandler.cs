using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Net;
using RM.Server.Common;

namespace AnyServer.Net
{
    public class AnyServerPacketHandler
    {
        public void InitHandler()
        {
            m_PacketHandlers.Add(RMProtocols.TEST_COMMAND, new PacketHandler<AnySession, TEST_COMMAND>(TestCommand));
        }

        public void TestCommand(AnySession session, TEST_COMMAND packet)
        {
        }

        public IPacketHandler<AnySession> this[RMProtocols pID]
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

        Dictionary<RMProtocols, IPacketHandler<AnySession>> m_PacketHandlers = new Dictionary<RMProtocols, IPacketHandler<AnySession>>();
    }
}
