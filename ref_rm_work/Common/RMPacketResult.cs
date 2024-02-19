using System.Collections.Generic;
using ProtoBuf;

namespace RM.Net
{
    // 공용적인 Result

    public enum PacketResult : byte
    {
        Success = 0,
        Fail,
        Fail_UID_NullOrEmpty,
        Fail_ConnectFail,
        Fail_ServerLimit,
        Fail_ServerClose,
        Fail_Retry,             // 다시 시도
        Fail_CantFindArena,
        Fail_CantCreateArena,
        Fail_InArenaAlready,
        Fail_InvaildArenaID,
        Fail_MatchingRequestAlready,
    }
};

