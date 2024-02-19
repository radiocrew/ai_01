using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace rm_login.Object
{
    public partial class ArenaObject
    {
        public Vector2 Position
        {
            get
            {
                return m_position;
            }
            //set
            //{
            //    m_position = value;
            //    m_move.SetPosition(value);
            //}
        }

        public void SetPosition(float x, float y)
        {
            m_position.x = x;
            m_position.y = y;
            m_move.SetPosition(new Vector2(x, y));
        }

        public void UpdatePosition(Vector2 newPos, Vector2 targetPos, float direction)
        {
            m_move.UpdatePosition(newPos, targetPos);

            // 보간 vector
            Vector2 lerpVec = Position - newPos;
            m_Interpolation.Reset(lerpVec, new Vector2(0, 0), m_InterpolationTime);

            // Direction
            //LerpDirection = direction;// BAMath.VecToDegree(targetPos - newPos);
        }

        public void Update(float dt)
        {
            //base.Update(dt);

            m_move.Update(dt);
            m_Interpolation.Update(dt);

            //-simjinsub, choose and, check 
            m_position = m_move.Position + m_Interpolation.Position;


            return;
            //base.Position = m_Move.Position;
        }

        protected LerpAtoB m_move = new LerpAtoB(20.0f);  // 속도

        Vector2 m_position = new Vector2();

        // 서버 클라 위치 보정
        LerpPosition m_Interpolation = new LerpPosition();
        float m_InterpolationTime = 1.0f;
    }
}
