using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;
using RM.Net;
using ArenaServer.Net;
using ArenaServer.Game;
using ArenaServer.Resource;

using RM.Server.Common;

namespace ArenaServer.Game
{
    public partial class ArenaObject : RMCollision, ITimerUpdate
    {
        public ArenaObject(Guid uid)
        {
            m_UID = uid;
        }

        public virtual bool Initialize(int arenaObjectID, ArenaObjectType arenaObjectType, Vector2 position, float direction)
        {
            var resArenaObject = ResourceManager.Instance.FindArenaObject(arenaObjectID);
            if (null == resArenaObject)
            {
                return false;
            }

            m_arenaObjectID = arenaObjectID;
            m_arenaObjectType = arenaObjectType;

            m_collision = CollisionBase.CreateCollision(resArenaObject.CollisionID);
            if (null == m_collision)
            {
                return false;
            }

            m_movement = new Movement();
            m_movement.Position = new Vector2(position.x, position.y);
            m_movement.Direction = direction;

            m_status = new Status();
            m_status.Initialize();

            m_stat = new Stat();
            m_stat.Initialize();

            m_statusEffectManager = new StatusEffectManager();
            m_statusEffectManager.Initialize();

            m_deckManager = new DeckManager(this);
            m_deckManager.Initialize();

            m_updateTimer = new TimerUpdater<ArenaObject>(DEFINE.TIME_ARENAOBJECT_UPDATE_INTERVAL_MS, this);

            return true;
        }

        public virtual void Destory()
        {
            Console.WriteLine("Arena object[{0}] in arena[{1}], Destroyed completed.", ArenaObjectType.ToString(), m_arena.ID);

            m_arenaObjectID = 0;
            m_arenaObjectType = ArenaObjectType.None;

            m_collision = null;

            m_movement.Destory();
            //m_movement = null;//-testcode, 임시, player가 logout 후에 Auto skill 에서 Position 접근하면?!

            m_health.Destory();
            m_health = null;

            m_status.Destroy();
            //m_status = null;//-testcode, 임시, player가 logout 후에 ai tick에서 IsDead를 물어보면?!  적어도 최소한의 물어봄은 is dead를 통해서.

            m_stat.Destroy();
            m_stat = null;

            m_statusEffectManager.Destroy();
            m_statusEffectManager = null;

            m_deckManager.Destroy();
            m_deckManager = null;

            m_updateTimer.Destroy();
            m_updateTimer = null;

            //m_arena = null; -jinsub, Destory 시켰지만, 타이머큐에 있던 N시간 이후에 스킬이 발동되면, m_area 가 참조될 수 있음.
        }

        public virtual ARENA_OBJECT_DATA SerializedObjectData()
        {
            ARENA_OBJECT_DATA data = new ARENA_OBJECT_DATA();
            data.UID = UID;
            data.ArenaObjectID = ArenaObjectID;
            data.Movement = Movement.SerializeData();
            data.Health = Health.SerializeData();
            data.Status = m_status.SerializeData();
            data.Stat = m_stat.SerializeData();

            return data;
        }

        public CollisionBase GetCollision()
        {
            if (null != m_collision)
            {
                m_collision.Position = this.Position;
                m_collision.Direction = this.Direction;
            }
            return m_collision;
        }

        public virtual bool Enter(Arena arena)
        {
            m_arena = arena;

            m_arena.AddObject(this);
            EnterNtf();
            return true;
        }

        public virtual void EnterNtf()
        {
            ARENA_ENTER_OBJECT_NTF packet = new ARENA_ENTER_OBJECT_NTF();
            packet.ArenaObjectData = SerializedObjectData();

            Arena.BroadCasting(packet, UID);
        }

        public virtual void Leave()
        {
            Arena.ObjectList.Foreach(arenaObject => {

                //if (arenaObject.UID != UID)//-testcode, 툴에서 내가 나간걸 알아야 툴의 오브젝트를 모두 지울수 있다,
                {
                    arenaObject.LeaveNtf(this);
                }
            });

            if (true == m_arena.RemoveObject(UID))
            {
                Destory();
            }
        }

        public virtual void LeaveNtf(ArenaObject destObject)
        {
        }

        public virtual void OnDie()
        {
        }

        public virtual bool Move(Vector2 targetPos)
        {
            return false;
        }

        public virtual void MoveNtf(HERO_MOVEMENT_NTF movement)
        {
            Position = new Vector2(movement.Position.X, movement.Position.Z);
            Direction = movement.Direction;
            m_arena.BroadCasting(movement);
        }

        public virtual void SendPacket(RMPacket packet)
        {
        }

        public virtual void ForceSendPacket(RMPacket packet)
        {
        }

        public int ArenaID { get => m_arena.ID; }

        public Arena Arena { get => m_arena; }

        public Guid UID { get => m_UID; }

        public int ArenaObjectID { get => m_arenaObjectID; }


        public ArenaObjectType ArenaObjectType
        {
            get         
            { 
                return m_arenaObjectType; 
            }
            private set 
            { 
                m_arenaObjectType = value; 
            }
        }

        public virtual float Sight 
        {
            get
            {
                return 0.0f;
            }
        }

        public virtual float ObjectRadius
        {
            get
            {
                return 0.0f;
            }
        }

        public virtual UnityEngine.Vector2 Position
        {
            get 
            { 
                return m_movement.Position; 
            }
            set
            {
                m_movement.Position = value;
            }
        }

        public bool IsDead()
        {
            return Status.Have(StatusType.Dead);
        }

        public float Direction
        {
            get => m_movement.Direction;
            set => m_movement.Direction = value;
        }
        public Movement Movement => m_movement;

        public Health Health => m_health;

        public Status Status => m_status;

        public Stat Stat => m_stat;

        public StatusEffectManager StatusEffect => m_statusEffectManager;

        public DeckManager Deck => m_deckManager;

        private readonly Guid m_UID;
        private volatile int m_arenaObjectID = 0;
        private volatile ArenaObjectType m_arenaObjectType = ArenaObjectType.None;
        
        protected CollisionBase m_collision = null;
        protected Movement m_movement = null;
        protected Health m_health = null;
        protected Status m_status = null;
        protected Stat m_stat = null;
        protected StatusEffectManager m_statusEffectManager = null;
        protected DeckManager m_deckManager = null;

        protected Arena m_arena;
        
        protected TimerUpdater<ArenaObject> m_updateTimer = null;
    }
}
