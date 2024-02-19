using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace rm_login.Tool
{
    public class DrawManager
    {
        static private readonly Lazy<DrawManager> s_lazy = new Lazy<DrawManager>(() => new DrawManager());
        static public DrawManager Instance { get { return s_lazy.Value; } }

        object m_deltaLock = new object();
        long LastTime = 0;
        double GetDeltaTime()
        {
            lock (m_deltaLock)
            {
                long now = DateTime.Now.Ticks;
                double dT = (now - LastTime);

                if (0 == LastTime)
                {
                    LastTime = now;
                    return 0;
                }

                LastTime = now;

                dT /= 10000000;
                var ret = Math.Round((double)dT, 3);
                return ret;
            }
        }

        public bool Initialize(PictureBox pictureBox)
        {
            m_pictureBox = pictureBox;
            m_graphics = m_pictureBox.CreateGraphics();

            //m_graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //m_graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            
            m_draws = new ConcurrentDictionary<Guid, BaseDraw>();

            m_updateTimer = new System.Threading.Timer(new System.Threading.TimerCallback(UpdateCallBack));
            m_updateTimer.Change(1, 100);

            m_refreshTimer = new System.Threading.Timer(new System.Threading.TimerCallback(RefreshCallBack));
            m_refreshTimer.Change(1, 1000);
            return true;
        }

        public BaseDraw Regist(Guid uid, ToolDefine.ObjectType objectType, float x, float y, float d, float sight, float atkRange)
        {
            if (false == m_draws.ContainsKey(uid))
            {
                var drawObject = DrawObjectFactory.Create(objectType, uid, x, y, d, sight, atkRange);
                m_draws.TryAdd(drawObject.UID, drawObject);

                return drawObject;
            }

            return null;
        }

        public void UnRegist(Guid uid)
        {
            BaseDraw remove = null;
            if (true == m_draws.TryRemove(uid, out remove))
            {
                remove.Destroy();

                lock (m_pictureBoxLock)
                {
                    remove.UnDraw(m_graphics);
                }
            }
        }

        public void Update(Guid uid, float x, float y, float d)
        {
            BaseDraw objectDraw = null;
            if (true == m_draws.TryGetValue(uid, out objectDraw))
            {
                objectDraw.UpdatePosition(x, y);
            }
        }

        public void Clear(bool isClear = true)
        {
            if (true == isClear)
            {
                m_draws.Clear();
            }

            
            lock (m_pictureBoxLock)
            {
                if (true == m_pictureBox.InvokeRequired)
                {
                    m_pictureBox.Invoke((MethodInvoker)delegate
                    {
                        if (true == isClear)
                        {
                            m_pictureBox.Refresh();
                        }
                        else
                        {
                            m_pictureBox.Update();
                        }
                    });
                }
            }                
        }

        private void UpdateCallBack(object state)
        {
            OnUpdate(GetDeltaTime());
        }

        private void RefreshCallBack(object state)
        {
            Clear(false);
        }


        private void OnUpdate(double dt)
        {
            DrawBackGround();
            UnDraw();
            Draw();
        }

        public void Draw()
        {
            m_draws.All(pair => {
                lock (m_pictureBoxLock)
                {
                    pair.Value.Draw(m_graphics);
                }
                return true;
            });
        }

        public void UnDraw()
        {
            m_draws.All(pair => {

                lock (m_pictureBoxLock)
                {
                    pair.Value.UnDraw(m_graphics);
                }
                
                return true;
            });
        }

        public void DrawBackGround()
        {
            lock (m_pictureBoxLock)
            {
                MapManager.Instance.Draw(m_graphics);
            }
        }

        object m_pictureBoxLock = new object();
        PictureBox m_pictureBox = null;
        Graphics m_graphics = null;

        ConcurrentDictionary<Guid, BaseDraw> m_draws = null;
        System.Threading.Timer m_updateTimer = null;

        System.Threading.Timer m_refreshTimer = null;
    }

}
