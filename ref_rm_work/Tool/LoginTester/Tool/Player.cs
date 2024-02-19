using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using RM.Common;

namespace rm_login.Tool
{
    public partial class Player
    {
        static private readonly Lazy<Player> s_lazy = new Lazy<Player>(() => new Player());
        static public Player Instance { get { return s_lazy.Value; } }

        static public readonly IPEndPoint sMatchingIP = new IPEndPoint(IPAddress.Parse("192.168.0.5"), 40501);
        static public readonly IPEndPoint sArenaIP = new IPEndPoint(IPAddress.Parse("192.168.0.5"), 30502);

        //public delegate void UIEvent(int type, string data);
        //public event UIEvent uiFunc;

        //public delegate void PositionEvent(int type, int x, int y);
        //public event PositionEvent posFunc;

        public bool Initialize()
        {
            m_uid = Guid.NewGuid();

            Console.WriteLine("[{0}] Player Initialize completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            return true;
        }

        //public void ToUI(int type, string data)
        //{
        //    uiFunc(type, data);
        //}

        //public void ToPosition(int type, int x, int y)
        //{
        //    posFunc(type, x, y);
        //}

        

        public Guid UID
        {
            get
            {
                return m_uid;
            }

            set
            {
                m_uid = value;
            }
        }

        Guid m_uid = new Guid();

        public int RaidArenaID { get; set; }

        public int SingleArenaID { get; set; }
        
        public string ArenaKey { get; set; }

        public int Level { get; set; }

        public long Exp { get; set; }

        public int ActiveLevel { get; set; }

        public long ActiveExp { get; set; }

    }
}
