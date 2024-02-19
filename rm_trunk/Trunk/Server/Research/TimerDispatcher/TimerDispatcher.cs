using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Level9.Expedition.Mobile.Util;
using Priority_Queue;

namespace RM.Server.Common
{
    public abstract class TimerInstance
    {
        public abstract void TimerExpired(int id);

        public void Submit(ulong callTimeInMs, int id = 0)
        {
            TimerDispatcher.Instance.Submit(this, callTimeInMs, id);
        }

        public void SubmitImmediately(int id = 0)
        {
            TimerDispatcher.Instance.Submit(this, 1, id);
        }
    }

    public class DelayedTask : TimerInstance
    {
        public DelayedTask(Action action)
        {
            m_action = action;
        }

        public override void TimerExpired(int id)
        {
            m_action.Invoke();
        }

        Action m_action = null;
    }

    public class TimerDispatcher : Singleton<TimerDispatcher>
    {
        public class TimerWrapper : StablePriorityQueueNode
        {
            public TimerWrapper(TimerInstance instance, ulong expireTime, int id)
            {
                m_instance = instance;
                ExpireTIme = expireTime;
                ID = id;
            }

            public TimerInstance GetInstance()
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

            TimerInstance m_instance = null;
        }

        public bool Initialize()
        {
            m_workerPool = new WorkerPool();
            m_workerPool.Initialize(4);

            ThreadStart threadDelegate = new ThreadStart(CheckThread);
            m_thread = new Thread(threadDelegate);
            m_thread.Start();

            return true;
        }
        public virtual bool Submit(TimerInstance instance, ulong callTimeMS, int id)
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
                            task.TimerExpired(id);
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
        StablePriorityQueue<TimerWrapper> m_queue = new StablePriorityQueue<TimerWrapper>(1000000);
        volatile bool m_termination = false;
        
        WorkerPool m_workerPool = null;        
    }
}
