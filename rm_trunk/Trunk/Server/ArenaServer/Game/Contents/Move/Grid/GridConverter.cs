using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ArenaServer.Game
{
    public class Rect
    {
        public Rect(float w, float h)
        {
            W = w;
            H = h;
        }

        public float W { get; set; }
        public float H { get; set; }

        //public int CX
        public double CX
        {
            get
            {
                if (false == m_cx.HasValue)
                {
                    //m_cx = Math.Round((W / 2), MidpointRounding.AwayFromZero);
                    m_cx = Math.Round((W / 2), 2);
                }

                //return (int)m_cx.Value;
                return m_cx.Value;
            }

        }
        //public int CY
        public double CY
        {
            get
            {
                if (false == m_cy.HasValue)
                {
                    //m_cy = Math.Round((H / 2), MidpointRounding.AwayFromZero);
                    m_cy = Math.Round((H / 2), 2);
                }

                //return (int)m_cy.Value;
                return m_cy.Value;
            }
        }

        private double? m_cx;
        private double? m_cy;
    }

    public class GridConverter
    {
        public GridConverter(int grid_w, int grid_h, int pixel_w, int pixel_h)
        {
            m_grid = new Rect(grid_w, grid_h);
            m_pixel = new Rect(pixel_w, pixel_h);
            m_gridBlock = new Rect((m_pixel.W / m_grid.W), (m_pixel.H / m_grid.H));   
        }

        public GridCoord PixelToGrid(float px, float py)
        {
            GridCoord grid = new GridCoord(); ;

            if (false == IsValid(px, py))
            {
                grid.IsValid = false;
                return grid;
            }

            grid.X = (int)(px / m_gridBlock.W);
            grid.Y = (int)(py / m_gridBlock.H);

            //grid.X = (int)Math.Round((px / m_gridBlock.W), MidpointRounding.AwayFromZero);
            //grid.Y = (int)Math.Round((py / m_gridBlock.H), MidpointRounding.AwayFromZero);

            grid.IsValid = true;
            return grid;
        }

        public Vector2 GridToPixel(int gx, int gy)
        {
            Vector2 pixel = new Vector2();

            pixel.x = (float)((gx * m_gridBlock.W) + m_gridBlock.CX);
            pixel.y = (float)((gy * m_gridBlock.H) + m_gridBlock.CY);

            return pixel;
        }

        private bool IsValid(float x, float y)
        {
            if ((m_pixel.W > x) 
            &&  (m_pixel.H > y)
            &&  (0 <= x)
            &&  (0 <= y))
            {
                return true;
            }

            return false;
        }

        readonly Rect m_grid = null;
        readonly Rect m_gridBlock = null;
        readonly Rect m_pixel = null;
    }
}
