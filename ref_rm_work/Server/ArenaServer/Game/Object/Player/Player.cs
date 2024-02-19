using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//using SuperSocket.SocketBase;

using UnityEngine;

using RM.Common;
using RM.Net;
using RM.Server.Common;

using ArenaServer.Net;
using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public partial class Player : ArenaObject
    {
        public Player(Guid playerUID)
            : base(playerUID)
        {
        }

        public bool Initialize( Vector2 position, float direction, int dataFromDB)
        {
            if (false == base.Initialize(dataFromDB, RM.Common.ArenaObjectType.Player, position, direction))
            {
                Console.WriteLine("Error!, player initialize failed.");
                return false;
            }

            var resPlayer = ResourceManager.Instance.FindPlayer(ArenaObjectID);
            if (null == resPlayer)
            {
                return false;
            }

            HEALTH_DATA healthData = new HEALTH_DATA();
            healthData.Hp = resPlayer.Health.Hp;
            healthData.HpMax = resPlayer.Health.HpMax;
            healthData.Level = DEFINE.TEST_PLAYER_LEVEL;
            healthData.Exp = DEFINE.TEST_PLAYER_EXP;
            m_health = new Health();
            m_health.Initialize(healthData);

            m_heartBeat = new HeartBeat();
            m_heartBeat.Initialize(this);

            m_autoSkill = new AutoSkill();
            m_autoSkill.Initialize(this);

            m_activeLevel = new ActiveLevel();
            m_activeLevel.Initialize();

            m_inventory = new Inventory();
            m_inventory.Initialize();

            m_itemEquipManager = new ItemEquipManager();
            m_itemEquipManager.Initialize();

            m_playerClassType = resPlayer.PlayerClassType;

            m_stat.Calculate(this, false);

            m_updateTimer.Submit(m_updateTimer.GetTimerInterval, (int)TimerDispatcherIDType.ArenaObjectUpdate);
            return true;
        }

        public void Reset(int arenaObjectID)
        {
            var position= PlayerTestCode.Position(new Vector2(0, 0), 10, 10);
            var direction = PlayerTestCode.Direction();

            Initialize(position, direction, arenaObjectID);
        }

        public override void Destory()
        {
            base.Destory();
   
            m_heartBeat.Destroy();
            m_heartBeat = null;
            m_autoSkill.Destory();
            m_autoSkill = null;
            m_activeLevel.Destroy();
            m_activeLevel = null;
            m_inventory.Destroy();
            m_inventory = null;
            m_itemEquipManager.Destroy();
            m_itemEquipManager = null;
        }

        public void BindSession(PlayerSession session)
        {
            lock (m_sessionLock)
            {
                if (null != m_session)
                {
                    m_session.Close();
                }

                m_session = session;
            }
        }

        public HERO_DATA SerializedHeroData()
        {
            HERO_DATA data = new HERO_DATA();
            data.ArenaObjectID = ArenaObjectID;
            data.UID = UID;
            data.Health = Health.SerializeData();
            data.Movement = Movement.SerializeData();
            data.ActiveLevel = ActiveLevel.SerializeData();
            data.Stat = Stat.SerializeData();
            data.Status = Status.SerializeData();

            return data;
        }

        public override bool Enter(Arena arena)
        {
            base.Enter(arena);

            return true;
        }

        public override void EnterNtf()
        {
            ARENA_ENTER_PLAYER_NTF packet = new ARENA_ENTER_PLAYER_NTF();
            packet.ArenaID = m_arena.ID;
            packet.MapID = m_arena.MapID;

            packet.HeroData = new HERO_DATA();
            packet.HeroData = SerializedHeroData();

            packet.ArenaObjectIDs = new List<int>();
            packet.ArenaObjectDataList = new List<ARENA_OBJECT_DATA>();

            //foreach (var pair in m_arena.ObjectList.Where(pair => pair.Value.UID != this.UID))
            //{
            //    packet.ArenaObjectIDs.Add(pair.Value.ArenaObjectID);
            //    packet.ArenaObjectDataList.Add(pair.Value.SerializedObjectData());
            //}
            //SendPacket(packet);

            Arena.ObjectList.Foreach(arenaObject => {

                if (arenaObject.UID != UID)
                {
                    packet.ArenaObjectIDs.Add(arenaObject.ArenaObjectID);
                    packet.ArenaObjectDataList.Add(arenaObject.SerializedObjectData());
                }
            });
            SendPacket(packet);

            base.EnterNtf();
        }

        public override void Leave()
        {
            base.Leave();
        }

        public override void LeaveNtf(ArenaObject destObject)
        {
            ARENA_LEAVE_OBJECT_NTF packet = new ARENA_LEAVE_OBJECT_NTF();
            packet.UID = destObject.UID;
            SendPacket(packet);
        }

        public void OnHeartbeat(int id, uint count)
        {
            if (true == HeartBeat.Stop)
            {
                return;
            }

            if (false == HeartBeat.Pulse())
            {
                ForceRemove();
                return;
            }

            //-todo. what you think, what you need and what you have to do
            //Console.WriteLine("[{0}] listen to my heart beat ? arena id[{1}]", System.DateTime.Now.ToString("hh:mm:ss.fff"), m_arena.ID);
        }

        public void ForceRemove()
        {
            Remove();
            Disconnect();
        }

        private void Remove()
        {
            HeartBeat.Stop = true;

            DelayedTask dt = new DelayedTask(() => {

                var playerUID = UID;
                var arenaMember = ArenaMemberManager.Instance.GetArenaMember(playerUID);
                if (null != arenaMember)
                {
                    arenaMember.LeaveArena();
                    ArenaMemberManager.Instance.Remove(playerUID);
                }
            });
            dt.SubmitImmediately((int)TimerDispatcherIDType.PlayerRemove);
        }

        public override float ObjectRadius
        {
            get
            {
                var collisionType = m_collision as CollisionCircle;
                if (null != collisionType)
                {
                    return collisionType.Radius;
                }

                return 0.0f;
            }
        }

        public PlayerClassType PlayerClassType
        {
            get
            {
                return m_playerClassType;
            }
            private set
            {
                m_playerClassType = value;
            }
        }

        public AutoSkill AutoSkill => m_autoSkill;

        public HeartBeat HeartBeat => m_heartBeat;

        public ActiveLevel ActiveLevel => m_activeLevel;

        public Inventory Inventory => m_inventory;

        public ItemEquipManager ItemEquipManager => m_itemEquipManager;

        object m_sessionLock = new object();
        PlayerSession m_session = null;

        HeartBeat m_heartBeat = null;
        AutoSkill m_autoSkill = null;
        ActiveLevel m_activeLevel = null;
        Inventory m_inventory = null;
        ItemEquipManager m_itemEquipManager = null;

        private volatile PlayerClassType m_playerClassType = PlayerClassType.None;
    }
}
