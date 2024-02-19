using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Drawing;

using RM.Common;
using RM.Net;
using rm_login.Network;

namespace rm_login.Tool
{
    public partial class Player
    {
        public void TestCommand(TestCommandType type, int int0 = 0, int int1 = 0, float float0 = 0.0f, float float1 = 0.0f)
        {
            TEST_COMMAND packet = new TEST_COMMAND();
            packet.TestCommandType = type;
            packet.Int_0 = int0;
            packet.Int_1 = int1;
            packet.Float_0 = float0;
            packet.Float_1 = float1;

            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void Login(int playerObjectID)
        {
            var login = new ARENA_LOGIN_REQ();
            login.PlayerUID = UID;
            login.TestPlayerObjectID = playerObjectID;//
            ArenaServerSession.Instance.SendPacket(login);
        }

        public void MoveNtf(float x, float y, float z, float d)
        {
            var move = new HERO_MOVEMENT_NTF();
            move.UID = Player.Instance.UID;
            move.Position = new NVECTOR3(Convert.ToSingle(x), 0, Convert.ToSingle(z));
            move.Direction = Convert.ToSingle(d);
            ArenaServerSession.Instance.SendPacket(move);
        }

        public void StartAttack()
        {
            var start = new START_ATTACK();
            start.UID = UID;
            ArenaServerSession.Instance.SendPacket(start);
        }

        public void StopAttack()
        {
            var stop = new STOP_ATTACK();
            stop.UID = UID;
            ArenaServerSession.Instance.SendPacket(stop);
        }

        public void CastSkill()
        {
            var skill = new CAST_SKILL();

            switch (Player.Instance.ClassType)
            {
                case PlayerClassType.Rogue:
                    skill.SkillID = ToolDefine.TEST_CAST_ATTACK_SKILL_ID;
                    break;
                case PlayerClassType.Mage:
                    skill.SkillID = ToolDefine.TEST_CAST_PROJECTILE_SKILL_ID;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        
            skill.SkillParamData = new SKILL_PARAM_DATA();
            ArenaServerSession.Instance.SendPacket(skill);
        }

        public void ArenaMove()
        {
            ARENA_MOVE_REQ packet = new ARENA_MOVE_REQ();
            packet.ArenaID = SingleArenaID;
            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void RequetMatching()
        {
            ARENA_MATCHING_REQ packet = new ARENA_MATCHING_REQ();
            packet.ArenaID = Player.Instance.RaidArenaID;
            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void CancelMatching()
        {
            ARENA_MATCHING_CANCEL packet = new ARENA_MATCHING_CANCEL();
            packet.PlayerUID = UID;
            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void RequestRevive()
        {
            HERO_REVIVE_REQ packet = new HERO_REVIVE_REQ();
            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void RequestDeckGen()
        {
            if (0 >= DeckPoint)
            {
                LogDelegate.Instance.Log("We need deck point...", Color.Red);
                return;
            }

            DECK_GEN_REQ packet = new DECK_GEN_REQ();
            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void SelectDeck(int no)
        {
            if ((0 < SelectableDeckList.Count) && (no < SelectableDeckList.Count))
            {
                DECK_SELECT_REQ packet = new DECK_SELECT_REQ();
                packet.DeckID = SelectableDeckList[no];

                ArenaServerSession.Instance.SendPacket(packet);

            //-deck 하나 선택했지만 아직 포인트 여분이 있으니, 한번 더 gen 을 해주세요.
                if (1 < DeckPoint)
                {
                    RequestDeckGen();
                }
                   
                return;
            }
        }

        public void InventoryReq()
        {
            INVENTORY_REQ packet = new INVENTORY_REQ();
            ArenaServerSession.Instance.SendPacket(packet);
        }

        public void ItemEquip()
        {
            //ITEM_EQUIP packet = new ITEM_EQUIP();

            //Player.Instance.Inventory


        }



    }
}
