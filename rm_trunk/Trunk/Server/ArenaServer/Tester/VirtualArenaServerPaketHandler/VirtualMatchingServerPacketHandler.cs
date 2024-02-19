using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Net;
using RM.Server.Net;
using RM.Server.Common;
using ArenaServer.Game;

namespace ArenaServer.Tester
{
    /*
     


        ★ here is VIRTUAL MATCHING SERVER. thanks. 



     */
    public class VirtualMatchingServerPacketHandler
    {
        static public void ArenaMatchingReq(S_ARENA_MATCHING_REQ packet)
        {
            Waiter waiter = new Waiter();
            waiter.Depature = new MATCHING_DEPATURE();
            waiter.Depature = packet.Depature;

            if (false == VirtualMatchingManager.Instance.Request(waiter))
            {
                S_ARENA_MATCHING_ACK ack = new S_ARENA_MATCHING_ACK();
                ack.Result = PacketResult.Fail_MatchingRequestAlready;
                ack.PlayerUID = packet.Depature.PlayerUID;

                VirtualArenaServerPacketHandler.MatchingAck(ack);
            }
        }

        static public void CreateArenaAck(S_ARENA_CREATE_ACK packet)
        {
            packet.MatchedPlayers.All(matchedPlayer => {

                MatchingTask toEachPlayer = new MatchingTask(() => {

                    ARENA_MATCHING_COMPLETE_NTF ntf = new ARENA_MATCHING_COMPLETE_NTF();
                    ntf.Arrival.IP = "0.0.0.0";
                    ntf.Arrival.Port = 69;
                    //ntf.Arrival.PlayerUID = Guid.Empty;//N/A
                    ntf.PlayerUID = matchedPlayer.PlayerUID;

                    VirtualArenaServerPacketHandler.MatchingCompleteNtf(ntf);
                });

                toEachPlayer.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
                return true;
            });
        }
    }
}
