using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace rm_login.Tool
{
    public class DrawObjectFactory
    {
        static public BaseDraw Create(ToolDefine.ObjectType objectType, Guid uid, float x, float y, float d, float sight, float atkRange)
        {
            BaseDraw baseDraw = null;

            switch(objectType)
            {
                case ToolDefine.ObjectType.Hero:
                    baseDraw = new DPlayer();
                    break;
                case ToolDefine.ObjectType.Player:
                    baseDraw = new DPlayer();
                    break;
                case ToolDefine.ObjectType.Npc:
                    baseDraw = new DNpc();
                    break;
                case ToolDefine.ObjectType.Projectile:
                    baseDraw = new DProjectile();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            if (null != baseDraw)
            {
                baseDraw.Initialize(uid, objectType, x, y, d, sight, atkRange);
            }

            return baseDraw;
        }
    }
}
