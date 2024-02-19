using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;
using RM.Net;

using ArenaServer.Net;

namespace ArenaServer.Game
{
    public class TargetFilter
    {
        static public List<ArenaObject> GetTargets(Arena arena, Vector2 currentPos, TargetFilterType filterOpt, Func<ArenaObject, bool> customFilter)
        {
            var targetList = new List<ArenaObject>();
            var battleObjects = arena.ObjectList.GetObjects();

            if (TargetFilterType.NearSingle == filterOpt)
            {
                GetNearSingleTarget(currentPos, battleObjects, customFilter, ref targetList);
            }
            else if (TargetFilterType.Multi == filterOpt)
            {
                GetMultiTarget(currentPos, battleObjects, customFilter, ref targetList);
            }

            return targetList;
        }

        static private void GetNearSingleTarget(Vector2 currentPos, List<ArenaObject> arenaObjects, Func<ArenaObject, bool> customFilter, ref List<ArenaObject> targetList)
        {
            ArenaObject target = null;
            float lastDistance = float.MaxValue;

            //battleObjects.Find
            foreach (var arenaObject in arenaObjects)
            {
                if (null != customFilter && false == customFilter(arenaObject))
                {
                    continue;
                }

                //-jinsub, unity lib로 교체
                float[] point1 = new float[2] { currentPos.x, currentPos.y };
                float[] point2 = new float[2] { arenaObject.Position.x, arenaObject.Position.y };
                var distance = (float)Math.Sqrt(point1.Zip(point2, (a, b) => (a - b) * (a - b)).Sum());

                if (lastDistance < distance)
                {
                    continue;
                }

                lastDistance = distance;
                target = arenaObject;
            }

            if (null != target)
            {
                targetList.Add(target);
            }
        }

        // 멀티 타겟      
        static private void GetMultiTarget(Vector2 currentPos, List<ArenaObject> battleObjects, Func<ArenaObject, bool> customFilter, ref List<ArenaObject> targetList)
        {
            foreach (var battleObject in battleObjects)
            {
                if (null != customFilter && false == customFilter(battleObject))
                {
                    continue;
                }

                targetList.Add(battleObject);
            }
        }
    }
}
