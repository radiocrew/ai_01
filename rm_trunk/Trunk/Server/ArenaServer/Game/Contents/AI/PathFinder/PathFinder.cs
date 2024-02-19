using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace ArenaServer.Game.AI
{
    public class PathFinder
    {

        static int TEST_CASE_A_COUNT = 0;
        static int TEST_CASE_B_COUNT = 0;
        static int TEST_CASE_C_COUNT = 0;

        int GridRows
        {
            get
            {
                return m_map.RowCount;
            }
        }
        int GridCols
        {
            get
            {
                return m_map.ColumnCount;
            }
        }

        public PathFinder(GridMap grid)
        {
            m_map = grid;
        }

        public Stack<Node> FindPath(int start_gx, int start_gy, int end_gx, int end_gy)
        {
            Node start = new Node((start_gx / Node.NODE_SIZE), (start_gy / Node.NODE_SIZE));
            Node end = new Node((end_gx / Node.NODE_SIZE), (end_gy / Node.NODE_SIZE));

            if (false == m_map[(int)end.Position.x, (int)end.Position.y].Walkable)
            {
                Console.WriteLine("-case A : {0} count a[{1}]b[{2}]c[{3}]", 
                    System.DateTime.Now.ToString("hh:mm:ss.fff"), 
                    ++TEST_CASE_A_COUNT,
                    TEST_CASE_B_COUNT,
                    TEST_CASE_C_COUNT
                    );
                return null;
            }
                
            Stack<Node> Path = new Stack<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = start;

            // add start node to Open List
            OpenList.Add(start);

            while (OpenList.Count != 0 && !ClosedList.Exists(element => element.Position == end.Position))
            {
                current = OpenList[0];

                //-검사항목에서 가장 좋은 점수의 것을 삭제하고,
                OpenList.Remove(current);
                //-fix된 항목에 집어 넣는다. 
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);

                foreach (Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.Walkable)
                    {
                        if (!OpenList.Contains(n))
                        {
                            n.Parent = current;

                            //-it seems menhatan distance
                            n.DistanceToTarget = Math.Abs(n.Position.x - end.Position.x) + Math.Abs(n.Position.y - end.Position.y);
                            n.Cost = n.Weight + n.Parent.Cost;
                            OpenList.Add(n);

                            //-타겟으로 부터 가장 근접한(가장 좋은 점수), F가 낮은 것이 index(0)위치에 온다. (오름차순)
                            OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (false == ClosedList.Exists(x => x.Position == end.Position))
            {

                //Console.WriteLine("*********     case b : {0} {1} {2}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount, ClosedList.Count);
                Console.WriteLine("-case B : {0} count a[{1}]b[{2}]c[{3}]",
                    System.DateTime.Now.ToString("hh:mm:ss.fff"),
                    TEST_CASE_A_COUNT,
                    ++TEST_CASE_B_COUNT,
                    TEST_CASE_C_COUNT
                    );
                //-작성한 path에 도착점이 없네..
                return null;
            }

            //-closed list 
            //  0 : start, index(array - 1) : end

            // if all good, return path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            if (temp == null)
            {
                //Console.WriteLine("*********     case c : {0} {1} {2}", System.DateTime.Now.ToString("hh:mm:ss.fff"), Environment.TickCount, ClosedList.Count);
                Console.WriteLine("-case C : {0} count a[{1}]b[{2}]c[{3}]", 
                    System.DateTime.Now.ToString("hh:mm:ss.fff"),
                    TEST_CASE_A_COUNT, 
                    TEST_CASE_B_COUNT, 
                    ++TEST_CASE_C_COUNT);
                return null;
            }

            do
            {
                Path.Push(temp);
                temp = temp.Parent;
            } while (temp != start && temp != null);
            return Path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> temp = new List<Node>();

            /*
                ○ ● ● ●
                │ │ │ │
                ○ ● ● ●
                │ │ │ │
                ○-●-●-●

                ○ 갯수 : row count

                ○-●-●-● : column count
            */
            int row = (int)n.Position.y;
            int col = (int)n.Position.x;

            if (row + 1 < GridRows)//12
            {
                if (true == m_map[col, row + 1].Walkable)
                {
                    temp.Add(m_map[col, row + 1]);
                }
            }
            if ((row + 1 < GridRows) && (col + 1 < GridCols))
            {
                if (true == m_map[col + 1, row + 1].Walkable)
                {
                    temp.Add(m_map[col + 1, row + 1]);
                }
            }
            if (col + 1 < GridCols)//3
            {
                if (true == m_map[col + 1, row].Walkable)
                {
                    temp.Add(m_map[col + 1, row]);
                }
            }
            if ((col + 1 < GridCols) && (row - 1 >= 0))
            {
                if (true == m_map[col + 1, row - 1].Walkable)
                {
                    temp.Add(m_map[col + 1, row - 1]);
                }
            }
            if (row - 1 >= 0)//6
            {
                if (true == m_map[col, row - 1].Walkable)
                {
                    temp.Add(m_map[col, row - 1]);
                }
            }
            if ((row - 1 >= 0) && (col - 1 >= 0))
            {
                if (true == m_map[col - 1, row - 1].Walkable)
                {
                    temp.Add(m_map[col - 1, row - 1]);
                }
            }
            if (col - 1 >= 0)//9
            {
                if (true == m_map[col - 1, row].Walkable)
                {
                    temp.Add(m_map[col - 1, row]);
                }
            }
            if ((col - 1 >= 0) && (row + 1 < GridRows))
            {
                if (true == m_map[col - 1, row + 1].Walkable)
                {
                    temp.Add(m_map[col - 1, row + 1]);
                }
            }

            return temp;
        }

        GridMap m_map;
    }
}
