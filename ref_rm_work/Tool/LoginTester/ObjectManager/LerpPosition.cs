using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;

namespace rm_login.Object
{
    // 보간
    public class LerpPosition
    {
        public Vector2 Position
        {
            set
            {
                m_Dt = 0.0f;
                m_PastPos = m_CurrentPos;
                m_TargetPos = value;
            }
            get
            {
                return m_CurrentPos;
            }
        }

        public LerpPosition()
        {
            m_LerpTime = 1.0f;
        }

        public LerpPosition(float lerpTime)
        {
            m_LerpTime = lerpTime;
        }

        public float LerpTime()
        {
            return m_LerpTime;
        }

        public void Reset(Vector2 pastPos, Vector2 targetPos, float lerpTime)
        {
            if (0.0f == lerpTime)
            {
                lerpTime = 1.0f;
                Debug.Assert(false);
            }

            m_Dt = 0.0f;
            m_LerpTime = lerpTime;

            m_PastPos = pastPos;
            m_CurrentPos = pastPos;
            m_TargetPos = targetPos;
        }

        public void Reset(Vector2 pastPos, Vector2 targetPos)
        {
            m_Dt = m_LerpTime;

            m_PastPos = pastPos;
            m_CurrentPos = targetPos;
            m_TargetPos = targetPos;
        }

        public void Update(float dt)
        {
            m_Dt += dt;
            if (m_LerpTime <= m_Dt)
            {
                m_Dt = m_LerpTime;
                m_PastPos = m_TargetPos;
            }

            // Current / lerpTime
            m_CurrentPos = RMMath.Lerp(m_PastPos, m_TargetPos, m_Dt / m_LerpTime);
        }


        // Option
        float m_LerpTime = 1.0f; // 기본 1초

        // Variable
        float m_Dt = 0.0f;

        Vector2 m_PastPos;
        Vector2 m_TargetPos;
        Vector2 m_CurrentPos;
    }
}
