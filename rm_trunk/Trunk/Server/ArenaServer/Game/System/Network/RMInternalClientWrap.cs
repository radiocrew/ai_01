using System;
using System.Diagnostics;
using System.Net;
using System.Xml;

using Level9.Expedition.Mobile.Config;
using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Net.SuperSocket.Client.TCP;
using Level9.Expedition.Mobile.Net.SuperSocket.Common;
using Level9.Expedition.Mobile.Util;

using RM.Common;
using RM.Net;

namespace ArenaServer.Net
{
    public sealed class RMInternalClientWrap : Singleton<RMInternalClientWrap>
    {
        private RMInternalClient _InternalClient = null;

        private long _serverUpdatePeriod = 0;
        private long _serverUpdateCurrent = 0;

        //private long _roomUpdatePeriod = 1000;
        //private long _roomUpdateCurrent = 0;

        private RMServer NetPVPServer = null;

        private RMInternalClientWrap()
        {
        }

        public RMInternalClient NetPVPManager { get { return this._InternalClient; } }

        public bool Initialize(RMServer netPvpServer)
        {
            if (null == netPvpServer)
                return false;

            if (null != this._InternalClient)
                return false;

            NetPVPServer = netPvpServer;

            XmlNode serverConfig = ConfigManager.Instance.GetServerConfig();

            if (null == serverConfig)
            {
                Logger.Log(LogLevel.Error, "[NetServerManager : Initialize] serverConfig null");
                return false;
            }

            //if (false == LoadPVPManagerServerInfo(serverConfig))
            //    return false;

            //if (false == this._InternalClient.Initialize())
            //    return false;

            return true;
        }

        public void UnInitialize()
        {
            this._serverUpdateCurrent = 0;

            if (null != this._InternalClient)
            {
                this._InternalClient.UnInitialize();
                this._InternalClient = null;
            }
        }

        public bool ServerConnect()
        {
            if (null != this._InternalClient)
            {
                if (false == this._InternalClient.IsConnected)
                    this._InternalClient.Connect();
            }

            return true;
        }

        public void RetryServerConnect()
        {
            if (null != this._InternalClient && false == this._InternalClient.IsConnected)
            {
                this._InternalClient.Connect();
            }
        }

        public void Process()
        {
            //NetContentsManager.Instance.UpdateContentsManager();

            var currentTick = GetTickCount.CurrentTick();

            // Game Room Update
            //if (currentTick > _roomUpdateCurrent + _roomUpdatePeriod)
            //{
            //    _roomUpdateCurrent = currentTick;
            //    GameRoomManager.Instance.UpdateGameRoom(_roomUpdateCurrent);
            //}

            // Server Update
            if (currentTick > _serverUpdateCurrent + _serverUpdatePeriod)
            {
                _serverUpdateCurrent = currentTick;

                //if (false == CONST.BATTLE_SERVER_STAND_ALONE)
                {
                    RetryServerConnect();
                    UpdatePVPServerInfo();
                }
            }
        }

        public void SendPacket(RMPacket packet)
        {
            _InternalClient.SendPacket(packet.ToStream());
        }

        private bool LoadPVPManagerServerInfo(XmlNode serverConfig)
        {
            if (null == serverConfig)
                return false;

            XmlNode pvpManagerServerInfo = serverConfig.SelectSingleNode("MatchingServer");

            if (null == pvpManagerServerInfo)
            {
                Logger.Log(LogLevel.Error, "[NetServerManager : Initialize] pvpManagerServerInfo null");
                return false;
            }

            if (null == pvpManagerServerInfo.Attributes["serverUID"])
            {
                Logger.Log(LogLevel.Error, "[NetServerManager : Initialize] serverUID null");
                return false;
            }

            string serverUID = pvpManagerServerInfo.Attributes["serverUID"].Value;

            if (null == pvpManagerServerInfo.Attributes["connectIP"])
            {
                Logger.Log(LogLevel.Error, "[NetServerManager : Initialize] connectIP null");
                return false;
            }

            string connectIP = pvpManagerServerInfo.Attributes["connectIP"].Value;

            if (null == pvpManagerServerInfo.Attributes["connectPort"])
            {
                Logger.Log(LogLevel.Error, "[NetGateServer : Initialize] connectPort null");
                return false;
            }

            int listenPort = Convert.ToInt32(pvpManagerServerInfo.Attributes["connectPort"].Value);

            int receiveBufferSize = 8192;

            if (null != pvpManagerServerInfo.Attributes["receiveBufferSize"])
                receiveBufferSize = Convert.ToInt32(pvpManagerServerInfo.Attributes["receiveBufferSize"].Value);

            if (0 >= receiveBufferSize)
                receiveBufferSize = 8192;

            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(connectIP), listenPort) as EndPoint;

            this._InternalClient = new RMInternalClient(endPoint, receiveBufferSize);
            this._InternalClient.ServerUID = serverUID;

            return true;
        }

        private void UpdatePVPServerInfo()
        {
            if (null == _InternalClient || !_InternalClient.IsConnected)
                return;

            //var sendJsonData = new PPM_Update_PVPServer_Ntf
            //{
            //};

//#if DEBUG
            //Logger.Log(LogLevel.Debug, "[UpdatePVPServerInfo] 디버그 에만 기록 합니다. PPM_Update_PVPServer_Ntf : {0}.", sendJsonData.ToString());
//#endif
//            this._InternalClient.SendQueue.Add(sendJsonData);

            //var sendPacket =  sendJsonData.ToStream();
            //this._netPVPManager.SendPacket(sendPacket);
        }

    }
}