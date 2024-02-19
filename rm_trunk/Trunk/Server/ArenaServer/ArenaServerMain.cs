using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

using Level9.Expedition.Mobile.Util;
using Level9.Expedition.Mobile.Frame;
using Level9.Expedition.Mobile.Logging;

using RM.Server.Common;

using ArenaServer.Resource;
using ArenaServer.Game;
using ArenaServer.Tester;
using ArenaServer.Game.AI;

namespace ArenaServer.Net
{
    public sealed class ArenaServer : ServerFrame
    {
        private RMServer m_server = null;


        public ArenaServer()
        {
            this.m_server = new RMServer();
        }

        public static string GetServerVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void Process()
        {
            RMInternalClientWrap.Instance.Process();
        }

        protected override bool OnInitialize()
        {
            Logger.Log(LogLevel.Info, "ServerUID({0})", ArenaServer.ServerUID);

            if (false == base.OnInitialize())
            {
                Logger.Log(LogLevel.Error, "[Arena Server : OnInitialize] base Initialize false");
                return false;
            }

            GetTickCount.Init();

            if (false == RMInternalClientWrap.Instance.Initialize(m_server))
                return false;

            if ((null == this.m_server) || false == this.m_server.Initialize())
                return false;


            if (false == InitGameManagers())
            {
                Logger.Log(LogLevel.Error, "[Arena Server : OnInitialize] Manager initialize failed.");
                return false;
            }


            if (false == this.m_server.Start())
            {
                // Local IP 설정 문제 확률 있음
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[Arena Server : Start] Failed to Start!");

                Debug.Assert(false);
                return false;
            }

            Logger.Log(LogLevel.Info, "Initialize Success");

            return true;
        }

        protected override void OnUnInitialize()
        {
            if (null != this.m_server)
            {
                m_server.UnInitialize();
            }

            RMInternalClientWrap.Instance.UnInitialize();
            ResourceManager.Instance.Release();
            TimerDispatcher.Instance.Terminate();

            base.OnUnInitialize();
            Logger.Log(LogLevel.Info, "UnInitialize Success");
        }

        protected override void OnRun()
        {
            if (false == IsServiceMode())
            {
                if (true == Console.KeyAvailable)
                {
                    char keyChar = Console.ReadKey(true).KeyChar;

                    if ('q' == keyChar)
                    {
                        RunStop();
                    }

                    //if ('c' == keyChar)
                    //{
                    //    throw new ArgumentNullException("Exception");
                    //}
                }
            }

            Process();

            Thread.Sleep(1);
        }

        private bool InitGameManagers()
        {
            if (false == ResourceManager.Instance.Initialize("..\\..\\..\\..\\..\\..\\Deploy\\Server\\ServerData\\"))
            {
                return false;
            }

            if (false == TimerDispatcher.Instance.Initialize())//-콘텐츠 중에 가장 먼저.
            {
                return false;
            }

            if (false == VirtualMatchingManager.Instance.Initialize())
            {
                return false;
            }

            if ((false == VirtualMatchingServer.Instance.Initialize()) || (false == VirtualMatchingDispatcher.Instance.Initialize()))
            {
                return false;
            }

            if (false == VirtualDBServer.Instance.Initialize())
            {
                return false;
            }

            if (false == ArenaManager.Instance.Initialize())
            {
                return false;
            }

            if (false == ArenaMemberManager.Instance.Initialize())
            {
                return false;
            }

            if (false == ArenaFactory.Instance.Initialize())
            {
                return false;
            }

            if (false == NpcFactory.Instance.Initialize())
            {
                return false;
            }

            if (false == PlayerFactory.Instance.Initialize())
            {
                return false;
            }

            if (false == AiFSMFactory.Instance.Initialize())
            {
                return false;
            }

            //if (false == PlayerFormular.Instance.Initialize())
            //{
            //    return false;
            //}

            if (false == LevelExperienceManager.Instance.Initialize())
            {
                return false;
            }

            if (false == BaseStatManager.Instance.Initialize()) //-jinsub, BaseStat 을 BaseStat으로 몽땅 바꾸는 것을 고려해보자. 
            {
                return false;
            }

            if (false == ActiveLevelExperienceManager.Instance.Initialize())
            {
                return false;
            }

            if (false == SkillManager.Instance.Initialize())
            {
                return false;
            }

            if (false == ItemManager.Instance.Initialize())
            {
                return false;
            }

            return true;
        }
    }
}