using System;
using System.Threading;

namespace RM.Server.Common
{
    public interface ITimerUpdate
    {
        void OnUpdate(int id, double accumT, double dT);
    }

    public class TimerUpdater<T> : TimerBase
        where T : ITimerUpdate
    {
        public TimerUpdater(ulong timerInterval, T timerObject)
        {
            m_timerInterval = timerInterval;

            m_objectLock = new object();
            m_object = timerObject;

            m_gameTimer = new GameTimer();
            m_gameTimer.Start();
        }

        public override void CallBack(int id)
        {
            T tempObject = default(T);
            lock (m_objectLock)
            {
                tempObject = m_object;
            }

            if (null != tempObject)
            {
                double accumT = .0f;
                double dT = .0f;
                m_gameTimer.Elapsed(out accumT, out dT);

                tempObject.OnUpdate(id, accumT, dT);
                Submit(m_timerInterval, id);
            }
        }

        public void Destroy()
        {
            lock (m_objectLock)
            {
                m_object = default(T);
            }
            
            m_gameTimer.Stop();
            m_gameTimer = null;
        }

        GameTimer m_gameTimer;

        public ulong GetTimerInterval { get => m_timerInterval; }

        object m_objectLock = new object();
        T m_object = default(T);
        private readonly ulong m_timerInterval;
    }

    public interface ITimerHeartbeat
    {
        void OnHeartbeat(int id, uint count);
    }

    public class TimerHeartbeater<T> : TimerBase
    where T : ITimerHeartbeat
    {
        public TimerHeartbeater(T timerObject)
        {
            m_objectLock = new object();
            m_object = timerObject;
        }

        public override void CallBack(int id)
        {
            T tempObject = default(T);
            lock (m_objectLock)
            {
                tempObject = m_object;
            }

            if (null != tempObject)
            {
                tempObject.OnHeartbeat(id, (uint)IncrementCount());
                Submit(1000, id);
            }
        }

        public void Destroy()
        {
            m_object = default(T);
        }

        public int IncrementCount()
        {
            return Interlocked.Increment(ref m_count);
        }

        object m_objectLock = new object();
        T m_object = default(T);
        private volatile int m_count = 0;
    }

}