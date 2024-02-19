using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace RM.Server.Common//sim
{
    public class Worker
    {
        public delegate void UpdateWorkingCount(int id, int count);
        public event UpdateWorkingCount WorkingEvent;

        public bool Initialize(int id)
        {
            ID = id;
            m_waitEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
            m_queue = new ConcurrentQueue<Action>();

            m_thread = new Thread(OnUpdate);
            m_thread.Start();

            return true;
        }

        public void Terminate()
        {
            m_termination = true;
            m_waitEvent.Set();

            m_thread.Join();
        }

        public int ID
        {
            get;
            private set;
        }

        public void Push(Action task)
        {
            if (false == m_termination)
            {
                OnBeginWork();
                m_queue.Enqueue(task);

                m_waitEvent.Set();
            }
        }

        protected void OnBeginWork()
        {
        }

        protected void OnEndWork()
        {
            WorkingEvent(ID, m_queue.Count());
            Interlocked.Increment(ref m_workedCount);
        }

        private void OnUpdate()
        {
            while (false == m_termination)
            {
                while (true)
                {
                    if (0 == m_queue.Count)
                    {
                        break;
                    }

                    Action action = null;
                    if ((false == m_queue.TryDequeue(out action)) || (null == action))
                    {
                        continue;
                    }

                    action();
                    OnEndWork();
                }

                m_waitEvent.WaitOne();
            }

            while (0 != m_queue.Count)
            {
                Action action = null;
                m_queue.TryDequeue(out action);
            }

        }

        volatile bool m_termination = false;
        volatile int m_workedCount = 0;

        EventWaitHandle m_waitEvent = null;

        ConcurrentQueue<Action> m_queue = null;
        Thread m_thread = null;
    }
}
