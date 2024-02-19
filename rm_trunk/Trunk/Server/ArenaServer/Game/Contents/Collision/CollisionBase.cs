using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;
using RM.Net;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public interface RMCollision
    {
        CollisionBase GetCollision();
    }

    public abstract class CollisionBase
    {
        public UnityEngine.Vector2 Position
        {
            get
            {
                lock (m_Lock)
                {
                    return m_Position;
                }
            }
            set
            {
                lock (m_Lock)
                {
                    m_Position = value;
                }
            }
        }

        public float Direction
        {
            get
            {
                lock (m_Lock)
                {
                    return m_Direction;
                }
            }
            set
            {
                lock (m_Lock)
                {
                    m_Direction = value;
                }
            }
        }

        public object m_Lock = new object();
        public UnityEngine.Vector2 m_Position;
        public float m_Direction;

        public abstract bool Collision(CollisionBase collisionBase);
        public abstract bool CollisionByArc(CollisionArc collisionRect);
        public abstract bool CollisionByCircle(CollisionCircle collisionCircle);

        static public bool Collision(RMCollision collisionA, RMCollision collisionB)
        {
            var baseColl_A = collisionA.GetCollision();
            var baseColl_B = collisionB.GetCollision();

            if (null != baseColl_A && null != baseColl_B)
            {
                return baseColl_A.Collision(baseColl_B);
            }
            return false;
        }

        static public CollisionBase CreateCollision(ResData_Collision jsonCollision)
        {
            CollisionBase collision = null;

            switch (jsonCollision.CollisionType)
            {
                case CollisionType.Circle:
                    {
                        collision = new CollisionCircle(jsonCollision.Arg_0);
                    }
                    break;
                case CollisionType.Arc:
                    {
                        collision = new CollisionArc(jsonCollision.Arg_0, jsonCollision.Arg_1);
                    }
                    break;
                case CollisionType.Rect:
                    {
                    }
                    break;
                case CollisionType.Line:
                    {
                    }break;
            }

            return collision;
        }

        static public CollisionBase CreateCollision(int collisonID)
        {
            var resCollision = ResourceManager.Instance.FindCollision(collisonID);
            if (null != resCollision)
            {
                return CreateCollision(resCollision.Collision);
            }

            return null;
        }


        static protected bool CollisionCircleCircle(CollisionCircle collisionA, CollisionCircle collisionB)
        {
            return RMMath.IntersectCircleCircle(collisionA.Position, collisionA.Radius, collisionB.Position, collisionB.Radius);
        }

        static protected bool CollisionCircleArc(CollisionCircle collisionA, CollisionArc collisionB)
        {
            // 원 충돌
            if (false == RMMath.IntersectCircleCircle(collisionA.Position, collisionA.Radius, collisionB.Position, collisionB.Radius))
            {
                return false;
            }

            // 원안에 원호 포함
            if (true == RMMath.IntersectCircleCircle(collisionA.Position, collisionA.Radius, collisionB.Position, 0.1f))
            {
                return true;
            }

            var dirA = collisionA.Position;
            var dirB = collisionB.Position;

            // 바라보는 각 
            Vector2 dir = dirA - dirB;
            float radian = RMMath.NearAngleVectors(dir, RMMath.AngleToVector2(collisionB.Direction));
            
            //Console.WriteLine("{0}, {1}", RMMath.ToDegree(radian), collisionB.InternalAngle);

            if (RMMath.ToDegree(radian) <= collisionB.InternalAngle)
            {
                return true;
            }

            return false;
        }
    }
}
