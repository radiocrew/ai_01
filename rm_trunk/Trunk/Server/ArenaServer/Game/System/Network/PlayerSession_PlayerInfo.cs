using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Net.SuperSocket.Server.TCP;

using RM.Net;
using RM.Common;
using RM.Server.Common;

using ArenaServer.Game;

namespace ArenaServer.Net
{
    public class PlayerInfo
    {
        public PlayerInfo(Guid account, Guid player)
        {
            AccountUID = account;
            PlayerUID = player;
        }

        public Guid AccountUID { get; set; }

        public Guid PlayerUID { get; set; }
    }

    public sealed partial class PlayerSession : SSSession<PlayerSession>
    {
        public Player Player
        {
            get
            {
                if (null != m_playerInfo)
                {
                    return ArenaMemberManager.Instance.GetPlayer(m_playerInfo.PlayerUID);
                }

                return null;
            }
        }

        public void InitPlayerInfo(Guid accountUID, Guid playerUID)
        {
            m_playerInfo = new PlayerInfo(accountUID, playerUID);
        }

        public void DestoryPlayerInfo()
        {
            if (null != m_playerInfo)
            {
                //m_playerInfo.AccountUID
                //m_playerInfo.PlayerUID = null;
            }

            m_playerInfo = null;
        }

        private PlayerInfo m_playerInfo = null;
    }
}