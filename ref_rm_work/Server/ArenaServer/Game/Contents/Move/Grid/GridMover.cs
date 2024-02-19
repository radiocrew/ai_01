using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;

using ArenaServer.Game.AI;

namespace ArenaServer.Game
{
    public class GridMover
    {
        public GridMover()
        {
        }

        public bool Initialize(float speed, GridMap gridMap, Vector2 pos, int mapWidth, int mapHeight)
        {
            m_speed = speed;
            m_gridHelper = new GridHelper(gridMap, mapWidth, mapHeight);
            return true;
        }

        public Vector2 Position
        {
            get
            {
                lock (m_lock)
                {
                    return m_currentPos;
                }
            }
        }
        public Vector2 TargetPosition
        {
            get
            {
                lock (m_lock)
                {
                    return m_targetPos;
                }
            }
        }

        public void SetPosition(Guid uid, Vector2 pos)
        {
            lock (m_lock)
            {
                m_dt = 0.0f;
                m_arrival = true;
                m_takeTime = 0.0f;

                m_pastPos = pos;
                m_targetPos = pos;
                m_currentPos = pos;

                m_gridHelper.UpdateOnMap(uid, m_currentPos.x, m_currentPos.y);
            }
        }

        public bool Move(Vector2 toPos)
        {
            lock (m_lock)
            {
                m_targetPos = m_currentPos;

                if (true == m_gridHelper.IsSameGrid(m_currentPos, toPos))
                {
                    m_targetPos = toPos;
                }
                else
                {
                    var target = m_gridHelper.PathFind(m_currentPos, toPos);
                    if (null == target)
                    {
                        return false;
                    }

                    m_targetPos = target.Value;
                    //Console.WriteLine("**Last path target {0}, {1}", target.Value.x, target.Value.y);
                }

                m_dt = 0.0f;
                m_pastPos = m_currentPos;
                
                var vector = m_targetPos - m_currentPos;
                float distance = vector.magnitude;
                m_takeTime = distance / m_speed;

                //Console.WriteLine("**Last info : distance[{0}], take time[{1}]", distance, m_takeTime);
                m_arrival = false;
            }

            return true;
        }

        public void Update(Guid uid, float dt)
        {
            lock (m_lock)
            {
                if (true == m_arrival)
                {
                    return;
                }

                m_dt += dt;

                if (m_takeTime <= m_dt)
                {
                    m_dt = m_takeTime;
                    m_pastPos = m_currentPos = m_targetPos;
                    m_arrival = true;

                    m_gridHelper.UpdateOnMap(uid, m_currentPos.x, m_currentPos.y);
                    return;
                }

                m_currentPos = RMMath.Lerp(m_pastPos, m_targetPos, m_dt / m_takeTime);
                m_gridHelper.UpdateOnMap(uid, m_currentPos.x, m_currentPos.y);
            }
        }
        public void Destory(Guid uid)
        {
            lock (m_lock)
            {
                m_gridHelper.LeaveOnMap(uid);
            }
        }

        object m_lock = new object();

        float m_speed = 0.0f;
        float m_dt = 0.0f;
        bool m_arrival = false;
        float m_takeTime = 1.0f;

        Vector2 m_pastPos;
        Vector2 m_targetPos;
        Vector2 m_currentPos;

        GridHelper m_gridHelper = null;
    }
}
