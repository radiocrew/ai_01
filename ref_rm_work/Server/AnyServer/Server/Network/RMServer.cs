using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Net.SuperSocket.Common;
using Level9.Expedition.Mobile.Net.SuperSocket.Server.TCP;
using Level9.Expedition.Mobile.Logging;
using Level9.Expedition.Mobile.Config;

namespace AnyServer.Net
{
    public sealed class RMServer : SSServer<AnySession>
    {
        public bool Initialize()
        {
            return false;
        }

        public override void UnInitialize()
        {
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }
    }
}
