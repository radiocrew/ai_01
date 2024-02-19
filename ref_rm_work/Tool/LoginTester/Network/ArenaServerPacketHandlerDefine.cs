using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using RM.Net;
using RM.Common;

using rm_login.Tool;
using rm_login.Tool.Script;

namespace rm_login.Network
{
    public partial class ArenaServerPacketHandler
    {
        static public void ReceivedLog(RMPacket packet)
        {
            LogDelegate.Instance.Log(string.Format("[>] {0}", packet.GetType().Name.ToString()));
        }

        public void ArenaHelloNtf(ARENA_HELLO_NTF packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] hello ntf"));
            FormDelegate.Instance.ToUI(ToolDefine.UIType.TestCommand, "you can input test command here." + "\n");
        }

        public void ArenaLoginAck(ARENA_LOGIN_ACK packet)
        {
            if (packet.Result != PacketResult.Success)
            {
                LogDelegate.Instance.Log(string.Format("[<] login ack failed : {0}", packet.Result.ToString()));
                return;
            }

            LogDelegate.Instance.Log(string.Format("[<] login ack success"));
            Player.Instance.ArenaKey = packet.ArenaKey.ToString();
        }

        public void ArenaMoveAck(ARENA_MOVE_ACK packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] arena move ack result[{0}]", packet.Result.ToString()));
        }

        public void ArenaMatchingAck(ARENA_MATCHING_ACK packet)
        {
            if (PacketResult.Success == packet.Result)
            {
                FormDelegate.Instance.ToUI(ToolDefine.UIType.MatchingAck, string.Empty);
            }

            LogDelegate.Instance.Log(string.Format("[<] matching ack result[{0}]", packet.Result.ToString()));
        }

        public void ArenaMatchingCancel(ARENA_MATCHING_CANCEL packet)
        {
            //ArenaObjectManager.Instance.Draw();
            LogDelegate.Instance.Log(string.Format("[<] matching cancel"));

            
        }

        public void ArenaMatchingCompleteNtf(ARENA_MATCHING_COMPLETE_NTF packet)
        {
            //ArenaObjectManager.Instance.Draw();
            LogDelegate.Instance.Log(string.Format("[<] matching complted!!!"));

            FormDelegate.Instance.ToUI(ToolDefine.UIType.MatchingTimerStop, string.Empty);

            //-이동 시작!!!
            MATCHED_ARENA_MOVE_REQ req = new MATCHED_ARENA_MOVE_REQ();
            req.ArenaKey = packet.Arrival.ArenaKey;
            req.playerUID = Player.Instance.UID;
            ArenaServerSession.Instance.SendPacket(req);
        }

        public void MatchedArenaMoveAck(MATCHED_ARENA_MOVE_ACK packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] raid arena move ack!!! result[{0}]", packet.Result.ToString()));
        }

        public void ArenaEnterPlayerNtf(ARENA_ENTER_PLAYER_NTF packet)
        {
            ResJson_Map resMap = null;
            if (false == ResourceManager.Instance.JsonMap.TryGetValue(packet.MapID, out resMap))
            {
                Debug.Assert(false);
                return;
            }

            MapManager.Instance.Reset(packet.MapID);
            FormDelegate.Instance.UIClear();

            //FormDelegate.Instance.ToUI(ToolDefine.UIType.TestCommand, "you can input test command here." + "\n");

            
            //ActiveControl = rtb_test_command;

            //if (Player.Instance.UID == packet.HeroData.UID)
            //{
            //    /*
            //     *-testcode, jinsub 버그 있음.
            //     *  server에서 npc deploy가 먼저 오면?! npc 지우네... 
            //     *
            //     */ 

            //    ArenaObjectManager.Instance.Clear();
            //}


            LogDelegate.Instance.Log(string.Format("[<] enter player[{0}] ntf.", LogDelegate.ToSimpleUID(packet.HeroData.UID)));

            var player = ResourceManager.Instance.FindPlayer(packet.HeroData.ArenaObjectID);
            Player.Instance.ClassType = player.PlayerClassType;
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerClass, Player.Instance.ClassType.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerName, "noname");

            Player.Instance.HP = packet.HeroData.Health.Hp;
            Player.Instance.HPMax = packet.HeroData.Health.HpMax;
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHp, packet.HeroData.Health.Hp.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHpMax, packet.HeroData.Health.HpMax.ToString());

            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerMp, packet.HeroData.Health.Mp.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerMpMax, packet.HeroData.Health.MpMax.ToString());

            Player.Instance.Level = packet.HeroData.Health.Level;
            Player.Instance.Exp = packet.HeroData.Health.Exp;
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerLevel, Player.Instance.Level.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerExp, Player.Instance.Exp.ToString());

            FormDelegate.Instance.ToUI(ToolDefine.UIType.AccountUIDTextBox, packet.HeroData.UID.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerUIDTextBox, packet.HeroData.UID.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.ArenaIDTextBox, packet.ArenaID.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.MapInfoTextBox, string.Format("map id:{0},w:{1},h:{2}", packet.MapID, resMap.Size.Width, resMap.Size.Height));

            Player.Instance.ActiveLevel = packet.HeroData.ActiveLevel.Level;
            Player.Instance.ActiveExp = packet.HeroData.ActiveLevel.Exp;
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerActiveLevel, Player.Instance.ActiveLevel.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerActiveExp, Player.Instance.ActiveExp.ToString());

            Player.Instance.Stat.AddOrUpdate(packet.HeroData.Stat.Stat);

            ArenaObjectManager.Instance.Enter(packet.HeroData.UID, ToolDefine.ObjectType.Hero, packet.HeroData.Movement.Position.X, packet.HeroData.Movement.Position.Z);
            DrawManager.Instance.Regist(packet.HeroData.UID,
                ToolDefine.ObjectType.Hero,
                packet.HeroData.Movement.Position.X,
                packet.HeroData.Movement.Position.Z,
                packet.HeroData.Movement.Direction,
                DPlayer.PLAYER_SIGHT,
                DPlayer.PLAYER_ATTACK_RANGE);

            if (null != packet.ArenaObjectDataList)//-element 가 없으면  List는 null 이지,
            {
                foreach (var data in packet.ArenaObjectDataList)
                {
                    LogDelegate.Instance.Log(string.Format("    other object id[{0}][{1}]", data.ArenaObjectID, LogDelegate.ToSimpleUID(data.UID)));

                    ArenaObjectManager.Instance.Enter(data.UID, ToolDefine.ObjectType.Npc, (int)data.Movement.Position.X, (int)data.Movement.Position.Z);
                    var resNpc = ResourceManager.Instance.FindNpc(data.ArenaObjectID);
                    DrawManager.Instance.Regist(data.UID, 
                        (null != resNpc) ? ToolDefine.ObjectType.Npc : ToolDefine.ObjectType.Player, 
                        data.Movement.Position.X, 
                        data.Movement.Position.Z, 
                        data.Movement.Direction, 
                        (null != resNpc) ? resNpc.Sight : DPlayer.PLAYER_SIGHT,
                        ToolDefine.TEST_OBJECT_ATTACK_RANGE);
                }
            }

            //ArenaObjectManager.Instance.Draw();
        }

        public void ArenaEnterObjectNtf(ARENA_ENTER_OBJECT_NTF packet)
        {
            var x = packet.ArenaObjectData.Movement.Position.X;
            var y = packet.ArenaObjectData.Movement.Position.Y;
            var z = packet.ArenaObjectData.Movement.Position.Z;
            var d = packet.ArenaObjectData.Movement.Direction;

            LogDelegate.Instance.Log(string.Format("[<] enter object ID[{0}][{1}], x[{2}]y[{3}]", 
                packet.ArenaObjectData.ArenaObjectID,
                LogDelegate.ToSimpleUID(packet.ArenaObjectData.UID),
                x,
                z
                ));

            ArenaObjectManager.Instance.Enter(packet.ArenaObjectData.UID, ToolDefine.ObjectType.Npc, (int)x, (int)z);

            var resNpc = ResourceManager.Instance.FindNpc(packet.ArenaObjectData.ArenaObjectID);
            DrawManager.Instance.Regist(packet.ArenaObjectData.UID,
                (null != resNpc) ? ToolDefine.ObjectType.Npc : ToolDefine.ObjectType.Player,
                x,
                z,
                d,
                (null != resNpc) ? resNpc.Sight : DPlayer.PLAYER_SIGHT,
                ToolDefine.TEST_OBJECT_ATTACK_RANGE);
        }

        public void ArenaLeaveObjectNtf(ARENA_LEAVE_OBJECT_NTF packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] leave some object ID[{0}]", LogDelegate.ToSimpleUID(packet.UID)));

            ArenaObjectManager.Instance.Leave(packet.UID);
            DrawManager.Instance.UnRegist(packet.UID);

            if (Player.Instance.UID == packet.UID)
            {
                ArenaObjectManager.Instance.Clear();
                DrawManager.Instance.Clear();
            }
        }

        public void MovementNtf(HERO_MOVEMENT_NTF packet)
        {
            var x = packet.Position.X;
            var y = packet.Position.Y;
            var z = packet.Position.Z;
            var dir = packet.Direction;

            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerX, x.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerZ, z.ToString());

            LogDelegate.Instance.Log(string.Format("[<] move ntf UID[{0}] x[{1}], z[{2}], dir[{3}]", LogDelegate.ToSimpleUID(packet.UID), x, z, dir));
            ArenaObjectManager.Instance.Move(packet.UID, x, z, dir);

            DrawManager.Instance.Update(packet.UID, x, z, dir);

            //ArenaObjectManager.Instance.Draw();
        }

        public void CastSkillNtf(CAST_SKILL_NTF packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] cast skill noti UID id[{0}]", LogDelegate.ToSimpleUID(packet.UID)));
        }

        public void CastSkillProjectileNtf(CAST_SKILL_PROJECTILE_NTF packet)
        {
            if (Player.Instance.UID != packet.OwnerUID)
            {
                Debug.Assert(false);
                return;
            }

            var baseDraw = DrawManager.Instance.Regist(packet.ProjectileData.UID, ToolDefine.ObjectType.Projectile,
                packet.MovementData.Position.X,
                packet.MovementData.Position.Z,
                packet.ProjectileData.Direction,
                0.0f,
                0.0f);

            var projectile = baseDraw as DProjectile;
            if (null != projectile)
            {
                if (true == projectile.LoadJson(packet.ProjectileData.ProjectileID))
                {
                    projectile.Fire();
                }
            }
        }

        public void InvokiSkillNtf(INVOKE_SKILL_NTF packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] invoke skill[{0}] noti UID id[{1}]", packet.SkillID, LogDelegate.ToSimpleUID(packet.UID)));
        }

        public void DamageNtf(DAMAGE_NTF packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] damage noti UID[{0}] dmg[{1}] hp[{2}] skill[{3}]",  
                LogDelegate.ToSimpleUID(packet.UID), packet.Damage, packet.HealthData.Hp, packet.SkillID), System.Drawing.Color.Red);

            if (Player.Instance.UID == packet.UID)
            {
                Player.Instance.HP = packet.HealthData.Hp;
                Player.Instance.HPMax = packet.HealthData.HpMax;

                FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHp, packet.HealthData.Hp.ToString());
                FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHpMax, packet.HealthData.HpMax.ToString());
            }
        }

        public void NpcMovementNtf(NPC_MOVEMENT_NTF packet)
        {
            var x = packet.StartPos.X;
            var z = packet.StartPos.Z;
            var d = packet.Direction;
            //-hard codin'
            if (1.0f >= Math.Abs(packet.StartPos.X - packet.EndPos.X))
            {
                x = packet.EndPos.X;
            }
            //-hard codin'
            if (1.0f >= Math.Abs(packet.StartPos.Z - packet.EndPos.Z))
            {
                z = packet.EndPos.Z;
            }

            //LogDelegate.Instance.Log(string.Format("start[{0}][{1}] end[{2}][{3}]", 
            //    packet.StartPos.X,
            //    packet.StartPos.Z,
            //    packet.EndPos.X,
            //    packet.EndPos.Z));

            ArenaObjectManager.Instance.Move(packet.UID, x, z, d);
            DrawManager.Instance.Update(packet.UID, x, z, d);
            //ArenaObjectManager.Instance.Draw();
        }

        public void StatusNtf(STATUS_NTF packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] STATUS_NTF"));

            if (null != packet.StatusData.Status)
            {
                foreach (var status in packet.StatusData.Status)
                {
                    LogDelegate.Instance.Log(string.Format("    UID[{0}] changed status[{1}]", LogDelegate.ToSimpleUID(packet.UID), status.ToString()));
                }
            }
        }

        public void HeroReviveAck(HERO_REVIVE_ACK packet)
        {
            LogDelegate.Instance.Log(string.Format("[<] HERO_REVIVE_ACK"));
        }

        public void HealthNtf(HEALTH_NTF packet)
        {
            Player.Instance.Level = packet.HealthData.Level;
            Player.Instance.Exp = packet.HealthData.Exp;

            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHp, packet.HealthData.Hp.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerHpMax, packet.HealthData.HpMax.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerMp, packet.HealthData.Mp.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerMpMax, packet.HealthData.MpMax.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerLevel, Player.Instance.Level.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerExp, Player.Instance.Exp.ToString());
        }

        public void ActiveLevelNtf(ACTIVE_LEVEL_NTF packet)
        {
            Player.Instance.ActiveLevel = packet.ActiveLevelData.Level;
            Player.Instance.ActiveExp = packet.ActiveLevelData.Exp;

            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerActiveLevel, Player.Instance.ActiveLevel.ToString());
            FormDelegate.Instance.ToUI(ToolDefine.UIType.PlayerActiveExp, Player.Instance.ActiveExp.ToString());
        }

        public void StatNtf(STAT_NTF packet)
        {
            if (Player.Instance.UID == packet.UID)
            {
                Player.Instance.Stat.AddOrUpdate(packet.StatData.Stat);
                return;
            }

            Debug.Assert(false);
        }

        public void DeckNtf(DECK_NTF packet)
        {
            if (Player.Instance.UID != packet.UID)
            {
                Debug.Assert(false);
                return;
            }

            int deckPoint = packet.DeckData.DeckGenPoint;

            FormDelegate.Instance.ToUI(ToolDefine.UIType.DeckPoint, deckPoint.ToString());
            Player.Instance.DeckPoint = deckPoint;

            if (null != packet.DeckData.SelectableDeck)
            {
                int deck1 = packet.DeckData.SelectableDeck.ElementAt(0);
                int deck2 = packet.DeckData.SelectableDeck.ElementAt(1);
                int deck3 = packet.DeckData.SelectableDeck.ElementAt(2);

                Player.Instance.SelectableDeckList.Clear();

                FormDelegate.Instance.ToUI(ToolDefine.UIType.Deck1, deck1.ToString());
                Player.Instance.SelectableDeckList.Add(deck1);

                FormDelegate.Instance.ToUI(ToolDefine.UIType.Deck2, deck2.ToString());
                Player.Instance.SelectableDeckList.Add(deck2);

                FormDelegate.Instance.ToUI(ToolDefine.UIType.Deck3, deck3.ToString());
                Player.Instance.SelectableDeckList.Add(deck3);
            }

            FormDelegate.Instance.ToUI(ToolDefine.UIType.ActiveDeckList, string.Empty);

            if (null != packet.DeckData.ActiveDeck)
            {
                packet.DeckData.ActiveDeck.All(pair => {

                    FormDelegate.Instance.ToUI(ToolDefine.UIType.ActiveDeckList, string.Format("Deck id[{0}], point[{1}]", pair.Value.DeckID, pair.Value.Point));
                    return true;
                });
            }
        }

        public void InventoryAck(INVENTORY_ACK packet)
        {
        }

        public void InventoryItemUpdate(INVENTORY_ITEM_UPDATE packet)
        {
            Item item = null;
            switch (packet.UpdateType)
            {
                case INVENTORY_ITEM_UPDATE.ItemUpdateType.Created:
                    item = Player.Instance.Inventory.Add(packet.ItemData.ItemID, packet.ItemData.UID);
                    if (null != item)
                    {
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.Inventory, string.Format("{0}:ItemID[{1}]", item.UIID, item.ID));
                    }
                    break;
                case INVENTORY_ITEM_UPDATE.ItemUpdateType.Deleted:
                    item = Player.Instance.Inventory.FindItem(packet.ItemData.UID);
                    if (null != item)
                    {
                        Player.Instance.Inventory.Remove(packet.ItemData.UID);
                        FormDelegate.Instance.ToUI(ToolDefine.UIType.InventoryItemRemove, item.UIID.ToString());
                    }
                    break;
                case INVENTORY_ITEM_UPDATE.ItemUpdateType.Updated:
                    break;
            }
        }

        public void InventoryItemDelete(INVENTORY_ITEM_DELETE_ACK packet)
        {

        }

        public void ItemEquip(ITEM_EQUIP packet)
        {
            if (true == packet.Result)
            {
                var item = Player.Instance.Inventory.FindItem(packet.UID);
                if (null == item)
                {
                    Debug.Assert(false);
                    return;
                }

                item.Equipped = packet.IsEquipOrNot;

                if (true == packet.IsEquipOrNot)
                {
                    FormDelegate.Instance.ToUI(ToolDefine.UIType.InventoryItemEquip, item.UIID.ToString());
                }
                else
                {
                    FormDelegate.Instance.ToUI(ToolDefine.UIType.InventoryItemUnEquip, item.UIID.ToString());
                }

                return;
            }

            var str = (true == packet.IsEquipOrNot) ? ("equip") : ("unequip");
            LogDelegate.Instance.Log("Error, item " + str + " failed", System.Drawing.Color.Red);
        }

        public void ProjectileRemoveNtf(PROJECTILE_REMOVE_NTF packet)
        {
            if (true == packet.Hit)
            {
                DrawManager.Instance.UnRegist(packet.UID);
                LogDelegate.Instance.Log(string.Format("We Hit !!! pos x[{0}], y[{1}]", packet.HitPosition.X, packet.HitPosition.Z));
            }
        }
    }
}
