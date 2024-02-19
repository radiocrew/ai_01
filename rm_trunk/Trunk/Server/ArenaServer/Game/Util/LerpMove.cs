using System.Diagnostics;

using RM.Common;
using RM.Net;

namespace ArenaServer.Util
{
    /*
    public class LerpMove
    {       
        public LerpMove(float speed)
        {
            m_Speed = speed;
        }

        public NVECTOR2 Position
        {
            get
            {
                lock (m_Lock)
                {
                    return m_CurrentPos;
                }
            }
        }

        public NVECTOR2 TargetPosition
        {
            get
            {
                lock (m_Lock)
                {
                    return m_TargetPos;
                }                    
            }
        }

        public bool IsArrival
        {
            get 
            {
                lock (m_Lock)
                {
                    return m_Arrival;
                }                
            }
        }

        public void SetPosition(NVECTOR2 position)
        {
            lock (m_Lock)
            {
                m_Dt = 0.0f;
                m_LerpTime = 0.0f;

                m_PastPos = position;
                m_CurrentPos = position;
                m_TargetPos = position;
                m_Arrival = true;
            }
        }

        public void Move(NVECTOR2 position)
        {
            lock (m_Lock)
            {
                m_Dt = 0.0f;
                m_PastPos = m_CurrentPos;
                m_TargetPos = position;

                //var vector = m_TargetPos - m_CurrentPos; to do formula
                //float distance = vector.Length;
                //m_LerpTime = distance / m_Speed;

                m_Arrival = false;
            }
        }

        public void Update(float dt)
        {
            lock (m_Lock)
            {
                if (true == m_Arrival)
                {
                    return;
                }

                m_Dt += dt;
                if (m_LerpTime <= m_Dt)
                {
                    m_Dt = m_LerpTime;
                    m_CurrentPos = m_PastPos = m_TargetPos;
                    m_Arrival = true;
                    return;
                }

                // Current / lerpTime
                //m_CurrentPos = BAMath.Lerp(m_PastPos, m_TargetPos, m_Dt / m_LerpTime);
            }
        }

        // Lock 처리 해야함
        object m_Lock = new object();

        bool m_Arrival = false;
        float m_Speed = 0.0f;
        float m_LerpTime = 1.0f; // 기본 1초

        // Variable
        float m_Dt = 0.0f;

        NVECTOR2 m_PastPos;
        NVECTOR2 m_TargetPos;
        NVECTOR2 m_CurrentPos;
    }
    */
}
