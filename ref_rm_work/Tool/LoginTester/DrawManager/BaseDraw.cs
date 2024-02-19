using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using UnityEngine;

namespace rm_login.Tool
{
    public class BaseDraw
    {
        public virtual bool Initialize(Guid uid, ToolDefine.ObjectType objectType, float x, float y, float d, float sight, float atkRange)
        {
            m_uid = uid;
            m_sight = sight;
            m_atkRange = atkRange;

            m_undraw_list = new Queue<Vector2>();

            switch (objectType)
            {
                case ToolDefine.ObjectType.Hero:
                    m_objectBrush = Brushes.GreenYellow;
                    break;
                case ToolDefine.ObjectType.Player:
                    m_objectBrush = Brushes.Blue;
                    break;
                case ToolDefine.ObjectType.Npc:
                    m_objectBrush = Brushes.Red;
                    break;
                case ToolDefine.ObjectType.Projectile:
                    m_objectBrush = Brushes.Orange;
                    break;
                default:
                    Debug.Assert(false);
                    return false;
            }

            m_eraseBrush = Brushes.Black;
           
            UpdatePosition(x, y);
            return true;
        }

        public virtual void Destroy()
        {
            lock (m_pos_lock)
            {
                m_undraw_list.Enqueue(new Vector2(m_cur_x, m_cur_y));
            }
        }

        public virtual void UpdatePosition(float x, float y)
        {
            lock (m_pos_lock)
            {
                if ((m_cur_x != x) || (m_cur_y != y))
                {
                    m_old_x = m_cur_x;
                    m_cur_x = x;

                    m_old_y = m_cur_y;
                    m_cur_y = y;
                }

                if ((m_old_x != m_cur_x) || (m_old_y != m_cur_y))
                {
                    var oldPos = new Vector2(m_old_x, m_old_y);
                    m_undraw_list.Enqueue(oldPos);
                }

                //엥피씨는 d 안검사, 플레이어, 히어로는 d 검사
            }
        }

        public void Draw(System.Drawing.Graphics graphics)
        {
            float x = 0.0f;
            float y = 0.0f;

            lock (m_pos_lock)
            {
                x = m_cur_x;
                y = m_cur_y;
            }

            OnDrawObject(graphics, x, y);
        }

        public void UnDraw(System.Drawing.Graphics graphics)
        {
            Queue<Vector2> undrawList = null;

            lock (m_pos_lock)
            {
                undrawList = new Queue<Vector2>(m_undraw_list);
                m_undraw_list.Clear();
            }

            while (0 < undrawList.Count)
            {
                var oldPos = undrawList.Dequeue();
                OnDrawObject(graphics, oldPos.x, oldPos.y, true);
            }
        }

        public virtual void OnDrawObject(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
        }

        public virtual void DrawSight(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
        }

        public virtual void DrawAtkRange(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
        }

        public float X
        {
            get
            {
                lock (m_pos_lock)
                {
                    return m_cur_x;
                }
            }
        }

        public float Y
        {
            get
            {
                lock (m_pos_lock)
                {
                    return m_cur_y;
                }
            }
        }

        public Guid UID => m_uid;
        
        Guid m_uid;

        protected System.Drawing.Brush m_eraseBrush = null;
        protected System.Drawing.Brush m_objectBrush = null;

        object m_pos_lock = new object();

        float m_old_x = -1.0f;
        float m_old_y = -1.0f;

        float m_cur_x = -1.0f;
        float m_cur_y = -1.0f;

        protected float m_sight = 0.0f;
        protected float m_atkRange = 0.0f;

        Queue<UnityEngine.Vector2> m_undraw_list;
    }
}
