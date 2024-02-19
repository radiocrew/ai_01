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
             * .delayed 작동 여부 테스트  (스레드 점유율 및 worker상태, 큐, 데이터 확인)
             * .N개의 no delayed 작동 여부 테스트 (스레드 점유율 및 worker상태, 큐, 데이터 확인)
             * .N개의 expired timer 작동 테스트 (스레드 점유율 및 worker상태, 큐, 데이터 확인)
             * .위의 모든 상황을 N 개의 task에 넣어서 테스트, 최소 10만개의 데이터 (CPU 점유율 확인할 것)(스레드 점유율 및 worker상태, 큐, 데이터 확인) 
             * .TimerDispatcher 의 종료 테스트
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
