using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using AStarSharp;

namespace _02_astar_test_1.astar
{
    public class test
    {
        public void first()
        {
            Grid g = new Grid();
            g.Initialize(4, 4);

            //g.Marking(0, 3);

            Astar astar = new Astar(g.GetGrid);

            Vector2 start = new Vector2(0, 0);
            Vector2 end = new Vector2(3, 3);


            //g.Marking(0, 1);
            g.Marking(1, 1);
            g.Marking(1, 0);




            var stack = astar.FindPath(start, end);

            



            return;
        }

    }
}
