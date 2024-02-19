using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ArenaServer.Game.AI
{
    public class Node
    {
        // Change this depending on what the desired size is for each element in the grid
        //public static int NODE_SIZE = 32;
        public static int NODE_SIZE = 1;
        public Node Parent;
        public Vector2 Position;

        public Vector2 Center
        {
            get
            {
                return new Vector2(Position.x + NODE_SIZE / 2, Position.y + NODE_SIZE / 2);
            }
        }
        public float DistanceToTarget;
        public float Cost;
        public float Weight;
        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }

        private int m_occupied = 0;

        public int Occupied
        {
            get
            {
                return m_occupied;
            }
            set
            {
                m_occupied = value;
            }
        }

        public bool Walkable
        {
            get
            {
                return (0 >= Occupied);
            }
        }

        public Node(int x, int y, float weight = 1)
        {
            Parent = null;
            Position = new Vector2(x, y);
            DistanceToTarget = -1;
            Cost = 1;
            Weight = weight;

            //-생성자 초기화에서는 모든 노드가 이동 가능으로 고정됨.
            Occupied = 0;
        }
    }
}
