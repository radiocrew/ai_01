using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using AStarSharp;

namespace _02_astar_test_1.astar
{
    public class Grid
    {
        public void Initialize(int w, int h)
        {
            m_grid = new List<List<Node>>();

            for (int x = 0; x < w; ++x)
            {
                List<Node> list = new List<Node>();

                for (int y = 0; y < h; ++y)
                {
                    list.Add(new Node(new Vector2(x, y), true));
                }

                m_grid.Add(list);
            }
        }

        public void Marking(int x, int y)
        {
            (m_grid[x])[y].Walkable = false;
        }

        public List<List<Node>> GetGrid
        {
            get
            {
                return m_grid;
            }            
        }

        List<List<Node>> m_grid;
    }
}
