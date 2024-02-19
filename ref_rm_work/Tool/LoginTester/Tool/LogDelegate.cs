using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rm_login.Tool
{
    public class LogDelegate
    {
        static private readonly Lazy<LogDelegate> s_lazy = new Lazy<LogDelegate>(() => new LogDelegate());
        static public LogDelegate Instance { get { return s_lazy.Value; } }

        public delegate void EventHandler(string log, System.Drawing.Color ? color = null);

        //-LogEvent 의 type 은 EventHandler. LogEvent 에 등록하려면, EventHandler 타입으로 등롴. 
        public event EventHandler LogEvent;

        public bool Initialize()
        {
            Console.WriteLine("[{0}] Log delegate Initialize completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            return true;
        }

        public void Log(string log, System.Drawing.Color ? color = null)
        {
            LogEvent?.Invoke(log, color);
        }

        static public string ToSimpleUID(Guid uid)
        {
            var guidstring = uid.ToString();
            return guidstring.Substring(0, guidstring.IndexOf("-")).Trim();
        }
    }
}
