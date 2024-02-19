using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Net;
using ArenaServer.Net;

namespace ArenaServer.Game
{
    public class SkillParam
    {
        public SkillParam()
        {
        }

        public bool Initialize(Arena arena, SKILL_PARAM_DATA data)
        {
            if (null == data)
            {
                return false;
            }

            if (Guid.Empty != data.UID)
            {
                Target = arena.ObjectList.FindObject(data.UID);
                if (null == Target)
                {
                    return false;
                }
            }

            CastPosition = new Vector2(data.CastPosition.X, data.CastPosition.Z);
            Direction = data.Direction;
            TargetPosition = new Vector2(data.TargetPosition.X, data.TargetPosition.Z);
            JogRate = data.JogRate;

            return true;
        }

        public SKILL_PARAM_DATA SerializeData()
        {
            var data = new SKILL_PARAM_DATA();

            data.CastPosition = new NVECTOR3(CastPosition.x, .0f, CastPosition.y);
            data.Direction = Direction;
            data.TargetPosition = new NVECTOR3(TargetPosition.x, .0f, TargetPosition.y);
            data.JogRate = JogRate;

            if (null != Target)
            {
                data.UID = Target.UID;
            }

            return data;
        }
        public Vector2 CastPosition { get; set; }
        public float Direction { get; set; }
        public Vector2 TargetPosition { get; set; }
        public ArenaObject Target { get; set; }
        public float JogRate { get; set; }
    }
}
