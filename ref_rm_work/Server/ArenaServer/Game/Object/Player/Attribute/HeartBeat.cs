using System;
using System.Threading;

using RM.Server.Common;

namespace ArenaServer.Game
{
    public class HeartBeat
    {
        static readonly int InitCount = DEFINE.TIME_PLAYER_HEARTBEAT_WAIT_S;

        public void Initialize(Player own)
        {
            m_timeHeartBeater = new TimerHeartbeater<Player>(own);
            m_timeHeartBeater.Submit(DEFINE.TIME_PlAYER_HEARTBEAT_INTERVAL_MS, (int)TimerDispatcherIDType.PlayerHeartbeat);
        }

        public void Destroy()
        {
            m_timeHeartBeater.Destroy();
            m_timeHeartBeater = null;
        }

        public bool Revive()
        {
            if (0 < m_count)
            {
                Interlocked.Exchange(ref m_count, InitCount);
                return true;
            }

            return false;
        }

        public bool Pulse()
        {
            Interlocked.Decrement(ref m_count);

            var ret = (0 <= m_count);
            if (true == ret)
            {
                return true;
            }

            return false;
        }

        public bool Stop
        {
            get
            {
                return Interlocked.Equals(1, m_stop);
            }
            set
            {
                Interlocked.Exchange(ref m_stop, ((value == true) ? 1 : 0));
            }
        }
        
        volatile int m_stop = 0;// 0 for false, 1 for true,
        volatile int m_count = InitCount;
        TimerHeartbeater<Player> m_timeHeartBeater = null;
    }
}
