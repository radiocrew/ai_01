using System;
using System.Collections.Generic;
using System.Diagnostics;

using ArenaServer.Resource;

namespace ArenaServer.Util
{
    public class TimeStream
    {
        public TimeStream(float lifeTime)
        {
            m_LifeTime = lifeTime;
            m_CurrentLifeTime = lifeTime;
            LifeType = TimeStreamLifeType.Once;
        }

        public bool IsRemove { get; set; }

        public Action StartAction { get; set; }

        public Action EndAction { get; set; }

        public TimeStreamLifeType LifeType { get; set; }

        public int InvokeCount { get; set; }

        public void ResetLifeTime(float lifeTime)
        {
            m_LifeTime = lifeTime;
            m_CurrentLifeTime = lifeTime;
        }

        public virtual void Update(float dt)
        {
            if (TimeStreamLifeType.Once == LifeType)
            {
                Update_Once(dt);
            }
            else if (TimeStreamLifeType.Infinite == LifeType)
            {
                Update_Infinite(dt);
            }
            else
            {
                Debug.Assert(false);
            }
        }

        protected virtual void Update_Once(float dt)
        {
            if (true == IsRemove)
            {
                return;
            }

            m_CurrentLifeTime -= dt;
            if (0 >= m_CurrentLifeTime)
            {
                m_CurrentLifeTime = 0.0f;
                IsRemove = true;
                // 이벤트 호출
                EndAction?.Invoke();
            }
        }

        protected virtual void Update_Infinite(float dt)
        {
            if (true == IsRemove)
            {
                return;
            }

            m_CurrentLifeTime -= dt;
            if (0 >= m_CurrentLifeTime)
            {
                m_CurrentLifeTime = m_LifeTime;
                // 이벤트 호출
                EndAction?.Invoke();
            }
        }

        protected virtual void Update_Count(float dt)
        {
            if (true == IsRemove)
            {
                return;
            }
            // 미구현
            Debug.Assert(false);
        }

        protected float m_LifeTime = 0.0f;
        protected float m_CurrentLifeTime = 0.0f;
    }

    //public class InfiniteTimeSteam : TimeStream
    //{
    //    public InfiniteTimeSteam(float lifeTime)
    //        : base(lifeTime)
    //    {
    //    }

    //    public override void Update(float dt)
    //    {
    //        if (true == IsRemove)
    //        {
    //            return;
    //        }

    //        m_CurrentLifeTime -= dt;
    //        if (0 >= m_CurrentLifeTime)
    //        {
    //            m_CurrentLifeTime = m_LifeTime;
    //            // 이벤트 호출
    //            EndAction?.Invoke();
    //        }
    //    }
    //}
}
