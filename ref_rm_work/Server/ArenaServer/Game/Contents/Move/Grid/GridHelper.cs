using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using ArenaServer.Game.AI;

namespace ArenaServer.Game
{
    public class GridHelper
    {
        public GridHelper(GridMap gridMap, int mapWidth, int mapHeight)
        {
            m_gridMap = gridMap;
            m_converter = new GridConverter(gridMap.Width, gridMap.Height, mapWidth, mapHeight);
        }

        public bool IsSameGrid(Vector2 start, Vector2 end)
        {
            var gstart = m_converter.PixelToGrid(start.x, start.y);
            var gend = m_converter.PixelToGrid(end.x, end.y);

            return gstart.IsSame(gend);
        }

        public void UpdateOnMap(Guid uid, float x, float y)
        {
            GridCoord gridCoord = m_converter.PixelToGrid(x, y);
            m_gridMap.AddOrUpdate(uid, gridCoord.X, gridCoord.Y);
        }

        public void LeaveOnMap(Guid uid)
        {
            m_gridMap.Leave(uid);
        }

        public Vector2? PathFind(Vector2 start, Vector2 end)
        {
            GridCoord gstart = m_converter.PixelToGrid(start.x, start.y);
            GridCoord gend = m_converter.PixelToGrid(end.x, end.y);

            if ((false == gstart.IsValid) || (false == gend.IsValid))
            {
                Console.WriteLine("Critical!, pos was wrong start[{0}][{1}] end[{2}][{3}]", start.x, start.y, end.x, end.y);
                return null;
            }

            var node = m_gridMap.PathFind(gstart.X, gstart.Y, gend.X, gend.Y);
            if (null != node)
            {
                Vector2 target = m_converter.GridToPixel((int)node.Position.x, (int)node.Position.y);
                return target;
            }

            Console.WriteLine("Can't find path!! : {0} {1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount);
            return null;
        }

        GridMap m_gridMap = null;
        GridConverter m_converter = null;
    }
}
