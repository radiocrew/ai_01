using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RM.Net;

using ArenaServer.Resource;

namespace ArenaServer.Game.AI
{
    public class BlackBoard
    {
        public BlackBoard(ArenaObject arenaObject)
        {
            m_arenaObject = arenaObject;
            m_skillSelector = new SkillSelector();
        }

        public bool Initialize(List<ResData_NpcSkill> resSkillList)
        {
            m_skillSelector.Initialize(resSkillList);
            return true;
        }

        public ArenaObject TargetObject
        {
            get
            {
                ArenaObject ret = null;
                lock (m_targetObjectLock)
                {
                    ret = m_targetObject;
                }
                return ret;
            }
            set
            {
                lock (m_targetObjectLock)
                {
                    m_targetObject = value;
                }
            }
        }

        public ArenaObject Owner 
        {
            get => m_arenaObject;
        }

        public Vector2 TargetPosition //-testcode, lock처리 고려
        {
            get
            {
                Vector2 ret;
                lock (m_targetPosLock)
                {
                    ret = m_targetPos;
                }
                return ret;
            }
            set 
            {
                lock (m_targetPosLock)
                {
                    m_targetPos = value;
                }
            }
        }

        public void TargetPositionClear()
        {
            lock (m_targetPosLock)
            {
                m_targetPos.x = 0.0f;
                m_targetPos.y = 0.0f;
            }
        }

        public SkillSelector SkillSelector => m_skillSelector;

        private readonly ArenaObject m_arenaObject = null;

        private object m_targetPosLock = new object();
        private Vector2 m_targetPos;

        private object m_targetObjectLock = new object();
        private ArenaObject m_targetObject = null;

        private SkillSelector m_skillSelector = null;
    }
}
