using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

using ArenaServer.Resource;

namespace ArenaServer.Util
{
    public class TimeStreamManager
    {
        public TimeStreamManager()
        {

        }

        public void AddTimeStream(TimeStream timeStream)
        {
            lock (m_Lock)
            {
                m_TimeStream.Add(timeStream);
            }

            timeStream.StartAction?.Invoke();
        }

        public void AddTimeStream(float time, Action action)
        {
            lock (m_Lock)
            {
                var timeStream = new TimeStream(time);
                timeStream.EndAction = action;
                m_TimeStream.Add(timeStream);
            }
        }

        public void AddInfiniteTimeSteam(float time, Action action)
        {
            lock (m_Lock)
            {
                var timeStream = new TimeStream(time);
                timeStream.LifeType = TimeStreamLifeType.Infinite;
                timeStream.EndAction = action;
                m_TimeStream.Add(timeStream);
            }
        }

        public void Update(float dt)
        {

            lock (m_Lock)
            {
                m_TimeStream.RemoveAll(timeStream =>
                {
                    timeStream.Update(dt);                    
                    return timeStream.IsRemove;
                });

                //Debug.WriteLine("stream cout {0}", m_TimeStream.Count);
            }
        }

        object m_Lock = new object();        
        List<TimeStream> m_TimeStream = new List<TimeStream>();              
    }
}
