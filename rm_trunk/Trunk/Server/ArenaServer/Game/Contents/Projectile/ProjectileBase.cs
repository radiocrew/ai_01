using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using UnityEngine;

using RM.Net;
using RM.Server.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public abstract class ProjectileBase : ITimerUpdate
    {
        public ProjectileBase(Arena arena, ArenaObject owner, ResData_Skill jsonSkill)
        {
            m_arena = arena;
            m_owner = owner;
            m_jsonSkill = jsonSkill;
        }

        public Guid UID { get; set; }

        public Vector2 Position { get; set; }

        public virtual bool Initialize()
        {
            m_updateTimer = new TimerUpdater<ProjectileBase>(DEFINE.TIME_PROJECTILE_UPDATE_INTERVAL_MS, this);
            m_updateTimer.Submit(m_updateTimer.GetTimerInterval, (int)TimerDispatcherIDType.ProjectileUpdate);

            return true;
        }

        public virtual void Destroy()
        {
            m_updateTimer.Destroy();
            m_updateTimer = null;
        }

        public abstract PROJECTILE_DATA SerializedData();

        //public abstract bool IsRemoved();//-jinsub 필요없어도, 그냥 삭제해뻐려도 되지 않을까?! 

        public virtual void OnUpdate(int id, double accumT, double dT)
        {
        }

        protected bool Stop
        {
            get
            {
                return Interlocked.Equals(DEFINE.True, m_stop);
            }
            set
            {
                Interlocked.Exchange(ref m_stop, ((value == true) ? DEFINE.True : DEFINE.False));
            }
        }
   
        protected Arena m_arena = null;
        protected ArenaObject m_owner = null;
        protected readonly ResData_Skill m_jsonSkill = null;

        protected TimerUpdater<ProjectileBase> m_updateTimer = null;

        private volatile int m_stop = DEFINE.False;
    }
}
