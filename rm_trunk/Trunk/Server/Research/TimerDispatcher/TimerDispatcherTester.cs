using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace RM.Server.Common
{
    public class TimerQueueTester
    {
        public void Begin()
        {
            /*
             * .delayed �۵� ���� �׽�Ʈ  (������ ������ �� worker����, ť, ������ Ȯ��)
             * .N���� no delayed �۵� ���� �׽�Ʈ (������ ������ �� worker����, ť, ������ Ȯ��)
             * .N���� expired timer �۵� �׽�Ʈ (������ ������ �� worker����, ť, ������ Ȯ��)
             * .���� ��� ��Ȳ�� N ���� task�� �־ �׽�Ʈ, �ּ� 10������ ������ (CPU ������ Ȯ���� ��)(������ ������ �� worker����, ť, ������ Ȯ��) 
             * .TimerDispatcher �� ���� �׽�Ʈ
             */

            m_tasks = new ConcurrentDictionary<int, Task>();

            for (int i = 0; i < 4; ++i)
            {
                m_tasks.TryAdd(i, new Task(
                    new Action(() => {
                        for (int y = 0; y < 50000; ++y)
                        {
                            //Console.WriteLine(">> dt calling : {0}, T{1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount);

                            DelayedTask dt = new DelayedTask(() =>
                            {
                                Console.WriteLine("<< dt called : {0} {1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount);
                            });

                            dt.Submit(1);
                            //Thread.Sleep(1);
                        }
                    })
                    ));
            }
        }

        public void Run()
        {
            Console.WriteLine("Test start : {0}, T{1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount);

            m_tasks.All(pair => {

                pair.Value.Start();

                return true;
            });
        }

        public void End()
        {
            //m_tasks.All(pair => {

            //    pair.Value.Wait();
            //    return true;
            //});

            Console.WriteLine("Test Finish : {0}, T{1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount);
            TimerDispatcher.Instance.Statistic();

            return;
        }


        ConcurrentDictionary<int, Task> m_tasks = null;

    }
}
