using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Level9.Expedition.Mobile.Util;

using ArenaServer.Resource;

namespace ArenaServer.Game.AI
{
    public class AiFSMFactory : Singleton<AiFSMFactory>
    {
        public bool Initialize()
        {
            return true;
        }

        public bool Build(int aiBagID, AiFSM fsm, BlackBoard blackBoard)
        {
            var bag = ResourceManager.Instance.FindNpcAiBag(aiBagID);
            if (null != bag)
            {
                if (true == bag.StateTypeList.All(element =>
                {
                    AiStateType aiStateType = (AiStateType)element;

                    var state = Create(aiStateType);
                    if (null != state)
                    {
                        state.Initialize(blackBoard);
                        fsm.AddState(aiStateType, state);
                        return true;
                    }

                    return false;
                }))
                {
                    //-set first state
                    fsm.ChangeState((AiStateType)bag.FirstStateType);
                    return true;
                }

                return false;
            }

            return false;
        }

        private T Newer<T>(ResJson_NpcAiState state)
        {
            return (T)Activator.CreateInstance(typeof(T), state.Arg_0, state.Arg_1, state.Arg_2, state.Arg_3);
        }

        private AiState Create(AiStateType type)
        {
            AiState ret = null;

            var resAiState = ResourceManager.Instance.FindNpcAiState((int)type);
            if (null == resAiState)
            {
                return null;
            }

            switch (type)
            {
                case AiStateType.Idle:
                    {
                        ret = Newer<StateIdle>(resAiState);
                    }
                    break;
                case AiStateType.Wander:
                    {
                        ret = Newer<StateWander>(resAiState);
                    }
                    break;
                case AiStateType.Chase:
                    {
                        ret = Newer<StateChase>(resAiState);
                    }
                    break;
                case AiStateType.Attack:
                    {
                        ret = Newer<StateAttack>(resAiState);
                    }break;
                case AiStateType.Chase_Test:
                    {
                        ret = Newer<StateChase_Test>(resAiState);
                    }break;
                default:
                    {
                        Debug.Assert(false);
                    }break;
            }
        
            return ret;
        }

        private AiFSMFactory()
        {
        }
    }
}
