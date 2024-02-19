using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BA.Common;
using BattleServer.Battle;

namespace BattleServer.Net
{
    public class PlayerInfo
    {
        public ChampPresenceKey PresenceKey { get; set; }
        public BattleTeamType BattleTeamType { get; set; }

        //public string PlayerUID { get; set; }
        //public ulong BattleArenaID { get; set; }
    }
}
