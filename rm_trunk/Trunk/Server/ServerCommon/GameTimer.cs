using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace RM.Server.Common
{
    public class GameTimer
    {
        public void Start()
        {
            m_timer.Start();
        }

        public void Stop()
        {
            m_timer.Stop();
        }

        public void Elapsed(out double accumulateTime, out double deltaTime)
        {
            accumulateTime = 0;
            deltaTime = 0;

            lock (m_timer)
            {
                var cuTicks = m_timer.Elapsed.Ticks;
                m_momentTime += TimeSpan.FromTicks(cuTicks - m_prevTicks);
                m_prevTicks = cuTicks;

                m_elapsedTime = m_momentTime;
                m_totalTime += m_momentTime;
                m_momentTime = TimeSpan.Zero;

                accumulateTime = m_totalTime.TotalSeconds;
                deltaTime = m_elapsedTime.TotalSeconds;
            }
        }

        long m_prevTicks = 0;

        TimeSpan m_momentTime = TimeSpan.Zero;
        TimeSpan m_elapsedTime = TimeSpan.Zero;
        TimeSpan m_totalTime = TimeSpan.Zero;

        Stopwatch m_timer = new Stopwatch();
    }
}
