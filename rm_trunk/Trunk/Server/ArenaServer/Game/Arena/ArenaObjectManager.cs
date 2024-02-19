using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

using RM.Common;
using RM.Server.Common;

namespace ArenaServer.Game
{
    public class ArenaObjectManager
    {
        public bool Initialize()
        {
            m_objects = new ConcurrentDictionary<Guid, ArenaObject>();
            return true;
        }

        public bool TryAdd(ArenaObject arenaObject)
        {
            return m_objects.TryAdd(arenaObject.UID, arenaObject);
        }

        public bool ExistPlayer(Guid playerUID)
        {
            var allClear = m_objects.All(pair => 
            {
                var player = pair.Value as Player;
                if ((null != player) && (player.UID == playerUID))
                {
                    return false;
                }

                return true;
            });

            return !allClear;
        }

        public bool TryRemove(Guid instanceID)
        {
            ArenaObject arenaObject = null;
            return m_objects.TryRemove(instanceID, out arenaObject);
        }

        public ArenaObject FindObject(Guid uid)
        {
            ArenaObject arenaObject = null;
            if (true == m_objects.TryGetValue(uid, out arenaObject))
            {
                return arenaObject;
            }

            return null;
        }

        public List<ArenaObject> GetObjects()
        {
            var battleObjects = new List<ArenaObject>();

            m_objects.All(pair => {
                battleObjects.Add(pair.Value);
                return true;
            });

            return battleObjects;
        }

        public void Foreach(Action<ArenaObject> action)
        {
            foreach (var arenaObject in m_objects)
            {
                action(arenaObject.Value);
            }
        }

        public bool NoPlayer()
        {
            if (true == m_objects.IsEmpty)
            {
                return true;
            }

            return m_objects.All(pair =>
            {
                return (ArenaObjectType.Player != pair.Value.ArenaObjectType);
            });
        }

        public void Destory()
        {
            m_objects.All(pair => {

            //-player 는 leave 할때 destory 함미다.
                if (pair.Value.ArenaObjectType != ArenaObjectType.Player)
                {
                    pair.Value.Destory();
                }
                return true;
            });

            m_objects.Clear();
            //m_objects = null;
        }

        ConcurrentDictionary<Guid, ArenaObject> m_objects;// = new ConcurrentDictionary<Guid, ArenaObject>();
    };
}
