using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace rm_login.Tool
{
    public class DNpc : BaseDraw
    {
        public override bool Initialize(Guid uid, ToolDefine.ObjectType objectType, float x, float y, float d, float sight, float atkRange)
        {
            if (false == base.Initialize(uid, objectType, x, y, d, sight, atkRange))
            {
                return false;
            }

            m_sightPen = new Pen(Color.WhiteSmoke, 1);
            m_atkRangePen = new Pen(Color.Yellow, 1);
            m_erasePen = new Pen(Color.Black, 1);
            return true;
        }

        public override void OnDrawObject(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
            float object_x = x;
            float object_y = y;

            MapManager.Instance.PositionOnMap(x, y, out object_x, out object_y);

            float radius_x = MapManager.Instance.MapScaleW * ToolDefine.TEST_OBJECT_RADIUS;
            float radius_y = MapManager.Instance.MapScaleH * ToolDefine.TEST_OBJECT_RADIUS;

            object_x -= radius_x;
            object_y -= radius_y;

            var brush = (true == remove) ? (m_eraseBrush) : (m_objectBrush);
            graphics.FillEllipse(brush, object_x, object_y, radius_x * 2, radius_y * 2);


            DrawSight(graphics, x, y, remove);
            DrawAtkRange(graphics, x, y, remove);
        }

        public override void DrawSight(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
            float sight_x = x;
            float sight_y = y;

            MapManager.Instance.PositionOnMap(x, y, out sight_x, out sight_y);

            float radius_x = MapManager.Instance.MapScaleW * m_sight;
            float radius_y = MapManager.Instance.MapScaleH * m_sight;

            sight_x -= radius_x;
            sight_y -= radius_y;

            var pen = (true == remove) ? (m_erasePen) : (m_sightPen);
            graphics.DrawEllipse(pen, sight_x, sight_y, radius_x * 2, radius_y * 2);
        }

        public override void DrawAtkRange(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
            float atk_x = x;
            float atk_y = y;

            MapManager.Instance.PositionOnMap(x, y, out atk_x, out atk_y);

            float radius_x = MapManager.Instance.MapScaleW * m_atkRange;
            float radius_y = MapManager.Instance.MapScaleH * m_atkRange;

            atk_x -= radius_x;
            atk_y -= radius_y;

            var pen = (true == remove) ? (m_erasePen) : (m_atkRangePen);
            graphics.DrawEllipse(pen, atk_x, atk_y, radius_x * 2, radius_y * 2);
        }

        protected System.Drawing.Pen m_sightPen = null;
        protected System.Drawing.Pen m_atkRangePen = null;
        protected System.Drawing.Pen m_erasePen = null;
    }
}
