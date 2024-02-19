// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Priority_Queue;

namespace RM.Server.Common
{

    public class TimerDispatcher : Singleton<TimerDispatcher>
    {
        public class TimerWrapper : StablePriorityQueueNode
        {
            public TimerWrapper(TimerBase instance, ulong expireTime, int id)
            {
                m_instance = instance;
                ExpireTIme = expireTime;
                ID = id;
            }

            public TimerBase GetInstance()
            {
                return m_instance;
            }

            public ulong ExpireTIme 
            {
                get;
                private set;
            }

            public int ID
            {
                get;
                private set;
            }

            TimerBase m_instance = null;
        }

        public bool Initialize()
        {
            m_workerPool = new WorkerPool();
            m_workerPool.Initialize(DEFINE.MAX_TIMER_WORKER_THREAD_COUNT);

            ThreadStart threadDelegate = new ThreadStart(CheckThread);
            m_thread = new Thread(threadDelegate);
            m_thread.Start();

            return true;
        }
        public virtual bool Submit(TimerBase instance, ulong callTimeMS, int id)
        {
            if (true == m_termination)
            {
                return false;
            }

            lock (m_queueLock)
            {
                var interval = (ulong)Environment.TickCount + callTimeMS;

                m_queue.Enqueue(new TimerWrapper(instance, interval, id), interval);
                Monitor.PulseAll(m_queueLock);
            }

            return true;
        }

        public void Statistic()
        {
            return;
        }

        public void Terminate()
        {
            lock (m_queueLock)
            {
                m_termination = true;
                Monitor.PulseAll(m_queueLock);
            }

            m_thread.Join();
        }

        protected void CheckThread()
        {
            lock (m_queueLock)
            {
                while (false == m_termination)
                {
                    ulong now = (ulong)Environment.TickCount;
                    ulong next = default;

                    while (true)
                    {
                        if (0 == m_queue.Count)
                        {
                            Monitor.Wait(m_queueLock, Timeout.Infinite);
                            break;
                        }

                        var first = m_queue.First();                        
                        if (now < first.ExpireTIme)
                        {
                            next = first.ExpireTIme - now;
                            break;
                        }

                        var task = first.GetInstance();
                        var id = first.ID;

                        m_queue.Dequeue();

                        if (null == task)
                        {
                            continue;
                        }

                        m_workerPool.Push(() => {
                            task.CallBack(id);
                        });
                    }

                    Monitor.Wait(m_queueLock, (int)next);
                }

                while (0 != m_queue.Count)
                {
                    m_queue.Dequeue();
                }
            }
        }

        private TimerDispatcher()
        {
        }

        object m_queueLock = new object();

        Thread m_thread = null;
        StablePriorityQueue<TimerWrapper> m_queue = new StablePriorityQueue<TimerWrapper>(DEFINE.MAX_TIMER_QUEUE_SIZE);
        volatile bool m_termination = false;
        
        WorkerPool m_workerPool = null;        
    }
}
