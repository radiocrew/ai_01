using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using Level9.Expedition.Mobile.Util;

using RM.Net;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class NpcFactory : Singleton<NpcFactory>
    {
        public bool Initialize()
        {
            return true;
        }

        public Npc Create(ResData_DeployArenaObject deployArenaObject, Arena arena)
        {
            var npc = new Npc(Guid.NewGuid());
            Vector2 position = new Vector2(deployArenaObject.Position.x, deployArenaObject.Position.y);

            //-jinsub, deploy pos 가 30,40인데 map size 가10, 10 이면 m_gridMap.AddOrUpdate(uid, gridCoord.X, gridCoord.Y); 안에서 디진다,

            if (true == npc.Initialize(deployArenaObject.ID, position, deployArenaObject.Direction, arena))
            {
                return npc;
            }

            Console.WriteLine("error, npc[{0}] initialize failed.", deployArenaObject.ID);
            return null;
        }

        private NpcFactory()
        {
        }
    }
}
