using System;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace rm_login.Tool
{
    public class ArenaObject
    {
        public enum ObjectType : int
        {
            None = 0,
            Hero = 1,
            Player = 2,
            Npc = 3,
        }


        public void Initialize(ObjectType type, PictureBox pictureBox)
        {
            m_type = type;

            switch (m_type)
            {
                case ObjectType.Hero:
                    m_brush = Brushes.Yellow;
                    break;
                case ObjectType.Player:
                    m_brush = Brushes.White;
                    break;
                case ObjectType.Npc:
                    m_brush = Brushes.Red;
                    break;
            }

            MapWidth = 150;// pictureBox.Size.Width;
            MapHeight = 150;// pictureBox.Size.Height;

            m_graphics = pictureBox.CreateGraphics();

            m_pen = new Pen(Color.White, 1);
        }

        public void Draw()
        {

            /*
             * windows : left top : 0, 0
             * server : left bottm : 0. 0
             */


            int object_radius = 2;
           
            int object_x = X - object_radius;
            int object_y = (MapHeight - Y - 1) - object_radius;
            m_graphics.FillEllipse(m_brush, object_x, object_y, object_radius * 2, object_radius * 2);

            if (ObjectType.Npc == m_type)
            {
                int object_sight_radius = 25;// db_npc.json.Sight 

                m_graphics.DrawEllipse(m_pen, 
                                        object_x - object_sight_radius + object_radius,
                                        object_y - object_sight_radius + object_radius,
                                        object_sight_radius * 2, 
                                        object_sight_radius * 2);
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

        ObjectType m_type = ObjectType.None;

        private Pen m_pen = null;
        private System.Drawing.Brush m_brush = null;
        private Graphics m_graphics;

        private int MapWidth { get; set; }
        private int MapHeight { get; set; }
    }

    public class ArenaObjectManager
    {
        static private readonly Lazy<ArenaObjectManager> s_lazy = new Lazy<ArenaObjectManager>(() => new ArenaObjectManager());
        static public ArenaObjectManager Instance { get { return s_lazy.Value; } }


        public delegate void PositionEvent(int type, int x, int y);
        public event PositionEvent posFunc;

        public void Initialize(PictureBox pictureBox)
        {
            m_pictureBox = pictureBox;
            m_objects = new ConcurrentDictionary<Guid, ArenaObject>();
        }

        public void DisplayPosition(int type, int x, int y)
        {
            posFunc(type, x, y);
        }


        public void Enter(Guid guid, ArenaObject.ObjectType type, int x, int y)
        {
            DrawRefresh();

            ArenaObject arenaObject = new ArenaObject();
            arenaObject.Initialize(type, m_pictureBox);
            arenaObject.X = x;
            arenaObject.Y = y;

            if (true == m_objects.TryAdd(guid, arenaObject))
            {
                return;
            }

            Debug.Assert(false);

            Draw();
        }

        public void Leave(Guid guid)
        {
            DrawRefresh(); 

            ArenaObject arenaObject;
            m_objects.TryRemove(guid, out arenaObject);

            Draw();
        }

        public void Move(Guid guid, int x, int y)
        {
            DrawRefresh();

            ArenaObject arenaObject;
            if (true == m_objects.TryGetValue(guid, out arenaObject))
            {
                arenaObject.X = x;
                arenaObject.Y = y;
            }

            Draw();
        }

        public void Clear()
        {
            m_objects.Clear();
            DrawRefresh();
        }

        public void Draw()
        {
            foreach (var obj in m_objects.Values)
            {
                obj.Draw();
            }
        }

        private void DrawRefresh()
        {
            if (true == m_pictureBox.InvokeRequired)
            {
                m_pictureBox.Invoke((MethodInvoker)delegate
                {
                    m_pictureBox.Refresh();
                });
            }
        }

        PictureBox m_pictureBox = null;
        ConcurrentDictionary<Guid, ArenaObject> m_objects = null;
    }
}
