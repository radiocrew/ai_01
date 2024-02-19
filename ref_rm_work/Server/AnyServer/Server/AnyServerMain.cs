using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Level9.Expedition.Mobile.Util;
using Level9.Expedition.Mobile.Frame;
using Level9.Expedition.Mobile.Logging;

namespace AnyServer.Net
{
    public sealed class AnyServer : ServerFrame
    {

        protected override bool OnInitialize()
        {
            return true;
        }

        protected override void OnUnInitialize()
        {
        }
    }
}
