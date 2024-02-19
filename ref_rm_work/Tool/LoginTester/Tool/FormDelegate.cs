using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rm_login.Tool
{
    public class FormDelegate
    {
        static private readonly Lazy<FormDelegate> s_lazy = new Lazy<FormDelegate>(() => new FormDelegate());
        static public FormDelegate Instance { get { return s_lazy.Value; } }

        public delegate void EventHandler(ToolDefine.UIType type, string data);
        public event EventHandler UIEvent;

        public bool Initialize()
        {
            Console.WriteLine("[{0}] Form delegate Initialize completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            return true;
        }

        public void ToUI(ToolDefine.UIType type, string data)
        {
            UIEvent(type, data);
        }

        public void UIClear()
        {
            ToUI(ToolDefine.UIType.MatchingTimerStop, string.Empty);

            ToUI(ToolDefine.UIType.PlayerClass, "0");
            ToUI(ToolDefine.UIType.PlayerName, "0");

            ToUI(ToolDefine.UIType.PlayerHp, "0");
            ToUI(ToolDefine.UIType.PlayerHpMax, "0");
            ToUI(ToolDefine.UIType.PlayerMp, "0");
            ToUI(ToolDefine.UIType.PlayerMpMax, "0");

            ToUI(ToolDefine.UIType.PlayerLevel, "0");
            ToUI(ToolDefine.UIType.PlayerActiveLevel, "0");

            ToUI(ToolDefine.UIType.PlayerAtk, "0");
            ToUI(ToolDefine.UIType.PlayerAtkSpdRate, string.Empty);
            ToUI(ToolDefine.UIType.PlayerVampireRate, string.Empty);
            ToUI(ToolDefine.UIType.PlayerVigorRecoveryRate, string.Empty);
            ToUI(ToolDefine.UIType.PlayerExpAddRate, string.Empty);

            ToUI(ToolDefine.UIType.ActiveDeckList, string.Empty);
            ToUI(ToolDefine.UIType.DeckPoint, string.Empty);
            ToUI(ToolDefine.UIType.Deck1, string.Empty);
            ToUI(ToolDefine.UIType.Deck2, string.Empty);
            ToUI(ToolDefine.UIType.Deck3, string.Empty);

            ToUI(ToolDefine.UIType.Inventory, string.Empty);

            ToUI(ToolDefine.UIType.TestCommand, string.Empty);
        }
    }
}
