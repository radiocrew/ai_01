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
        static public void ArenaMove(ARENA_MOVE_REQ packet)
        {
            VirtualMatchingServer.Instance.Move(packet);
        }

        static public void MatchedArenaMove(MATCHED_ARENA_MOVE_REQ packet)
        {   
            VirtualMatchingServer.Instance.MatchedMove(packet);
        }

        static public void ArenaMatchingReq(S_ARENA_MATCHING_REQ packet)
        {
            Waiter waiter = new Waiter();
            waiter.Depature = new ARENA_DEPATURE();
            waiter.Depature = packet.Depature;
            waiter.ArenaID = packet.ArenaID;

            var playerUID = waiter.Depature.PlayerUID;

            bool ret = VirtualMatchingManager.Instance.Waiting(waiter);

            MatchingTask mt = new MatchingTask(() => {

                S_ARENA_MATCHING_ACK ack = new S_ARENA_MATCHING_ACK();
                ack.PlayerUID = playerUID;
                ack.Result = (true == ret) ? (PacketResult.Success) : (PacketResult.Fail_MatchingRequestAlready);
                VirtualArenaServerPacketHandler.MatchingAck(ack);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        static public void CreateArenaAck(S_ARENA_CREATE_ACK packet)
        {
            var reqType = packet.RequestType;
            var arenaKey = packet.ArenaKey;
            var result = packet.Result;

            packet.MatchedPlayers.All(matchedPlayer => {

                MatchingTask toEachPlayer = new MatchingTask(() => {

                    if (RequestArenaType.Single == reqType)
                    {
                        VirtualArenaServerPacketHandler.CreateArenaAck(packet);
                    }
                    else
                    {
                        S_ARENA_MATCHING_COMPLETE_NTF ntf = new S_ARENA_MATCHING_COMPLETE_NTF();
                        ntf.Arrival = new ARENA_ARRIVAL();
                        ntf.Arrival.IP = "0.0.0.0";
                        ntf.Arrival.Port = 69;
                        ntf.Arrival.ArenaKey = arenaKey;
                        ntf.PlayerUID = matchedPlayer.PlayerUID;
                        ntf.Result = result;

                        VirtualArenaServerPacketHandler.MatchingCompleteNtf(ntf);
                    }
                });

                toEachPlayer.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
                return true;
            });
        }

        static public void ArenaMatchingCancel(S_ARENA_MATCHING_CANCEL packet)
        {
            VirtualMatchingManager.Instance.Cancel(packet.PlayerUID);
        }

    }
}
