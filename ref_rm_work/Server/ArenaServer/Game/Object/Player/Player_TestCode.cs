using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;

namespace ArenaServer.Game
{
    public partial class PlayerTestCode
    {
        static public Vector2 Position(Vector2 currentPos, float width, float height)
        {
            System.Random rand = new System.Random();
            Vector2 position = new Vector2(currentPos.x + rand.Next(-10, 10), currentPos.y + rand.Next(-10, 10));

            position.x = RMMath.Clamp(position.x, 0.0f, width);
            position.y = RMMath.Clamp(position.y, 0.0f, height);

            return position;
        }
        static public float Direction()
        {
            System.Random rand = new System.Random();
            return rand.Next(0, 360); ;
        }
    }
}
