using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ArenaServer.Game
{
    public class GridCoord
    {
        public GridCoord()
        {
        }

        public GridCoord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsSame(GridCoord coord)
        {
            return ((X == coord.X) && (Y == coord.Y));
        }

        public bool IsValid { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
