using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

using RM.Net;
using RM.Server.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class PlayerFactory : Singleton<PlayerFactory>
    {
        public bool Initialize()
        {
            return true;
        }

        public Player Create(Guid playerUID, int dataFromDB)
        {
            Player player = new Player(playerUID);

            //-
            var position = PlayerTestCode.Position(new UnityEngine.Vector2(10.0f, 10.0f), 30.0f, 30.0f);
            var direction = PlayerTestCode.Direction();

            if (false == player.Initialize(position, direction, dataFromDB))
            {
                return null;

            }
            return player;
        }

        private PlayerFactory()
        {
        }
    }
}
