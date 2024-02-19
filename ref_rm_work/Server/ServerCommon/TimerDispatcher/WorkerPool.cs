using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

namespace RM.Server.Common
{
    public class WorkerPool
    {
        public void Initialize(int maxThreadCount)
        {
            m_workerWorkingCount = new Dictionary<int, int>();
            m_workers = new ConcurrentDictionary<int, Worker>();

            for (int i = 0; i < maxThreadCount; ++i)
            {
                int workerID = i;

                Worker worker = new Worker();
                worker.Initialize(workerID);
                worker.WorkingEvent += new Worker.UpdateWorkingCount(UpdateWorkingCount);

                m_workers.TryAdd(worker.ID, worker);
                m_workerWorkingCount.Add(workerID, 0);
            }
        }

        public void Terminate()
        {
            m_workers.All(pair => {
                pair.Value.Terminate();
                return true;
            });
        }

        public void UpdateWorkingCount(int id, int count)
        {
            lock (m_workerWorkingCountLock)
            {
                m_workerWorkingCount[id] = count;
            }
                
        }

        public void Push(Action action)
        {
            GetLazyWorker()?.Push(action);
        }

        private Worker GetLazyWorker()
        {
            int id = 0;
            lock (m_workerWorkingCountLock)
            {
                id = m_workerWorkingCount.FirstOrDefault(pair => pair.Value == m_workerWorkingCount.Values.Min()).Key;
            }
                
            Worker worker = null;
            if (true == m_workers.TryGetValue(id, out worker))
            {
                return worker;
            }

            return null;
        }

        object m_workerWorkingCountLock = new object();

        Dictionary<int, int> m_workerWorkingCount = null;
        ConcurrentDictionary<int, Worker> m_workers = null;
    }
}
