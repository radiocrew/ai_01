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
    //    None = 0,       // �׽�Ʈ ��(��ġ ����)
    //    Normal = 1,     // �Ϲ� ��ġ
    //    Regen = 2,      // ����
    //    Schedule = 3,   // �����ٷ� ��ġ
    //}

    public enum RewardWho : byte
    {
        None = 0,
        LastHit,        //-��Ÿ �Ͻ� ��,
        Assist,         //-��ý�Ʈ ���ֽ� �е�,
        TeamInRange,    //-������ ���� ���� ���� �е�,
        Team            //-��� ���� �е�, 
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
        Default,       //-1�δ� �޴� �� = (reward value)
        Divide,        //-1�δ� �޴� �� = (reward value / ������ �е� ��)
    }
}
