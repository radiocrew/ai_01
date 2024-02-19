using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using RM.Common;
using rm_login.Tool.Contents;

namespace rm_login.Tool
{
    public partial class Player
    {
        static private readonly Lazy<Player> s_lazy = new Lazy<Player>(() => new Player());
        static public Player Instance { get { return s_lazy.Value; } }

        static public readonly IPEndPoint sMatchingIP = new IPEndPoint(IPAddress.Parse("192.168.141.3"), 40501);
        static public readonly IPEndPoint sArenaIP = new IPEndPoint(IPAddress.Parse("119.207.119.225"), 30502);

        public bool Initialize()
        {
            m_uid = Guid.NewGuid();
            m_stat = new Stat();
            m_stat.Initialize();
            SelectableDeckList = new List<int>();
            Inventory = new Inventory();
            Inventory.Initialize();

            Console.WriteLine("[{0}] Player Initialize completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            return true;
        }

        public void GenerateUID()
        {
            m_uid = Guid.NewGuid();
        }

        public Guid UID
        {
            get
            {
                return m_uid;
            }
        }

        public PlayerClassType ClassType { get; set;  }

        public string Name { get; set; }

        public int RaidArenaID { get; set; }

        public int SingleArenaID { get; set; }
        
        public string ArenaKey { get; set; }

        public int Level { get; set; }

        public long Exp { get; set; }

        public int ActiveLevel { get; set; }

        public long ActiveExp { get; set; }

        public int HP { get; set; }

        public int HPMax { get; set; }

        public int HPMaxAdd { get; set; }

        public int Atk { get; set; }

        public int AtkAdd { get; set; } 

        public float AtkRate { get; set; }

        public float AtkSpdRate { get; set; }

        public float VampireRate { get; set; }

        public float VigorRecoveryRate { get; set; }

        public float ExpAddRate { get; set; }

        public Stat Stat => m_stat;

        Guid m_uid;
        Stat m_stat = null;

        public List<int> SelectableDeckList { get; set; }

        public int DeckPoint { get; set; }

        public Inventory Inventory { get; set; }
    }
}
