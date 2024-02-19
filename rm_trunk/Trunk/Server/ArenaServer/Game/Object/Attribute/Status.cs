using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Net;

namespace ArenaServer.Game
{
    public class Status
    {
        public Status()
        {
            m_statusLock = new object();
            m_status = new Dictionary<StatusType, float>();
        }

        public bool Initialize()
        {
            lock (m_statusLock)
            {
                m_status.Clear();
            }

            return true;
        }

        public void Add(StatusType type, float value = 0.0f)
        {
            lock (m_statusLock)
            {
                m_status[type] = value;
            }
        }

        public bool Have(StatusType type)
        {
            lock (m_statusLock)
            {
                return m_status.ContainsKey(type);
            }
        }

        public void Remove(StatusType type)
        {
            lock (m_statusLock)
            {
                m_status.Remove(type);
            }
        }

        public STATUS_DATA SerializeData()
        {
            var data = new STATUS_DATA();

            lock (m_statusLock)
            {
                data.Status = new Dictionary<StatusType, float>(m_status);
            }

            return data;
        }

        public void Destroy()
        {
            lock (m_statusLock)
            {
                m_status.Clear();
            }
        }

        object m_statusLock = null;
        Dictionary<StatusType, float> m_status = null;
    }
}
