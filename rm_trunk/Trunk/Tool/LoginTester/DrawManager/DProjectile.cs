using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;

using rm_login.Tool.Script;

namespace rm_login.Tool
{
    public class DProjectile : BaseDraw
    {
        object m_deltaLock = new object();
        long LastTime = 0;
        double GetDeltaTime()
        {
            lock (m_deltaLock)
            {
                long now = DateTime.Now.Ticks;
                double dT = (now - LastTime);

                if (0 == LastTime)
                {
                    LastTime = now;
                    return 0.1f;
                }

                LastTime = now;

                dT /= 10000000;
                var ret = Math.Round((double)dT, 3);
                return ret;
            }
        }

        public override bool Initialize(Guid uid, ToolDefine.ObjectType objectType, float x, float y, float d, float sight, float atkRange)
        {
            if (true == base.Initialize(uid, objectType, x, y, d, sight, atkRange))
            {
                m_direction = d;
                return true;
            }

            return false;
        }

        public override void Destroy()
        {
            base.Destroy();

            UpdateStop();
        }

        public override void OnDrawObject(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
            float object_x = x;
            float object_y = y;

            MapManager.Instance.PositionOnMap(x, y, out object_x, out object_y);

            float radius_x = MapManager.Instance.MapScaleW * ToolDefine.TEST_OBJECT_RADIUS;
            float radius_y = MapManager.Instance.MapScaleH * ToolDefine.TEST_OBJECT_RADIUS;

            object_x -= radius_x;
            object_y -= radius_y;

            var brush = (true == remove) ? (m_eraseBrush) : (m_objectBrush);
            graphics.FillEllipse(brush, object_x, object_y, radius_x * 2, radius_y * 2);

            DrawSight(graphics, x, y, remove);
        }

        public override void DrawSight(System.Drawing.Graphics graphics, float x, float y, bool remove = false)
        {
        }

        public bool LoadJson(int projectileID)
        {
            var jsonProjectile = ResourceManager.Instance.FindProjectileConstant(projectileID);
            if (null != jsonProjectile)
            {
                m_speed = jsonProjectile.Speed;
                m_lifeTime = jsonProjectile.LifeTime;
                return true;
            }
            
            return false;
        }

        public void Fire()
        {
            m_updateTimer = new System.Threading.Timer(new System.Threading.TimerCallback(UpdateCallBack));
            m_updateTimer.Change(1, 200);
        }

        private void OnUpdate(float dt)
        {
            float ddt = dt * 0.2f;

            for (int i = 0; i < 5; ++i)
            {
                OnUpdateDivide(ddt);
            }
        }

        private void OnUpdateDivide(float dt)
        {
            if (true == m_stop)
            {
                return;
            }

            var vector = RMMath.AngleToVector2(m_direction) * (m_speed * dt);

            Vector2 pos = new Vector2(X, Y);
            pos += vector;

            UpdatePosition(pos.x, pos.y);

            if (0.0f >= m_lifeTime)
            {
                LogDelegate.Instance.Log("projectile life time over");

                UpdateStop();

                Task.Run(() => {
                    DrawManager.Instance.UnRegist(UID);
                });
            }

            m_lifeTime -= dt;
        }

        private void UpdateCallBack(object state)
        {
            OnUpdate((float)GetDeltaTime());
        }

        private void UpdateStop()
        {
            m_stop = true;
            m_updateTimer.Dispose();
        }

        System.Threading.Timer m_updateTimer = null;
        private float m_direction = 0.0f;
        private float m_speed = 0.0f;
        private float m_lifeTime = 0.0f;

        private volatile bool m_stop = false;
    }
}
