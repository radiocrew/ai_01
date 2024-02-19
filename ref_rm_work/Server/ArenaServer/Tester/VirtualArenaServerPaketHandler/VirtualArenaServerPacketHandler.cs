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
     


        ★ here is VIRTUAL ARENA SERVER. thanks. 



     */
    class VirtualArenaServerPacketHandler
    {
        static public void MatchingAck(S_ARENA_MATCHING_ACK packet)
        {
            //-matching server → arena server

            var playerUID = packet.PlayerUID;
            var result = packet.Result;

            MatchingTask mt = new MatchingTask(() => {
                var player = ArenaMemberManager.Instance.GetPlayer(playerUID);

                ARENA_MATCHING_ACK ack = new ARENA_MATCHING_ACK();
                ack.Result = result;
                ack.PlayerUID = playerUID;
                player.SendPacket(ack);
            });
        }

        static public void CreateArenaReq(S_ARENA_CREATE_REQ packet)
        {
            //-matching server → arena server

            int arenaID = 0;
            Guid arenaKey = Guid.Empty;

            /*
             * 
             * 여기에서 새로운 아레나를 생성.
             * 
             */
            var arena = ArenaManager.Instance.Create(packet.ArenaID);
            if (null != arena)
            {
                arenaID = arena.ID;
                arenaKey = arena.Key;
            }

            MatchingTask mt = new MatchingTask(() => {

                S_ARENA_CREATE_ACK ack = new S_ARENA_CREATE_ACK();
                ack.ArenaKey = arenaKey;
                ack.MatchedPlayers = new List<MATCHING_DEPATURE>();

                packet.MatchedPlayers.All(player => {
                    ack.MatchedPlayers.Add(player);
                    return true;
                });

                VirtualMatchingServerPacketHandler.CreateArenaAck(ack);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        //static public void MatchingAck(ARENA_MATCHING_ACK packet)
        //{
        //}

        //static public void MathcingCancel(ARENA_MATCHING_CANCEL packet)
        //{
            
        //}

        static public void MatchingCompleteNtf(ARENA_MATCHING_COMPLETE_NTF packet)
        {
            var player = ArenaMemberManager.Instance.GetPlayer(packet.PlayerUID);
            player.SendPacket(packet);
        }
    }
}
