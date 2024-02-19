using System;
using System.Xml;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

using Level9.Expedition.Mobile.Net.SuperSocket.Common;
using Level9.Expedition.Mobile.Net.SuperSocket.Server.TCP;
using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Config;

namespace ArenaServer.Net
{
    public sealed class RMServer : SSServer<PlayerSession>
    {
        public static string _ip = string.Empty;
        public static string _domain = string.Empty;
        public static int _port = 0;
        
        public static int _maxConnectionNumber { get; private set; }

        public bool Initialize()
        {
            XmlNode serverConfig = ConfigManager.Instance.GetServerConfig();

            if (null == serverConfig)
            {
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] serverConfig null");
                return false;
            }

            XmlNode serverInfo = serverConfig.SelectSingleNode("serverInfo");

            if (null == serverInfo)
            {
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] serverInfo null");
                return false;
            }

            if (null == serverInfo.Attributes["listenIP"])
            {
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] listenIP null");
                return false;
            }

            _ip = serverInfo.Attributes["listenIP"].Value;

            if (null == serverInfo.Attributes["domain"])
            {
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] domain null");
                return false;
            }

            _domain = serverInfo.Attributes["domain"].Value;
            //>> domain name to ipv4 (not ipv6)
            //      if domain name is wrong then use ip
            if (0 < _domain.Length)
            {
                try
                {
                    var ips = Dns.GetHostAddresses(_domain);
                    foreach (var ip in ips)
                    {
                        if (IPAddress.Parse(ip.ToString()).AddressFamily == AddressFamily.InterNetwork)
                        {
                            //Console.WriteLine(ip.ToString());
                            _ip = ip.ToString();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] domain name : {0}, {1}", _domain, ex.Message);
                    Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] so set ip again: {0}", _ip);
                }
            }
            //<<

            if (null == serverInfo.Attributes["listenPort"])
            {
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] listenPort null");
                return false;
            }

            _port = Convert.ToInt32(serverInfo.Attributes["listenPort"].Value);

            if (null != serverInfo.Attributes["maxConnectionNumber"])
                _maxConnectionNumber = Convert.ToInt32(serverInfo.Attributes["maxConnectionNumber"].Value);
            else
                _maxConnectionNumber = 8192 * 2;

            if (!base.Initialize(_ip, _port, _maxConnectionNumber))
            {
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Error, "[NetPVPServer : Initialize] Failed to base Initialize");
                return false;

            }

            // 패킷 암호화 키 설정
            //if (false == SSPacket.SetCryptoKey(NetSecurityKey.CRYPTO_KEY))
            //    return false;

            // IsLogConnection = false; // 테스트 를 위한 로그. 2018-09-11 에 기록 함. 태훈
            //var isLogConnection = System.Configuration.ConfigurationManager.AppSettings["IsLogConnection"];
            //if (!string.IsNullOrEmpty(isLogConnection))
            //{
            //    bool isLog = false;
            //    if (!bool.TryParse(isLogConnection, out isLog))
            //        isLog = false;
            //    IsLogConnection = isLog;
            //}

            return true;
        }

        public override void UnInitialize()
        {
            base.UnInitialize();

            _ip = string.Empty;
            _domain = string.Empty;
            _port = 0;
            _maxConnectionNumber = 0;
        }



        //public override void Stop()
        //{
        //    base.Stop();
        //}

        public static void GetServerInfo(out string ip, out string domain, out int port)
        {
            ip = _ip;
            domain = _domain;
            port = _port;
        }

        protected override void OnStarted()
        {
            base.OnStarted();
            Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, "[Arena Server : OnStarted] Success to Start!");
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, "[Arena  PServer : OnStopped] Success to Stop!");
        }
    }
}