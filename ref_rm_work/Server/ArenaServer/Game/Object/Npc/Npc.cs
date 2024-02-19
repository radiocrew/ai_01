using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Net;
using RM.Server.Common;

using ArenaServer.Resource;
using ArenaServer.Game.AI;

namespace ArenaServer.Game
{
    public partial class Npc : ArenaObject
    {
        public Npc(Guid uid)
            : base(uid)
        {
        }

        public bool Initialize(int arenaObjectID, Vector2 position, float direction, Arena arena)
        {
            if (false == base.Initialize(arenaObjectID, RM.Common.ArenaObjectType.Npc, position, direction))
            {
                return false;
            }

            var resNpc = ResourceManager.Instance.FindNpc(ArenaObjectID);
            if (null == resNpc)
            {
                return false;
            }

            HEALTH_DATA healthData = new HEALTH_DATA();
            healthData.Hp = resNpc.Health.Hp;
            healthData.HpMax = resNpc.Health.HpMax;
            healthData.Level = resNpc.Level;
            healthData.Exp = 0;
            m_health = new Health();
            m_health.Initialize(healthData);

            m_npcAttribute = new NpcAttribute();
            m_npcAttribute.CopyFrom(resNpc);

            m_attackerList = new NpcAttackerList();
            m_attackerList.Initialize();

            m_aiNpc = new AiNpc(this);
            if (false == m_aiNpc.Initialize(NpcAttribute.AiBagID, NpcAttribute.SkillBagID))
            {
                return false;
            }

            m_gridMover = new GridMover();
            if (false == m_gridMover.Initialize(NpcAttribute.MovementSpeed, arena.GridMap, position, arena.Map.Width, arena.Map.Height))
            {
                return false;
            }

            m_gridMover.SetPosition(UID, position);

            m_updateTimer.Submit(m_updateTimer.GetTimerInterval, (int)TimerDispatcherIDType.ArenaObjectUpdate);
            return true;
        }

        public override void Destory()
        {
            base.Destory();

            m_npcAttribute = null;

            m_attackerList.Destroy();
            m_attackerList = null;

            //m_aiNpc = null; testcode, timer que에 남아있던걸 빼서 쓸수 있음. 
            //m_mover = null;

            m_gridMover.Destory(UID);
            m_gridMover = null;
        }

        public override void OnDie()
        {
            base.OnDie();

            GiveExp();
            Leave();
        }

        public override float Sight
        {
            get
            {
                return m_npcAttribute.Sight;
            }
        }

        public NpcAttribute NpcAttribute
        {
            get => m_npcAttribute;
        }

        NpcAttribute m_npcAttribute = null;
        NpcAttackerList m_attackerList = null;

        AiNpc m_aiNpc = null;
        //Mover m_mover;
        GridMover m_gridMover;
    }
}
