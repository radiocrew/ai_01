using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using RM.Common;

namespace rm_login.Tool
{
    public class PlayerObject : ArenaObject
    {
        public PlayerObject()
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
        //    //    int sight_circle_radius = 15;

        //    //    Rectangle rect = new Rectangle((int)sight_x - sight_circle_radius + OBJECT_RADIUS, 
        //    //                                    (int)sight_y - sight_circle_radius + OBJECT_RADIUS, 
        //    //                                    (int)sight_circle_radius * 2, 
        //    //                                    (int)sight_circle_radius * 2);

        //    //    //-각도보정
        //    //    var adjustDir = Dir - 90.0f;
        //    //    RMMath.Clamp(adjustDir, 360.0f - adjustDir, 360.0f);

        //    //    float arc_angle = 60.0f;
        //    //    float startAngle = adjustDir - (arc_angle / 2.0f);
        //    //    float endAngle = arc_angle;

        //    //    m_graphics.DrawArc(m_penSkillSight, rect, startAngle, endAngle);
        //    //}
        //}
    }
}
