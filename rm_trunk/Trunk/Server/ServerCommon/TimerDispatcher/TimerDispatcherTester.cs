using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using System.Numerics;
using UnityEngine;

// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace RM.Server.Common
{
    public class TimerQueueTester
    {
        public void First()
        {
            UnityEngine.Vector2 v1 = new UnityEngine.Vector2(0.0f, 1.0f);
            UnityEngine.Vector2 v2 = new UnityEngine.Vector2(3.0f, 3.0f);

            var v3 = v2 - v1;

            var l = v3.magnitude;//4.2

            return;
        }

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

                            dt.Submit(1, (int)TimerDispatcherIDType.None);
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
