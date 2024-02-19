using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

using rm_login.Tool;
using rm_login.Tool.Contents;

namespace rm_login
{
    public partial class Form1
    {
        public void UIEvent(ToolDefine.UIType type, string data)
        {
            Action<TextBox, string> action = (t, s) =>
            {
                t.Invoke((MethodInvoker)delegate {
                    t.Clear();
                    t.Text = data;
                });
            };

            Action<TextBox, string, Color, Color> actionWithColor = (t, s, fc, bc) => {
                t.Invoke((MethodInvoker)delegate {
                    t.BackColor = (0 < data.Length) ? bc : Color.White;
                    t.ForeColor =  (0 < data.Length) ? fc : Color.Black;
                    t.Clear();
                    t.Text = data;
                });
            };

            switch (type)
            {
                case ToolDefine.UIType.PlayerX:
                    action(tbx_x, data);
                    break;
                case ToolDefine.UIType.PlayerY:
                    action(tbx_y, data);
                    break;
                case ToolDefine.UIType.PlayerZ:
                    action(tbx_z, data);
                    break;
                case ToolDefine.UIType.ArenaIDTextBox:
                    action(arenaIdtextBox3, data);
                    break;
                case ToolDefine.UIType.MapInfoTextBox:
                    action(tbx_map_info, data);
                    break;
                case ToolDefine.UIType.MatchingTimerStop:
                    m_matchingTimer.Change(Timeout.Infinite, Timeout.Infinite);

                    pgb_matching.Invoke((MethodInvoker)delegate {
                        pgb_matching.Value = 0;
                    });
                    break;
                case ToolDefine.UIType.AccountUIDTextBox:
                    action(accountUidTextBox4, data);
                    break;
                case ToolDefine.UIType.PlayerUIDTextBox:
                    action(playerUidTextBox5, data);
                    break;
                case ToolDefine.UIType.MatchingAck:
                    m_matchingTimer.Change(0, 10);
                    break;
                case ToolDefine.UIType.PlayerHp:
                    action(tbx_hp, data);
                    break;
                case ToolDefine.UIType.PlayerHpMax:
                    action(tbx_hpMax, data);
                    break;
                case ToolDefine.UIType.PlayerHpMaxAdd:
                    action(tbx_hpmax_add, data);
                    break;
                case ToolDefine.UIType.PlayerMp:
                    action(tbx_mp, data);
                    break;
                case ToolDefine.UIType.PlayerMpMax:
                    action(tbx_mpMax, data);
                    break;
                case ToolDefine.UIType.PlayerLevel:
                    action(tbx_Level, data);
                    break;
                case ToolDefine.UIType.PlayerExp:
                    pgb_Exp.Invoke((MethodInvoker)delegate
                    {
                        var percent = Math.Min(100, (int)LevelManager.Instance.GetExpPercent(Int32.Parse(data), Player.Instance.Level));
                        percent = Math.Max(0, percent);

                        pgb_Exp.CustomText = Player.Instance.Exp.ToString();
                        pgb_Exp.Value = percent;
                    });
                    break;
                case ToolDefine.UIType.PlayerActiveLevel:
                    action(tbx_ActiveLevel, data);
                    break;
                case ToolDefine.UIType.PlayerActiveExp:
                    pgb_ActiveExp.Invoke((MethodInvoker)delegate
                    {
                        var percent = Math.Min(100, (int)LevelManager.Instance.GetActiveExpPercent(Int32.Parse(data), Player.Instance.ActiveLevel));
                        pgb_ActiveExp.CustomText = Player.Instance.ActiveExp.ToString();
                        pgb_ActiveExp.Value = percent;
                    });
                    break;
                case ToolDefine.UIType.PlayerAtk:
                    action(tbx_atk, data);
                    break;
                case ToolDefine.UIType.PlayerAtkAdd:
                    action(tbx_atk_add, data);
                    break;
                case ToolDefine.UIType.PlayerAtkRate:
                    action(tbx_atk_rate, data);
                    break;
                case ToolDefine.UIType.PlayerAtkSpdRate:
                    action(tbx_atk_spd_rate, data);
                    break;
                case ToolDefine.UIType.PlayerVampireRate:
                    action(tbx_vampire_rate, data);
                    break;
                case ToolDefine.UIType.PlayerVigorRecoveryRate:
                    action(tbx_vigor_recovery_rate, data);
                    break;
                case ToolDefine.UIType.PlayerExpAddRate:
                    action(tbx_exp_add_rate, data);
                    break;
                case ToolDefine.UIType.DeckPoint:
                    action(tbx_deck_point, data);
                    break;
                case ToolDefine.UIType.Deck1:
                    action(tbx_deck1, data);
                    break;
                case ToolDefine.UIType.Deck2:
                    action(tbx_deck2, data);
                    break;
                case ToolDefine.UIType.Deck3:
                    action(tbx_deck3, data);
                    break;
                case ToolDefine.UIType.ActiveDeckList:
                    rtb_active_deck.Invoke((MethodInvoker)delegate {

                        if (0 == data.Length)
                        {
                            rtb_active_deck.Clear();
                            return;
                        }
                        rtb_active_deck.AppendText(data + Environment.NewLine);
                    });
                    break;

                case ToolDefine.UIType.Inventory:
                    rtb_item_list.Invoke((MethodInvoker)delegate {

                        if (0 == data.Length)
                        {
                            rtb_item_list.Clear();
                            return;
                        }

                        rtb_item_list.AppendText(data + Environment.NewLine);

                        //rtb_item_list.Text += "-";
                        //rtb_item_list.Text += "some logs and strings...";
                        //rtb_item_list.Text += "." + new string(' ', 1000) + Environment.NewLine;
                        //richTextBoxLog.Select(richTextBoxLog.GetFirstCharIndexFromLine(logCounter), richTextBoxLog.Lines[logCounter].Length);
                        //rtb_item_list.SelectionBackColor = (logCounter % 2 == 0) ? Color.LightBlue : Color.LightGray;
                        //logCounter++;
                        rtb_item_list.ScrollToCaret();

                    });
                    break;
                case ToolDefine.UIType.InventoryItemRemove:
                    rtb_item_list.Invoke((MethodInvoker)delegate {

                        string updateItems = string.Empty;

                        var items = rtb_item_list.Text.Split('\n');
                        foreach (var item in items)
                        {
                            if (0 < item.Length)
                            {
                                int idx = item.IndexOf(':');

                                int removeID = int.Parse(data);
                                int currentID = int.Parse(item.Substring(0, idx));

                                if (removeID != currentID)
                                {
                                    updateItems += (item + '\n');
                                }
                            }
                        }

                        //if (string.Empty != updateItems)
                        {
                            rtb_item_list.Clear();
                            rtb_item_list.AppendText(updateItems);
                        }
                    });
                    break;
                case ToolDefine.UIType.InventoryItemEquip:

                    rtb_item_list.Invoke((MethodInvoker)delegate {

                        string updateItems = string.Empty;

                        var items = rtb_item_list.Text.Split('\n');
                        foreach (var item in items)
                        {
                            if (0 < item.Length)
                            {
                                int idx = item.IndexOf(':');

                                int findID = int.Parse(data);
                                int currentID = int.Parse(item.Substring(0, idx));

                                if (findID == currentID)
                                {
                                    updateItems += (item + "[equip]" + '\n');
                                }
                                else
                                {
                                    updateItems += (item + '\n');
                                }
                            }
                        }

                        rtb_item_list.Clear();
                        rtb_item_list.AppendText(updateItems);
                    });
                    break;
                case ToolDefine.UIType.InventoryItemUnEquip:
                    rtb_item_list.Invoke((MethodInvoker)delegate {

                        string updateItems = string.Empty;

                        var items = rtb_item_list.Text.Split('\n');
                        foreach (var item in items)
                        {
                            if (0 < item.Length)
                            {
                                int idx = item.IndexOf(':');

                                int findID = int.Parse(data);
                                int currentID = int.Parse(item.Substring(0, idx));

                                if (findID == currentID)
                                {
                                    var rename = item;
                                    rename = rename.Replace("[equip]", "");
                                    updateItems += (rename + '\n');
                                }
                                else
                                {
                                    updateItems += (item + '\n');
                                }
                            }
                        }

                        rtb_item_list.Clear();
                        rtb_item_list.AppendText(updateItems);

                    });
                    break;
                case ToolDefine.UIType.TestCommand:
                    rtb_test_command.Invoke((MethodInvoker)delegate {

                        if (0 == data.Length)
                        {
                            rtb_test_command.Clear();
                            return;
                        }
                        rtb_test_command.AppendText(data);

                        ActiveControl = rtb_test_command;
                    });
                    break;
                case ToolDefine.UIType.PlayerClass:
                    actionWithColor(tbx_player_class, data, Color.Yellow, Color.Black);
                    break;
                case ToolDefine.UIType.PlayerName:
                    action(tbx_player_name, data);
                    break;
            }
        }
    }
}
