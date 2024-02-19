using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game
{
    public class CollisionArc : CollisionBase
    {
        public CollisionArc(float radius, float internalAngle)
        {
            m_Radius = radius;
            m_InternalAngle = internalAngle;
        }

        public override bool Collision(CollisionBase collisionBase)
        {
            return collisionBase.CollisionByArc(this);
        }

        public override bool CollisionByArc(CollisionArc collisionRect)
        {
            return true;
        }

        public override bool CollisionByCircle(CollisionCircle collisionCircle)
        {
            return CollisionBase.CollisionCircleArc(collisionCircle, this);
        }

        public float Radius
        {
            get { return m_Radius; }
        }

        public float InternalAngle
        {
            get { return m_InternalAngle; }
        }

        readonly float m_Radius = 0.0f;          // 반지름
        readonly float m_InternalAngle = 0.0f;   // 내각 (Degree)
    }
}
