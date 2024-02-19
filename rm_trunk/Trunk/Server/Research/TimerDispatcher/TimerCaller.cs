using System;
using System.Threading;

//using ArenaServer.Game;

namespace RM.Server.Common
{
    public interface ITimerUpdate
    {
        void OnUpdate(int id, double accumT, double dT);
    }

    public class TimerUpdater<T> : TimerInstance
        where T : ITimerUpdate
    {
        public TimerUpdater(ulong timerInterval, T timerObject)
        {
            m_timerInterval = timerInterval;
            m_object = timerObject;

            //m_gameTimer = new GameTimer();
            //m_gameTimer.Start();
        }

        public override void TimerExpired(int id)
        {
            if (null != m_object)
            {
                double accumT = .0f;
                double dT = .0f;
                //m_gameTimer.Elapsed(out accumT, out dT);

                m_object.OnUpdate(id, accumT, dT);
                Submit(m_timerInterval, id);
            }
        }

        public void Destroy()
        {
            m_object = default(T);
            //m_gameTimer.Stop();
        }

        //GameTimer m_gameTimer;

        public ulong GetTimerInterval { get => m_timerInterval; }

        T m_object = default(T);
        private readonly ulong m_timerInterval;
    }

    public interface ITimerHeartbeat
    {
        void OnHeartbeat(int id, uint count);
    }

    public class TimerHeartbeater<T> : TimerInstance
    where T : ITimerHeartbeat
    {
        public TimerHeartbeater(T timerObject)
        {
            m_object = timerObject;
        }

        public override void TimerExpired(int id)
        {
            if (null != m_object)
            {
                m_object.OnHeartbeat(id, (uint)IncrementCount());
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

        T m_object = default(T);
        private volatile int m_count = 0;
    }

}