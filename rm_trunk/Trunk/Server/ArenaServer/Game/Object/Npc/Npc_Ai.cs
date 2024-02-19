using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Net;
using RM.Common;
using RM.Server.Common;

using ArenaServer.Resource;
using ArenaServer.Game.AI;

namespace ArenaServer.Game
{
    public partial class Npc
    {
        public override void OnTakeDamage(ArenaObject attacker, int skillID, int damage, int hp)
        {
            base.OnTakeDamage(attacker, skillID, damage, hp);

            m_attackerList.Add(attacker.UID);
            
            if ((null == m_aiNpc.BlackBoard.TargetObject) && (NpcAttribute.Tendency == NpcTendency.Passive))//-testcode, jinsub, aggressive, passive 를 아름답게 처리할순 읎을까?!
            {
                m_aiNpc.BlackBoard.TargetObject = attacker;
            }
        }

        public override void OnUpdate(int id, double accumT, double dT)
        {
            base.OnUpdate(id, accumT, dT);

            //-
            MoverUpdate((float)dT);

            //-
            m_aiNpc.UpdateStateMachine((float)dT);

            //-
            SearchTarget(dT);
        }

        public override bool Move(Vector2 targetPos)
        {
            //Console.WriteLine("last target move {0}, {1}", targetPos.x, targetPos.y);

            //m_mover.Move(targetPos);
            var ret = m_gridMover.Move(targetPos);//-return false when path find stuck.

            NPC_MOVEMENT_NTF packet = new NPC_MOVEMENT_NTF();
            packet.UID = UID;
            packet.StartPos = (NVECTOR3)Position;
            //packet.EndPos = (NVECTOR3)targetPos; -error. don't...
            packet.EndPos = (NVECTOR3)m_gridMover.TargetPosition;
            packet.Direction = Vector2.Angle(Position, targetPos);

            m_arena.BroadCasting(packet);
            return ret;
        }

        private void MoverUpdate(float dt)
        {
            //m_mover.Update(dt);
            //Position = m_mover.Position;

            m_gridMover.Update(UID, dt);
            Position = m_gridMover.Position;//-test code, 문제는 읎는지!? 

        }

        public override void ChangeAiState(AiStateType stateType)
        {
            m_aiNpc.ChangeState(stateType);
        }

        private void SearchTarget(double dT)
        {
            if (NpcAttribute.Tendency == NpcTendency.Aggressive)
            {
                var interval = m_serachingDt += dT;
                if (DEFINE.TEST_SLIME_SEARCHING_INTERVAL_MS > interval)
                {
                    if (null == m_aiNpc.BlackBoard.TargetObject)
                    {
                        //-testcode, targetting 하는 filtering 로직 필요, ObjectManager 에 predicate refactoring필요.
                        //              find 자체를 objectmanager에 일임 할수 없을까나?! arg predicate 를 통해서, 
                        //var find = m_arena.ObjectList.FirstOrDefault(pair =>
                        //{
                        //    if ((pair.Value.ArenaObjectType == ArenaObjectType.Player)
                        //    && (false == pair.Value.IsDead())
                        //    && (NpcAttribute.Sight >= Vector2.Distance(Position, pair.Value.Position)))
                        //    {
                        //        return true;
                        //    }

                        //    return false;
                        //});

                        //if ((null != find.Value) && (ArenaObjectType.Player == find.Value.ArenaObjectType))
                        //{
                        //    m_aiNpc.BlackBoard.TargetObject = find.Value;
                        //}

                        ArenaObject find = null;
                        Arena.ObjectList.Foreach(arenaObject => { //-jinsub, ref : TargetFilter.GetTargets 

                            if ((null == find)
                            &&  (false == arenaObject.IsDead())
                            &&  (ArenaObjectType.Player == arenaObject.ArenaObjectType)
                            &&  (NpcAttribute.Sight >= Vector2.Distance(Position, arenaObject.Position)))
                            {
                                find = arenaObject;
                            }
                        });

                        if (null != find)
                        {
                            m_aiNpc.BlackBoard.TargetObject = find;
                        }
                    }


                    m_serachingDt = 0;
                }
            }
        }

        double m_serachingDt = 0;
    }
}
