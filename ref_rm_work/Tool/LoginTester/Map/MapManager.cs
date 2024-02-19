using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

using RM.Common;

using rm_login.Tool.Script;

namespace rm_login.Tool
{
    public class MapManager
    {
        static private readonly Lazy<MapManager> s_lazy = new Lazy<MapManager>(() => new MapManager());
        static public MapManager Instance { get { return s_lazy.Value; } }

        public bool Initialize(PictureBox pictureBox)
        {
            m_pen = new Pen(Color.Green, 1);

            m_pictureBoxW = pictureBox.Size.Width;
            m_pictureBoxH = pictureBox.Size.Height;

            return true;
        }

        public bool Reset(int mapID)
        {
            ResJson_Map resMap = null;
            if (false == ResourceManager.Instance.JsonMap.TryGetValue(mapID, out resMap))
            {
                Debug.Assert(false);
                return false;
            }

            if (resMap.Grid.Width != resMap.Grid.Height)
            {
                Debug.Assert(false);
                return false;
            }

            m_curMapW = resMap.Size.Width;
            m_curMapH = resMap.Size.Height;
            m_mapScaleW = m_pictureBoxW / m_curMapW;
            m_mapScaleH = m_pictureBoxH / m_curMapH;

            //m_curGridW = gridW;
            //m_curGridH = gridH;

            return true;
        }

        public void Draw(Graphics graphics)
        {
            //graphics.DrawRectangle(m_pen, 0, 0, ((m_curMapW) * m_mapScaleW) - 1, ((m_curMapH) * m_mapScaleH) - 1);

            int grid_count_x = 10;
            int grid_count_y = 10;

            for (int x = 0; x < grid_count_x; ++x)
            {
                float draw_x = x * (m_curMapW / grid_count_x) * m_mapScaleW;
                graphics.DrawLine(m_pen, draw_x, 0, draw_x, (m_curMapH * m_mapScaleH) - 1);
            }

            for (int y = 0; y < grid_count_y; ++y)
            {
                float draw_y = y * (m_curMapH / grid_count_y) * m_mapScaleH;
                graphics.DrawLine(m_pen, 0, draw_y, (m_curMapW * m_mapScaleW) - 1, draw_y);
            }
        }

        public void PositionOnMap(float object_x, float object_y, out float out_x, out float out_y)
        {
            out_x = object_x * m_mapScaleW;
            //out_y = (m_curMapH - object_y - 1) * m_mapScaleH;
            out_y = ((m_curMapH - object_y) * m_mapScaleH) - 1;
        }

        public void PositionClamp(float object_x, float object_y, out float out_x, out float out_y)
        {
            out_x = RMMath.Clamp(object_x, 0, m_curMapW - 1);
            out_y = RMMath.Clamp(object_y, 0, m_curMapH - 1);
        }


        public float MapScaleW 
        {
            get => m_mapScaleW;
        }

        public float MapScaleH 
        {
            get => m_mapScaleH;
        }

        Pen m_pen = null;

        int m_pictureBoxW = 0;
        int m_pictureBoxH = 0;
        
        float m_curMapW = 0.0f;
        float m_curMapH = 0.0f;

        float m_mapScaleW = 0.0f;
        float m_mapScaleH = 0.0f;

        //float m_curGridW = 0;
        //float m_curGridH = 0;

    }
}
