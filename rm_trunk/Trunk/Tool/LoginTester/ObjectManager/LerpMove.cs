using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace rm_login.Object
{
    public class LerpAtoB
    {
        static public Vector2 Lerp(Vector2 start, Vector2 finish, float rate)
        {
            return (start * (1f - rate) + finish * rate);
        }

        public LerpAtoB()
        {
            //m_LerpTime = 1.0f;
        }

        public LerpAtoB(float speed)
        {
            m_Speed = speed;
        }

        public float Speed { set => m_Speed = value; }

        
        public Vector2 Position
        {
            get
            {
                return m_CurrentPos;
            }
        }

        public void SetPosition(Vector2 position)
        {
            m_Dt = 0.0f;
            m_LerpTime = 0.0f;

            m_StartPos = position;
            m_CurrentPos = position;
            m_EndPos = position;
        }

        public void UpdatePosition(Vector2 startPos, Vector2 endPos)
        {
            m_Dt = 0.0f;
            m_CurrentPos = m_StartPos = startPos;
            m_EndPos = endPos;

            // 공식
            // 도착 시간 = speed / Distance
            // 거리에 따라서 시간을 다시구함
            var vector = m_EndPos - m_CurrentPos;
            float distance = vector.magnitude;
            m_LerpTime = distance / m_Speed;

        }

        public void Update(float dt)
        {
            m_Dt += dt;
            if (m_LerpTime <= m_Dt)
            {
                m_Dt = m_LerpTime;
                m_CurrentPos = m_StartPos = m_EndPos;
                return;
            }

            // Current / lerpTime
            //m_CurrentPos = BAMath.Lerp(m_StartPos, m_EndPos, m_Dt / m_LerpTime);
        }

        public bool IsMoving()
        {
            return (m_StartPos != m_EndPos);
        }

        Vector2 m_StartPos;     // 시작 
        Vector2 m_EndPos;       // 끝 (목표)
        Vector2 m_CurrentPos;   // 현재

        // Option
        float m_Speed = 0.0f;
        float m_LerpTime = 0.0f;

        // Variable 
        float m_Dt = 0.0f;

        //m_Interpolation
        //LerpPosition


    }
}
