using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

using RM.Net;
using RM.Server.Common;
using RM.Server.Net;

namespace ArenaServer.Tester
{
    public class VirtualMatchingManager : Singleton<VirtualMatchingManager>
    {
        public bool Initialize()
        {
            m_waiterList = new List<Waiter>();
            return true;
        }

        public bool Waiting(Waiter request)
        {
            lock (m_waiterListLock)
            {
                if (true == m_waiterList.Any(waiter => {
                    return (waiter.Depature.PlayerUID == request.Depature.PlayerUID);
                }))
                {
                    return false;
                }

                m_waiterList.Add(request);
            }

            int requestArenaID = request.ArenaID;

            //-매칭이 되었는지 평가.
            DelayedTask dt = new DelayedTask(() => {
                Evaluate(requestArenaID);
            });
            dt.SubmitImmediately((int)TimerDispatcherIDType.MathingRequest);

            return true;
        }

        public void Cancel(Guid playerUID)
        {
            int ret = 0;
            int remainCount = 0;

            lock (m_waiterListLock)
            {
                ret = m_waiterList.RemoveAll(waiter => {
                    return (playerUID == waiter.Depature.PlayerUID);
                });

                remainCount = m_waiterList.Count();
            }

            //-testcode, log
            Console.WriteLine("Matching cancel result[{0}], remain waiter count[{1}]", ((0 < ret) ? ("completed") : ("miss")), remainCount);

            if (0 == ret)
            {
            //-매칭 리스트에 없다는 것은, 매칭 취소를 받을 필요가?! 없다..
                return;
            }

            MatchingTask mt = new MatchingTask(() => {

                S_ARENA_MATCHING_CANCEL cancel = new S_ARENA_MATCHING_CANCEL();
                cancel.PlayerUID = playerUID;

                VirtualArenaServerPacketHandler.MatchingCancel(cancel);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        private void Evaluate(int evaluateArenaID)
        {
            //-
            List<Waiter> matchedList = new List<Waiter>();

            lock (m_waiterListLock)
            {
                m_waiterList.Where(waiter => (evaluateArenaID == waiter.ArenaID)).All(
                find => {
                    if (DEFINE.TEST_RAID_PLAYER_COUNT > matchedList.Count)
                    {
                        matchedList.Add(find);
                    }

                    return true;
                });

            //-성사,
                if (DEFINE.TEST_RAID_PLAYER_COUNT == matchedList.Count)
                {
                    m_waiterList.RemoveAll(waiter => matchedList.Contains(waiter));
                    RequestCreateArena(RequestArenaType.Raid, evaluateArenaID, matchedList);
                }
            }
        }

        //-single move, arena move 에 공용,
        public void RequestCreateArena(RequestArenaType reqType, int arenaID, List<Waiter> waiterList)
        {
            MatchingTask mt = new MatchingTask(()=> {

                S_ARENA_CREATE_REQ req = new S_ARENA_CREATE_REQ();
                req.RequestType = reqType;
                req.ArenaID = arenaID;
                req.MatchedPlayers = new List<ARENA_DEPATURE>();

                waiterList.All(waiter => {
                    req.MatchedPlayers.Add(waiter.Depature);

                    return true;
                });

                /*
                 
                 *-jinsub, 이쯤에서 matching server 에서 arena server L/B 가 들어가줘야 함,
                 
                 */
                VirtualArenaServerPacketHandler.CreateArenaReq(req);
            });

            mt.Send((int)TimerDispatcherIDType.Test_MatchingProcess);
        }

        private VirtualMatchingManager()
        {
        }

        object m_waiterListLock = new object();
        List<Waiter> m_waiterList;
    }
}
