using System;
using System.Threading;

namespace RM.Server.Common
{ 
    public class AsyncTask
    {
        static public void Execute(Action task, TimerDispatcherIDType id, ulong delayMS = 1)//-jinsub ¿Ã∞…∑Œ Ωœ πŸ≤„≥ı¿ª∞Õ DelayedTask...
        {
            DelayedTask dt = new DelayedTask(task);
            dt.Submit(delayMS, (int)id);
        }
    }
}
