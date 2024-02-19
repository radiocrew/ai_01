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
        public static int OBJECT_RADIUS = 2;

        //public override void Draw()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void UnDraw()
        //{
        //    throw new NotImplementedException();
        //}

        public virtual void Initialize(ToolDefine.ObjectType type)//, PictureBox pictureBox)
        {
            m_type = type;

            //lock (m_grahicsLock)
            //{
            //    switch (m_type)
            //    {
            //        case ObjectType.Hero:
            //            m_brush = Brushes.Yellow;
            //            break;
            //        case ObjectType.Player:
            //            m_brush = Brushes.White;
            //            break;
            //        case ObjectType.Npc:
            //            m_brush = Brushes.Red;
            //            break;
            //    }

            //    m_graphics = pictureBox.CreateGraphics();

                m_pen = new Pen(Color.White, 1);
                m_penSkillSight = new Pen(Color.Yellow, 1);
            //}
        }

        //public virtual void Draw()
        //{
        //    /*
        //     * winform  : left top      → 0, 0
        //     * server   : left bottm    → 0, 0
        //     */


        //    float object_x = 0.0f;
        //    float object_y = 0.0f;
        //    MapManager.Instance.PositionOnMap(X, Y, out object_x, out object_y);

        //    object_y -= OBJECT_RADIUS;

        //    //lock (m_grahicsLock)
        //    //{
        //    //    m_graphics.FillEllipse(m_brush, object_x, object_y, OBJECT_RADIUS * 2, OBJECT_RADIUS * 2);
        //    //}
        //}

        public float X { get; set; }
        public float Y { get; set; }

        public float Dir { get; set; }

        protected ToolDefine.ObjectType m_type = ToolDefine.ObjectType.None;

        protected Pen m_pen = null;
        protected System.Drawing.Brush m_brush = null;

        //protected object m_grahicsLock = new object();
        //protected Graphics m_graphics;

        protected Pen m_penSkillSight = null;

        //protected int MapWidth { get; set; }
        //protected int MapHeight { get; set; }
    }
}
