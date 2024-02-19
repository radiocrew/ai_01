using System;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace rm_login.Tool
{
    public class ArenaObjectManager
    {
        static private readonly Lazy<ArenaObjectManager> s_lazy = new Lazy<ArenaObjectManager>(() => new ArenaObjectManager());
        static public ArenaObjectManager Instance { get { return s_lazy.Value; } }


        public delegate void PositionEvent(int type, int x, int y);
        public event PositionEvent posFunc;

        //static long lastTime = 0;
        //static double GetDeltaTime()
        //{
        //    long now = DateTime.Now.Ticks;
        //    double dT = (now - lastTime) / 1000;
        //    lastTime = now;
            
        //    return dT;
        //}

        public bool Initialize()
        {
            m_objects = new ConcurrentDictionary<Guid, ArenaObject>();

            //m_updateTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerCallBack));
            //m_updateTimer.Change(1, 200);

            Console.WriteLine("[{0}] Arena object manager Initialize completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            return true;
        }

        //public void TimerCallBack(object state)
        //{
        //    OnUpdate(GetDeltaTime());
        //}

        //private void OnUpdate(double dt)
        //{
        //}

        public void Enter(Guid guid, ToolDefine.ObjectType type, float x, float y)
        {
            ArenaObject arenaObject = null;// new ArenaObject();

            switch (type)
            {
                case ToolDefine.ObjectType.Hero:
                    arenaObject = new PlayerObject();
                    break;
                case ToolDefine.ObjectType.Npc:
                    arenaObject = new NpcObject();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            if (null == arenaObject)
            {
                return;
            }

            arenaObject.Initialize(type/*, m_pictureBox*/);
            arenaObject.X = x;
            arenaObject.Y = y;

            if (true == m_objects.TryAdd(guid, arenaObject))
            {
                return;
            }
        }

        public void Leave(Guid guid)
        {
            ArenaObject arenaObject;
            m_objects.TryRemove(guid, out arenaObject);
        }

        public void Move(Guid guid, float x, float y, float dir)
        {
            ArenaObject arenaObject;
            if (true == m_objects.TryGetValue(guid, out arenaObject))
            {
                arenaObject.X = x;
                arenaObject.Y = y;
                arenaObject.Dir = dir;
            }

            //Draw();
        }

        public void Clear()
        {
            m_objects.Clear();
        }

        ConcurrentDictionary<Guid, ArenaObject> m_objects = null;
        //System.Threading.Timer m_updateTimer = null;
    }
}
