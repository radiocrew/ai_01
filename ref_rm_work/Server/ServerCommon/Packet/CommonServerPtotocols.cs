namespace RM.Server.Common
{
    // RM Protocols
    public enum ServerProtocols : ushort
    {
        PID_NONE = 0,

        S_ARENA_MATCHING_REQ,
        S_ARENA_MATCHING_ACK,

        S_ARENA_MATCHING_COMPLETE_NTF,
        S_ARENA_MATCHING_CANCEL,

        S_ARENA_CREATE_REQ,
        S_ARENA_CREATE_ACK,
    }
}
