using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Common;
using RM.Net;
using RM.Server.Common;

using ArenaServer.Net;
using ArenaServer.Resource;
using ArenaServer.Game.AI;


namespace ArenaServer.Game
{
    public partial class ArenaObject
    {
        public virtual void OnUpdate(int id, double accumT, double dT)
        {
            //Console.WriteLine("Object called : [{0}] accumt[{1}] dt[{2}]", System.DateTime.Now.ToString("hh:mm:ss.fff"), accumT, dT);


        }

        public virtual void ChangeAiState(AiStateType stateType)
        {
        }

        public virtual void Look(Vector2 lookPos)
        {
            var ownPos = m_movement.Position;
            m_movement.Direction = RMMath.VecToDeg(lookPos - ownPos);
        }

        public virtual void OnTakeDamage(ArenaObject attacker, int skillID, int damage, int hp)
        {
            DamageNtf(damage, hp, skillID, (null != attacker) ? attacker.ArenaObjectType : ArenaObjectType.None);
        }

        //public bool TakeDamage(int damage, ArenaObject attacker, ResData_Skill jsonSkill)
        public bool TakeDamage(int damage, ArenaObject attacker, int skillID)
        {
            int hp = 0;

            if (true == HPSync(-damage, out hp))
            {
                //OnTakeDamage(attacker, jsonSkill.SkillID, damage, hp);
                OnTakeDamage(attacker, skillID, damage, hp);

                if (0 >= hp)
                {
                    OnDie();
                }

                return true;
            }

            return false;
        }

        public bool IsAttackable(ArenaObject targetObject)
        {
            if (this.UID == targetObject.UID)
            {
                return false;
            }

            if (ArenaObjectType == targetObject.ArenaObjectType)
            {
                return false;
            }

            if (true == targetObject.IsDead())
            {
                return false;
            }

            return true;
        }

        public void CastSkill(int skillID, SkillParam skillParam)
        {
            Skill skill = SkillManager.Instance.FindSkill(skillID);
            if (null == skill)
            {
                Console.WriteLine("Can't cast, unknown skill id {0}", skillID);
                return;
            }

            if ((true == skill.JsonSkill.CastLookAtTarget) && (null != skillParam.Target))
            {
                Look(skillParam.Target.Position);
            }

            DelayedTask dt = new DelayedTask(() => {
                    skill.DoAction(m_arena, this, skillParam);

                    var invokeSkillNoti = new INVOKE_SKILL_NTF();
                    invokeSkillNoti.UID = UID;
                    invokeSkillNoti.SkillID = skillID;
                    invokeSkillNoti.MovementData = Movement.SerializeData();
                    invokeSkillNoti.SkillParamData = skillParam.SerializeData();
                    m_arena.BroadCasting(invokeSkillNoti);
                });
            dt.Submit(skill.JsonSkill.InvokeTimeMS, (int)TimerDispatcherIDType.CastKill);

            CAST_SKILL_NTF castSkillNoti = new CAST_SKILL_NTF();
            castSkillNoti.UID = UID;
            castSkillNoti.SkillID = skillID;
            castSkillNoti.MovementData = m_movement.SerializeData();
            castSkillNoti.CastDirection = skillParam.Direction;
            m_arena.BroadCasting(castSkillNoti, UID);
        }

        public void CastSkill(int skillID, ArenaObject targetObject)
        {
            var skillParam = new SkillParam();
            skillParam.Target = targetObject;

            CastSkill(skillID, skillParam);
        }

        public void DamageNtf(int damage, int hp, int skillID, ArenaObjectType attackerType)
        {
            DAMAGE_NTF packet = new DAMAGE_NTF();
            packet.UID = UID;
            packet.SkillID = skillID;
            packet.AttackerType = attackerType;
            packet.Damage = damage;
            packet.HealthData = new HEALTH_DATA();
            packet.HealthData.Hp = hp;//-현재 hp 가 아닌, 쳐맞았을 당시(동기화 된)의 hp 를 적용한다.
            packet.HealthData.HpMax = Health.HPMax;
            m_arena.BroadCasting(packet);
        }

        public void HealthNtf()
        {
            HEALTH_NTF ntf = new HEALTH_NTF();
            ntf.UID = UID;
            ntf.HealthData = Health.SerializeData();

            if (null != m_arena)
            {
                m_arena.BroadCasting(ntf);
            }
        }

        public void StatusNtf()
        {
            STATUS_NTF ntf = new STATUS_NTF();
            ntf.UID = UID;
            ntf.StatusData = Status.SerializeData();

            if (null != m_arena)
            {
                m_arena.BroadCasting(ntf);
            }
        }

        public void StatNtf()
        {
            STAT_NTF ntf = new STAT_NTF();
            ntf.UID = UID;
            ntf.StatData = Stat.SerializeData();

            if (null != m_arena)
            {
                m_arena.BroadCasting(ntf);
            }
        }

        protected bool HPSync(int adjust, out int hp)
        {
            hp = 0;

            lock (m_hpSyncLock)
            {
                if (0 == adjust)
                {
                    return false;
                }

                if (true == Status.Have(StatusType.Dead))
                {
                    return false;
                }

                hp = Health.IncreaseHP(adjust);

                if (0 >= hp)
                {
                    Status.Add(StatusType.Dead);
                    StatusNtf();
                }
            }

            return true;
        }

        public bool HPResetSync(int adjust)
        {
            if (0 >= adjust)
            {
                return false;
            }

            lock (m_hpSyncLock)
            {
                Health.ResetHP(adjust);
            }

            return true;
        }

        public virtual bool IncreaseExp(long exp)
        {
            return false;
        }

        public virtual bool IncreaseLevel(int delta = 1)
        {
            return false;
        }

        //public virtual void CalculateStat(bool ntf = false)
        //{
        //}

        //-dead 상태와 hp 와의 동기화를 위한 lock
        object m_hpSyncLock = new object();
        
    }
}


