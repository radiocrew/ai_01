using System;
using System.Diagnostics;
using System.Timers;

namespace ArenaServer.Util
{
    public class DurationTimer 
    {
        // 고정 프레임 쓰지말자
        //public bool IsFixed { get; set; }

        public Action<float, float> DtUpdate { get; set; }
        public Action<float> TickUpdate { get; set; }


        public DurationTimer(float dtInterval, float tickInterval)
        {
            m_DtTimer = new System.Timers.Timer(10);
            m_DtTimer.Elapsed += Step;

            m_TickTimer = new System.Timers.Timer(tickInterval);
            m_TickTimer.Elapsed += Tick;
           
            m_TargetElapsedTime = TimeSpan.FromTicks(166667 * 10); // 고정일때 프레임 6fps
            m_MaxElapsedTime = TimeSpan.FromMilliseconds(2000);
            //IsFixed = false;
        }

        public void Dispose()
        {
            // UpdateTimer 해제
            m_DtTimer.Stop();
            m_DtTimer.Dispose();

            m_TickTimer.Stop();
            m_TickTimer.Dispose();

            m_GameTimer.Stop();
        }

        public void Start()
        {
            m_DtTimer.Start();
            m_TickTimer.Start();
            m_GameTimer.Start();            
        }

        public void Stop()
        {
            m_DtTimer.Stop();
            m_TickTimer.Stop();
            m_GameTimer.Stop();
        }

        

        private void Step(Object source, ElapsedEventArgs e)
        {
            lock (m_GameTimer)
            {
                var currentTicks = m_GameTimer.Elapsed.Ticks;
                m_AccumulatedElapsedTime += TimeSpan.FromTicks(currentTicks - m_PreviousTicks);
                m_PreviousTicks = currentTicks;

                m_GameTimeElapsedGameTime = m_AccumulatedElapsedTime;
                m_GameTimeTotalGameTime += m_AccumulatedElapsedTime;
                m_AccumulatedElapsedTime = TimeSpan.Zero;
            }

            // Invoke!!
            DtUpdate?.Invoke((float)m_GameTimeTotalGameTime.TotalSeconds, (float)m_GameTimeElapsedGameTime.TotalSeconds);

            //if (IsFixed)
            //{
            //    FixedTimeStep();
            //}
            //else
            //{
            //    VariableTimeStep();
            //}
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            float dt = (float)(m_TickTimer.Interval * 0.001f);
            TickUpdate?.Invoke(dt);
        }

        // 고정 dt
        private void FixedTimeStep()
        {
            if (m_AccumulatedElapsedTime < m_TargetElapsedTime)
            {
                //Console.WriteLine("Leg!!!");
            }

            if (m_AccumulatedElapsedTime > m_MaxElapsedTime)
            {
                m_AccumulatedElapsedTime = m_MaxElapsedTime;
            }

            m_GameTimeElapsedGameTime = m_TargetElapsedTime;
            var stepCount = 0;

            // Perform as many full fixed length time steps as we can.
            while (m_AccumulatedElapsedTime >= m_TargetElapsedTime)
            {
                m_GameTimeTotalGameTime += m_TargetElapsedTime;
                m_AccumulatedElapsedTime -= m_TargetElapsedTime;
                ++stepCount;

                DtUpdate?.Invoke((float)m_AccumulatedElapsedTime.TotalSeconds, (float)m_GameTimeElapsedGameTime.TotalSeconds);
            }

            //Every update after the first accumulates lag
            m_UpdateFrameLag += Math.Max(0, stepCount - 1);

            //Every time we just do one update and one draw, then we are not running slowly, so decrease the lag
            if (stepCount == 1 && m_UpdateFrameLag > 0)
            {
                m_UpdateFrameLag--;
            }


            // Draw needs to know the total elapsed time
            // that occured for the fixed length updates.
            m_GameTimeElapsedGameTime = TimeSpan.FromTicks(m_TargetElapsedTime.Ticks * stepCount);

        }

        // 가변 dt
        private void VariableTimeStep()
        {
            m_GameTimeElapsedGameTime = m_AccumulatedElapsedTime;
            m_GameTimeTotalGameTime += m_AccumulatedElapsedTime;
            m_AccumulatedElapsedTime = TimeSpan.Zero;

            // Invoke!!
            DtUpdate?.Invoke((float)m_GameTimeTotalGameTime.TotalSeconds, (float)m_GameTimeElapsedGameTime.TotalSeconds);
        }


        #region Step Timer
        readonly System.Timers.Timer m_DtTimer = null;
        readonly System.Timers.Timer m_TickTimer = null;
        #endregion


        #region Duration
        Stopwatch m_GameTimer = new Stopwatch();

        TimeSpan m_TargetElapsedTime = TimeSpan.FromTicks(166667 * 10); // 6fps
        TimeSpan m_MaxElapsedTime = TimeSpan.FromMilliseconds(2000);
        //TimeSpan _inactiveSleepTime = TimeSpan.FromSeconds(0.02);

        TimeSpan m_AccumulatedElapsedTime = TimeSpan.Zero;

        TimeSpan m_GameTimeElapsedGameTime = TimeSpan.Zero;
        TimeSpan m_GameTimeTotalGameTime = TimeSpan.Zero;

        long m_PreviousTicks = 0;
        int m_UpdateFrameLag = 0;
        #endregion
    }
}
