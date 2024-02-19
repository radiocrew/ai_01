using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using ArenaServer.Game.AI;

namespace ArenaServer.Game
{
    public class GridMap
    {
        public GridMap(int width, int height)
        {
            m_grid = new List<List<Node>>();

            for (int x = 0; x < width; ++x)
            {
                List<Node> list = new List<Node>();

                for (int y = 0; y < height; ++y)
                {
                    list.Add(new Node(x, y));
                }

                m_grid.Add(list);
            }

            Width = width;
            Height = height;

            m_objectCoord = new Dictionary<Guid, GridCoord>();
            m_pathFinder = new PathFinder(this);
        }

        public void AddOrUpdate(Guid uid, int gx, int gy)
        {
            lock (m_lock)
            {
                GridCoord oldCoord;
                GridCoord newCoord = new GridCoord(gx, gy);

                if (false == m_objectCoord.TryGetValue(uid, out oldCoord))
                {
                //-add
                    m_objectCoord.Add(uid, new GridCoord(gx, gy));
                    m_grid[gx][gy].Occupied += 1;
                }
                else
                {
                    if (oldCoord != newCoord)
                    {
                //-update
                        m_objectCoord[uid] = newCoord;

                        m_grid[oldCoord.X][oldCoord.Y].Occupied -= 1;
                        m_grid[newCoord.X][newCoord.Y].Occupied += 1;

                        if (0 > m_grid[oldCoord.X][oldCoord.Y].Occupied)
                        {
                            Debug.Assert(false);
                        }
                    }
                }
            }
        }

        public void Leave(Guid uid)
        {
            GridCoord coord;

            lock (m_lock)
            {
                if (true == m_objectCoord.TryGetValue(uid, out coord))
                {
                    m_grid[coord.X][coord.Y].Occupied -= 1;
                    if (0 > m_grid[coord.X][coord.Y].Occupied)
                    {
                        Debug.Assert(false);
                    }

                    m_objectCoord.Remove(uid);
                }
            }
        }

        public Node PathFind(int start_gx, int start_gy, int end_gx, int end_gy)
        {
            lock (m_lock)
            {
                var path = m_pathFinder.FindPath(start_gx, start_gy, end_gx, end_gy);
                if (null != path)
                {
                    Node node = path.Peek();
                    return node;
                }
            }
            return null;
        }

        //-외부에서 사용하지 마시오. (PathFinder 전용)
        public Node this[int x, int y]
        {
            get
            {
                Node ret = null;
                //lock (m_lock)
                {
                    ret = m_grid[x][y];
                }

                return ret;
            }
        }

        public int RowCount
        {
            get
            {
                return m_grid[0].Count;
            }
        }

        public int ColumnCount
        {
            get
            {
                return m_grid.Count;
            }
        }

        public int Width { get; set; }

        public int Height { get; set; }

    //-properties

        object m_lock = new object();

        readonly List<List<Node>> m_grid = null;
        Dictionary<Guid, GridCoord> m_objectCoord = null;
        PathFinder m_pathFinder = null;
    }
}
