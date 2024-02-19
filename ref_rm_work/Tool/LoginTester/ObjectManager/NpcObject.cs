using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rm_login.Tool
{
    public class NpcObject : ArenaObject
    {
        public NpcObject()
        {
        }

        public override void Initialize(ToolDefine.ObjectType type)//, PictureBox pictureBox)
        {
            base.Initialize(type);//, pictureBox);
        }

        //public override void Draw()
        //{
        //    base.Draw();

        //    float sight_x = 0.0f;
        //    float sight_y = 0.0f;

        //    MapManager.Instance.PositionOnMap(X, Y, out sight_x, out sight_y);
        //    sight_y -= OBJECT_RADIUS;

        //    //lock (m_grahicsLock)
        //    //{
        //    //    int object_sight_radius = 15;// db_npc.json.Sight  jinsub,
        //    //    m_graphics.DrawEllipse(m_pen,
        //    //                        sight_x - object_sight_radius + OBJECT_RADIUS,
        //    //                        sight_y - object_sight_radius + OBJECT_RADIUS,
        //    //                        (float)(object_sight_radius * 2),
        //    //                        (float)(object_sight_radius * 2));
        //    //}            
        //}
    }
}
