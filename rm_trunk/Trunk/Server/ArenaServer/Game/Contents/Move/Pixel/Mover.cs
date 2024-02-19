using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;

namespace ArenaServer.Game
{
    public class Mover
    {
        public Mover(float speed)
        {
            m_speed = speed;
        }

        public Vector2 Position
        {
            get
            {
                lock (m_lock)
                {
                    return m_currentPos;
                }
            }
        }

        public Vector2 TargetPosition
        {
            get
            {
                lock (m_lock)
                {
                    return m_targetPos;
                }
            }
        }

        public bool IsArrival
        {
            get
            {
                lock (m_lock)
                {
                    return m_arrival;
                }
            }
        }

        public void SetPosition(Vector2 pos)
        {
            lock (m_lock)
            {
                m_dt = 0.0f;
                m_arrival = true;
                m_takeTime = 0.0f;

                m_pastPos = pos;
                m_targetPos = pos;
                m_currentPos = pos;
            }
        }

        public void Move(Vector2 pos)
        {
            lock (m_lock)
            {
                m_dt = 0.0f;
                m_pastPos = m_currentPos;
                m_targetPos = pos;

                var vector = m_targetPos - m_currentPos;
                float distance = vector.magnitude;
                m_takeTime = distance / m_speed;

                m_arrival = false;
            }
        }

        public void Update(float dt)
        {
            lock (m_lock)
            {
                if (true == m_arrival)
                {
                    return;
                }

                m_dt += dt;

                if (m_takeTime <= m_dt)
                {
                    m_dt = m_takeTime;
                    m_pastPos = m_currentPos = m_targetPos;
                    m_arrival = true;
                    return;
                }

                m_currentPos = RMMath.Lerp(m_pastPos, m_targetPos, m_dt / m_takeTime);
            }
        }

        object m_lock = new object();

        float m_speed = 0.0f;
        float m_dt = 0.0f;
        bool m_arrival = false;
        float m_takeTime = 1.0f;
        
        Vector2 m_pastPos;
        Vector2 m_targetPos;
        Vector2 m_currentPos;
    }
}
