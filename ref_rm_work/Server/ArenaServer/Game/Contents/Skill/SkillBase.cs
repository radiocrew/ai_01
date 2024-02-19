using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public abstract class Skill : RMCollision
    {
        public Skill(ResData_Skill jsonSkill)
        {
            m_jsonSkill = jsonSkill;
        }

        public abstract CollisionBase GetCollision();

        public virtual void DoAction(Arena arena, ArenaObject owner, SkillParam skillParam)
        {
        }

        public abstract float SkillRange();

        protected float SkillRange(ResData_Collision resCollision)
        {
            /*
            CollisionType

                Circle = 1,
                Arg_0 : radius

                Arc = 2,
                Arg_0 : radius
                Arg_1 : internal angle

                Rect = 3,
                Arg_0 : width
                Arg_1 : height

                Line = 4,
                Arg_0 : length
                Arg_1 : line width
            */
            switch (resCollision.CollisionType)
            {
                case CollisionType.Circle:
                    return resCollision.Arg_0;
                case CollisionType.Arc:
                    return resCollision.Arg_0;
                case CollisionType.Rect:
                    return resCollision.Arg_1;
                case CollisionType.Line:
                    return resCollision.Arg_0;
            }

            return 0.0f;
        }

        public ResData_Skill JsonSkill { get => m_jsonSkill; }

        readonly protected ResData_Skill m_jsonSkill;
    }
}
