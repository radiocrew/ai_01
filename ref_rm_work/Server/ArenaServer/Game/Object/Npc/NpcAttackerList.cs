using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game
{
    public class NpcAttackerList
    {
        const int MaxCount = 5;

        public bool Initialize()
        {
            m_lock = new object();
            m_attackers = new Queue<Guid>();

            return true;
        }

        public void Add(Guid uid)
        {
            lock (m_lock)
            {
                m_attackers.Enqueue(uid);

                if (MaxCount < m_attackers.Count)
                {
                    m_attackers.Dequeue();
                }
            }
        }

        public Guid GetLast()
        {
            Guid last;

            lock (m_lock)
            {
                last = m_attackers.ElementAtOrDefault((m_attackers.Count - 1));
            }

            return last;
        }

        public void Destroy()
        {
            m_attackers.Clear();
            m_attackers = null;
        }

        object m_lock = null;
        Queue<Guid> m_attackers = null;
    }
}
