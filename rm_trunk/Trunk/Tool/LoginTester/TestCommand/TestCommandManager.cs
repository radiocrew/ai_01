using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using RM.Net;
using RM.Common;

using rm_login.Tool.Script;

namespace rm_login.Tool
{
    public class TestCommandManager
    {
        static private readonly Lazy<TestCommandManager> s_lazy = new Lazy<TestCommandManager>(() => new TestCommandManager());
        static public TestCommandManager Instance { get { return s_lazy.Value; } }

        public bool Initialize()
        {
            return false;
        }

        public void Destroy()
        {
        }

        public TEST_COMMAND Parsing(string fullcommand)
        {
            int firstidx = fullcommand.IndexOf('/');
            int lastidx = fullcommand.IndexOf(" ");
            if ((0 == firstidx) && (0 < lastidx))
            {
                string commnad = fullcommand.Substring(firstidx + 1, lastidx - 1);
                var testCommandType = ResourceManager.Instance.FindTestCommand(commnad);
                if (TestCommandType.None == testCommandType)
                {
                    LogDelegate.Instance.Log(string.Format("Unknown test command : [{0}]", commnad), Color.Red);
                    return null;
                }

                var args = fullcommand.Substring(lastidx + 1, fullcommand.Length - lastidx - 1).Split(' ');
                if (0 < args.Count())
                {
                    TEST_COMMAND packet = null;
                    if (false == SetPacketArg(out packet, testCommandType, args))
                    {
                        LogDelegate.Instance.Log(string.Format("Wrong test command args.. : [{0}]", commnad), Color.Red);
                        return null;
                    }

                    return packet;
                }
            }

            LogDelegate.Instance.Log(string.Format("Warning test command invalid"), Color.Red);
            return null;
        }

        private bool SetPacketArg(out TEST_COMMAND packet, TestCommandType commandType, string[] args)
        {
            packet = new TEST_COMMAND();
            packet.TestCommandType = commandType;

            int uiID = 0;
            Item item = null;

            switch (packet.TestCommandType)
            {
                case TestCommandType.DelItem:
                    uiID = ParseArg(args.ElementAt(0));
                    item = Player.Instance.Inventory.FindItem(uiID);
                    if (null == item)
                    {
                        return false;
                    }

                    packet.UID_0 = item.UID;
                    break;
                case TestCommandType.EquipItem:
                    uiID = ParseArg(args.ElementAt(0));
                    item = Player.Instance.Inventory.FindItem(uiID);
                    if (null == item)
                    {
                        return false;
                    }

                    packet.UID_0 = item.UID;
                    packet.Int_0 = (2 <= args.Count()) ? (ParseArg(args.ElementAt(1))) : (0);
                    break;
                case TestCommandType.CreateNpc:
                    packet.Int_0 = ParseArg(args.ElementAt(0));
                    packet.Float_0 = (2 <= args.Count()) ? (ParseArg(args.ElementAt(1))) : (0);
                    packet.Float_1 = (3 <= args.Count()) ? (ParseArg(args.ElementAt(2))) : (0);
                    packet.Int_1   = (4 <= args.Count()) ? (ParseArg(args.ElementAt(3))) : (0);
                    break;
                case TestCommandType.Login:
                    Player.Instance.GenerateUID();
                    packet.UID_0 = Player.Instance.UID;
                    packet.Int_0 = ClassTypeParse(args.ElementAt(0));
                    if (0 == packet.Int_0)
                    {
                        return false;
                    }
                    break;
                case TestCommandType.ShutDown:
                    System.Windows.Forms.Application.ExitThread();
                    break;
                default:
                    packet.Int_0 = ParseArg(args.ElementAt(0));
                    packet.Int_1 = (2 <= args.Count()) ? (ParseArg(args.ElementAt(1))) : (0);
                    packet.Int_2 = (3 <= args.Count()) ? (ParseArg(args.ElementAt(2))) : (0);
                    break;
            }

            return true;
        }

        private int ParseArg(string arg)
        {
            if (string.Empty != arg)
            {
                return int.Parse(arg);
            }
            return 0;
        }

        private int ClassTypeParse(string arg)
        {
            PlayerClassType findClassType = PlayerClassType.None;

            Enum.GetValues(typeof(PlayerClassType)).Cast<PlayerClassType>().Any(element => {
                if ((PlayerClassType.None != element) && (arg == element.ToString().ToLower()))
                {
                    findClassType = element;
                    return true;
                }
                return false;
            });

            int arenaObjectID = 0;

            ResourceManager.Instance.JsonPlayer.Any(element => {
                if (findClassType == element.Value.PlayerClassType)
                {
                    arenaObjectID = element.Value.ArenaObjectID;
                    return true;
                }
                
                return false;
            });

            return arenaObjectID;
        }
    }
}
