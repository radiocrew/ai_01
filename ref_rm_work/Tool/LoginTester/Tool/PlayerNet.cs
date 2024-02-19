using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Common;
using RM.Net;
using rm_login.Network;

namespace rm_login.Tool
{
    public partial class Player
    {
        public void Login()
        {
            var login = new ARENA_LOGIN_REQ();
            login.PlayerUID = UID;
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
    }
}
