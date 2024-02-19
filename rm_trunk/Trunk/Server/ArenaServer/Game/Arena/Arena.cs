using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Net;
using ArenaServer.Resource;
using ArenaServer.Util;

using RM.Common;
using RM.Server.Common;

namespace ArenaServer.Game
{
    public partial class Arena : ITimerUpdate
    {
        public Arena(int id, Guid key)
        {
            ID = id;
            Key = key;
        }

        public bool Initialize(int mapID)
        {
            EmptyTime = System.DateTime.UtcNow;

            m_map = new ArenaMap();
            if (false == m_map.Initialize(mapID, this))
            {
                return false;
            }

            m_updateTimer = new TimerUpdater<Arena>(DEFINE.TIME_ARENA_UPDATE_INTERVAL_MS, this);
            m_updateTimer.Submit(m_updateTimer.GetTimerInterval, (int)TimerDispatcherIDType.ArenaUpdate);

            m_objectManager = new ArenaObjectManager();
            m_objectManager.Initialize();

            m_projectiles = new ConcurrentDictionary<Guid, ProjectileBase>();

            return true;
        }

        public void Destory()
        {
            m_updateTimer.Destroy();
            m_updateTimer = null;

            m_objectManager.Destory();
            //m_objectManager = null; -jinsub null 처리하면, 아직 남아있는 타이머 큐에서 호출 될 수 있음. 

            m_projectiles.Clear();
            m_projectiles = null;
        }

        public bool Deploy(ResData_DeployArenaObject deployArenaObject)
        {
            var createObject = NpcFactory.Instance.Create(deployArenaObject, this);
            if (null != createObject)
            {
                return createObject.Enter(this);
            }

            return false;
        }

        public bool ForceDeploy(int arenaObjectID, float x, float y, float d)
        {
            Npc npc = new Npc(Guid.NewGuid());
            if (true == npc.Initialize(arenaObjectID, new UnityEngine.Vector2(x, y), d, this))
            {
                npc.Enter(this);
            }

            return false;
        }

        public void AddObject(ArenaObject arenaObject)// jinsub, 이거를 이렇게 2중으로 감쌀 필요가 없는데, 
        {
            m_objectManager.TryAdd(arenaObject);
        }

        public bool RemoveObject(Guid uid) // jinsub, 이거를 이렇게 2중으로 감쌀 필요가 없는데, 
        {
            var ret = m_objectManager.TryRemove(uid);
            if (true == ret)
            {
                if (true == m_objectManager.NoPlayer())
                {
                    EmptyTime = System.DateTime.UtcNow;
                }

                return true;
            }

            return false;
        }

        public void BroadCasting(RMPacket packet, Guid? excludeUID = null)
        {
            ObjectList.Foreach(arenaObject => {

                if ((null != excludeUID) && (arenaObject.UID == excludeUID)) {
                }
                else
                {
                    arenaObject.SendPacket(packet);
                }

            });
        }

        public bool ExistPlayer(Guid playerUID)
        {
            return m_objectManager.ExistPlayer(playerUID);
        }

        public ArenaObjectManager ObjectList
        {
            get => m_objectManager;
        }

        public bool IsEmpty(DateTime now)
        {
            if (((now - EmptyTime).TotalMilliseconds > (DEFINE.TIME_WAIT_EMPTY_ARENA_S * 1000)) && m_objectManager.NoPlayer())
            {
                return true;
            }

            return false;
        }

        public System.DateTime EmptyTime
        {
            get
            {
                System.DateTime dateTime;
                lock (m_emptyTimeLock)
                {
                    dateTime = m_emptyTime;
                    return dateTime;
                }
            }
            set
            {
                lock (m_emptyTimeLock)
                {
                    m_emptyTime = value;
                }
            }
        }

        public int MapID
        {
            get
            {
                return m_map.ID;
            }
        }

        public ArenaMap Map { get => m_map; }

        public GridMap GridMap 
        {
            get 
            {
                return Map.GridMap;
            }
        }

        //-아레나가 비어있는 시간
        object m_emptyTimeLock = new object();
        System.DateTime m_emptyTime;

        public readonly Guid Key;
        public readonly int ID;

        ArenaMap m_map;

        TimerUpdater<Arena> m_updateTimer = null;
        ArenaObjectManager m_objectManager = null;
    }
}
