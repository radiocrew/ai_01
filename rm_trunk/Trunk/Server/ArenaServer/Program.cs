using System;
using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Util;

namespace ArenaServer.Net
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += MiniDumper.Current_UnhandledExceptionEventHandler;

            Log4NetLog log = new Log4NetLog();

            if (false == log.Initialize())
                return;

            if (false == Logger.Initialize(log))
                return;

            Logger.Log(LogLevel.Info, "===== Arena Server (Ver:{0}) =====", ArenaServer.GetServerVersion());

            ArenaServer server = new ArenaServer();

            if (false == server.Begin(server, "ArenaServer.exe.config"))
            {
                Logger.Log(LogLevel.Error, "[Program : Main] Begin false");
            }
            else
                server.Wait();

            server.End();
            server = null;

            Logger.Log(LogLevel.Info, "==========================================");
            Logger.UnInitialize();
            log.UnInitialize();
            log = null;
        }
    }
}