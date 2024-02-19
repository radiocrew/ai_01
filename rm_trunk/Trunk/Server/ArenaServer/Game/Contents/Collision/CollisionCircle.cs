using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaServer.Game
{
    public class CollisionCircle : CollisionBase
    {
        public CollisionCircle(float radius)
        {
            m_Radius = radius;
        }

        public override bool Collision(CollisionBase collisionBase)
        {
            return collisionBase.CollisionByCircle(this);
        }

        public override bool CollisionByCircle(CollisionCircle collisionCircle)
        {
            return CollisionBase.CollisionCircleCircle(this, collisionCircle);
        }

        public override bool CollisionByArc(CollisionArc collisionArc)
        {
            return CollisionBase.CollisionCircleArc(this, collisionArc);
        }

        public float Radius
        {
            get { return m_Radius; }
        }

        readonly float m_Radius = 0.0f;          // 반지름
    }
}
