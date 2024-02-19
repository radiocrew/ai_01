using System;
using System.Collections.Generic;

using RM.Common;
using RM.Server.Common;

using ArenaServer.Resource;
using ArenaServer.Util;
using ArenaServer.Net;

namespace ArenaServer.Game
{
    public class DeployManager
    {
        public DeployManager(Arena arena)
        {
            m_arena = arena;
        }

        public bool Initialize(List<int> deployIDs)
        {
            m_jsonDepoly = new Dictionary<int, ResJson_Deploy>();

            foreach (var deployID in deployIDs)
            {
                ResJson_Deploy resDeploy;
                if (true == ResourceManager.Instance.JsonDeploy.TryGetValue(deployID, out resDeploy))
                {
                    m_jsonDepoly.Add(deployID, resDeploy);
                }
            }
        
            InitDeploy();
            return true;
        }

        private void InitDeploy()
        {
            foreach (var pair in m_jsonDepoly)
            {
                DelayedTask task = new DelayedTask(() => {
                    Deploy(pair.Key);
                });
                task.Submit(pair.Value.StartDelayTime, (int)TimerDispatcherIDType.InitDeploy);
            }
        }

        private void Deploy(int deployID)
        {
            ResJson_Deploy jsonDeploy;
            if (true == m_jsonDepoly.TryGetValue(deployID, out jsonDeploy))
            {
                for (int count = 0; count < jsonDeploy.ArenaObjectCount; ++count)
                {
                    DelayedTask task = new DelayedTask(() => {
                        m_arena.Deploy(jsonDeploy.ArenaObject);
                    });
                    task.Submit((ulong)count * jsonDeploy.ArenaObjectCountIntervalTime, (int)TimerDispatcherIDType.Deploy);
                }
            }

            if (true == jsonDeploy.UseInterval)
            {
                DelayedTask subTask = new DelayedTask(() =>
                {
                    Deploy(jsonDeploy.DeployID);
                });
                subTask.Submit(jsonDeploy.IntervalTime, (int)TimerDispatcherIDType.IntervalDeploy);
            }
        }

        Arena m_arena;
        Dictionary<int, ResJson_Deploy> m_jsonDepoly = null;
    }
}
