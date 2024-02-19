using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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

            lock (m_grahicsLock)
            {
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
        }

        public void Draw()
        {

            /*
             * windows : left top : 0, 0
             * server : left bottm : 0. 0
             */


            int object_radius = 2;

            float object_x = X - object_radius;
            float object_y = (MapHeight - Y - 1) - object_radius;

            lock (m_grahicsLock)
            {
                m_graphics.FillEllipse(m_brush, object_x, object_y, object_radius * 2, object_radius * 2);
            }

            if (ObjectType.Npc == m_type)
            {
                int object_sight_radius = 15;// db_npc.json.Sight 

                lock (m_grahicsLock)
                {
                    m_graphics.DrawEllipse(m_pen,
                                        object_x - object_sight_radius + object_radius,
                                        object_y - object_sight_radius + object_radius,
                                        (float)(object_sight_radius * 2),
                                        (float)(object_sight_radius * 2));
                }
            }
        }

        public float X { get; set; }
        public float Y { get; set; }

        ObjectType m_type = ObjectType.None;

        private Pen m_pen = null;
        private System.Drawing.Brush m_brush = null;

        object m_grahicsLock = new object();
        private Graphics m_graphics;

        private int MapWidth { get; set; }
        private int MapHeight { get; set; }
    }
}
