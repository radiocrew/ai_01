using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace rm_login.Tool
{
    public class DPlayer : BaseDraw
    {
        public static float PLAYER_SIGHT = 5.0f;//-testcode, db_collision.json 을 참조해야해.
        public static float PLAYER_ATTACK_RANGE = 1f;

        public override bool Initialize(Guid uid, ToolDefine.ObjectType objectType, float x, float y, float d, float sight, float atkRange)
        {
            base.Initialize(uid, objectType, x, y, d, sight, atkRange);

            

            return true;
        }

        public override void OnDrawObject(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
            float object_x = x;
            float object_y = y;

            MapManager.Instance.PositionOnMap(x, y, out object_x, out object_y);

            //var brush = (true == remove) ? (m_eraseBrush) : (m_objectBrush);
            //graphics.FillEllipse(brush, object_x, object_y, 2, 2);


            float radius_x = MapManager.Instance.MapScaleW * ToolDefine.TEST_OBJECT_RADIUS;
            float radius_y = MapManager.Instance.MapScaleH * ToolDefine.TEST_OBJECT_RADIUS;

            object_x -= radius_x;
            object_y -= radius_y;

            var brush = (true == remove) ? (m_eraseBrush) : (m_objectBrush);
            graphics.FillEllipse(brush, object_x, object_y, radius_x * 2, radius_y * 2);

            DrawSight(graphics, x, y, remove);
        }

        public override void DrawSight(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
        }
    }
}
