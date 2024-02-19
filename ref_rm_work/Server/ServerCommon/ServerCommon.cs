using System;

namespace RM.Server.Common
{
    public enum RequestArenaType
    {
        None = 0,
        Single,
        Raid,
    }

    public enum TimerDispatcherIDType : int
    {
        None = 0,
        ArenaObjectUpdate = 1,
        ArenaUpdate = 2,
        ArenaManagerUpdate = 3,
        PlayerRemove = 4,
        PlayerMoveArena = 5,
        PlayerAutoSkill = 6,
        CastKill = 7,
        PlayerHeartbeat = 8,
        MathingRequest = 9,
        InitDeploy,
        IntervalDeploy,
        Deploy,
        ProjectileUpdate,
        ProjectileRemove,

        Test_MatchingProcess = 1000,
    }

    //public enum DeployType
    //{
    //    None = 0,       // 테스트 용(배치 안함)
    //    Normal = 1,     // 일반 배치
    //    Regen = 2,      // 리젠
    //    Schedule = 3,   // 스케줄로 배치
    //}

    public enum RewardWho : byte
    {
        None = 0,
        LastHit,        //-막타 하신 분,
        Assist,         //-어시스트 해주신 분들,
        TeamInRange,    //-임의의 범위 내의 팀원 분들,
        Team            //-모든 팀원 분들, 
    }

    public enum RewardWhat : byte
    {
        None = 0,
        DeckPoint,
        Exp,
        Buff,
    }

    public enum RewardHow : byte
    {
        None = 0,
        Default,       //-1인당 받는 양 = (reward value)
        Divide,        //-1인당 받는 양 = (reward value / 받으실 분들 수)
    }
}
