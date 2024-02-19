using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Net;

namespace ArenaServer.Game
{
    public class Movement
    {
        public UnityEngine.Vector2 Position
        {
            get
            {
                lock (m_lock)
                {
                    return m_position;
                }
            }
            set
            {
                lock (m_lock)
                {
                    m_position = value;
                }

            }
        }

        public float Direction
        {
            get
            {
                lock (m_lock)
                {
                    return m_direction;
                }
            }
            set
            {
                lock (m_lock)
                {
                    m_direction = value;
                }
            }
        }

        public MOVEMENT_DATA SerializeData()
        {
            var movementData = new MOVEMENT_DATA();
            lock (m_lock)
            {
                movementData.Position = new NVECTOR3(m_position.x, 0, m_position.y);
                movementData.Direction = m_direction;
            }

            return movementData;
        }

        public void Destory()
        {
            m_position = UnityEngine.Vector2.zero;
            m_direction = 0.0f;
        }

        object m_lock = new object();
        UnityEngine.Vector2 m_position;
        float m_direction = 0.0f;
    }
}
