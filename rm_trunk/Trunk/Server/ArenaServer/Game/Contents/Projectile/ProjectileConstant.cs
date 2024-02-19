using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using Level9.Expedition.Mobile.Logging;

using RM.Common;
using RM.Net;
using RM.Server.Common;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class ProjectileConstant : ProjectileBase, RMCollision
    {
        static readonly int TimeDevide = 5;
        static readonly float TimeDevideDT = 1.0f / TimeDevide;

        public ProjectileConstant(Arena arena, ArenaObject owner, ResData_Skill jsonSkill, ResJson_ProjectileConstant jsonProjectile, float direction)
            : base(arena, owner, jsonSkill)
        {
            m_jsonProjectile = jsonProjectile;
            m_startPos = owner.Position;
            m_direction = direction;
            m_speed = jsonProjectile.Speed;
            m_lifeTime = jsonProjectile.LifeTime;

            m_collision = CollisionBase.CreateCollision(jsonProjectile.Collision);

            Position = m_startPos;
        }

        public override bool Initialize()
        {
            if (true == base.Initialize())
            {
                m_hitObjects = new ConcurrentBag<Guid>();

                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, "[projectile] created...");
                return true;
            }

            return false;
        }

        public override PROJECTILE_DATA SerializedData()
        {
            PROJECTILE_DATA data = new PROJECTILE_DATA();
            data.UID = UID;
            data.ProjectileID = m_jsonProjectile.ProjectileID;
            data.StartPos = new NVECTOR3(m_startPos.x, 0.0f, m_startPos.y);
            data.Direction = m_direction;

            return data;
        }

        //public override bool IsRemoved()
        //{
        //    return true;
        //}

        public CollisionBase GetCollision()
        {
            if (null != m_collision)
            {
                m_collision.Position = Position;
            }

            return m_collision;
        }

        public override void OnUpdate(int id, double accumT, double dT)
        {
            if (true == Stop)
            {
                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, "[projectile] call again again again again again ?????");
                return;
            }

            float ddT = (float)dT * TimeDevideDT;

            for (int i = 0; i < TimeDevide; ++i)
            {
                UpdateTimeDivide(id, accumT, ddT);
            }
        }

        private void UpdateTimeDivide(int id, double accumT, float dT)
        {
            base.OnUpdate(id, accumT, dT);
            
            Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, "[projectile x[{0}], y[{1}]", Position.x, Position.y);

            var targetObjects = TargetFilter.GetTargets(m_arena, Position, m_jsonProjectile.TargetFilterType, (targetObject) => {

                if (true == m_hitObjects.Contains(targetObject.UID))
                {
                    return false;
                }

                if (false == m_owner.IsAttackable(targetObject))
                {
                    return false;
                }

                return CollisionBase.Collision(this, targetObject);
            });

            foreach (var targetObject in targetObjects)
            {
                m_hitObjects.Add(targetObject.UID);
                Hit(targetObject);
            }

            var vector = RMMath.AngleToVector2(m_direction) * (m_speed * dT);
            Position += vector;

            m_lifeTime -= dT;
            if (0.0f >= m_lifeTime)
            {
                m_lifeTime = 0.0f;

                Stop = true;
                Remove(UID);

                Level9.Expedition.Mobile.Logging.Logger.Log(LogLevel.Info, "[projectile] removed...");
            }
        }

        private void Hit(ArenaObject targetObject)
        {
            var dmg = FormulaManager.CalcDamage(m_owner, m_jsonSkill);
            targetObject.TakeDamage(dmg, m_owner, m_jsonSkill.SkillID);

            if (TargetFilterType.NearSingle == m_jsonProjectile.TargetFilterType)
            {
                PROJECTILE_REMOVE_NTF packet = new PROJECTILE_REMOVE_NTF();
                packet.UID = UID;
                packet.Hit = true;
                packet.HitPosition = new NVECTOR3(Position.x, 0.0f, Position.y);
                m_arena.BroadCasting(packet);

                Stop = true;
                Remove(UID);
            }
        }

        private void Remove(Guid uid)
        {
            AsyncTask.Execute(() => {
                m_arena.RemoveProjectile(UID);
            }, TimerDispatcherIDType.ProjectileRemove);
        }

        readonly ResJson_ProjectileConstant m_jsonProjectile;
        readonly Vector2 m_startPos;
        readonly float m_direction;
        readonly float m_speed;
        readonly CollisionBase m_collision;

        float m_lifeTime = 0.0f;
        ConcurrentBag<Guid> m_hitObjects = null;
    }
}
