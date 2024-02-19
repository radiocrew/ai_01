namespace RM.Common
{
    // RM Protocols
    public enum RMProtocols : ushort
    {
        PID_NONE = 0,                           // None
        HEART_BEAT,
        SC_ECHO_REQ,                            // 에코 요청
        TEST_COMMAND,                           // 테스트 커맨드
        ARENA_HELLO_NTF,

        //--------------------------------------------------------------------------------------
        //  Arena Server <-> Client
        ARENA_LOGIN_REQ,                        // [CS] Arena 서버에 로그인
        ARENA_LOGIN_ACK,


        //--------------------------------------------------------------------------------------
        // 맵, 서버 이동 관련 
        ARENA_ENTER_PLAYER_NTF,                 // [SC] 아레나에 처음 진입시 처음 보내는 패킷
        ARENA_ENTER_OBJECT_NTF,                 // [SC] 오브젝트 
        ARENA_LEAVE_OBJECT_NTF,

        ARENA_MOVE_REQ,                         // [CS] 아레나 이동 요청
        ARENA_MOVE_ACK,                         // [SC] 아레나 이동 응답 (Ok 갈수있어, 이후->MAP_ENTER_PLAYER_NTF)
        MATCHED_ARENA_MOVE_REQ,                 // [CS] 매칭된 아레나로 이동 요청
        MATCHED_ARENA_MOVE_ACK,                 // [SC] 

        //--------------------------------------------------------------------------------------
        // 매칭
        ARENA_MATCHING_REQ,
        ARENA_MATCHING_CANCEL,
        ARENA_MATCHING_ACK, 
        ARENA_MATCHING_COMPLETE_NTF,


        //-원격지 에서 만들떄 
        SERVER_INSTANCE_MOVE_NTF,               // [SC] Arena 서버 변경

        HERO_MOVEMENT_NTF,                      // [CS] 움직임 
        START_ATTACK,                           // [CS] 공격 On
        STOP_ATTACK,                            // [CS] 공격 Off
        CAST_SKILL,                             // [CS] 1회 
        CAST_SKILL_NTF,                         // [SC] Skill Cast 노티
        INVOKE_SKILL_NTF,

        CAST_SKILL_PROJECTILE_NTF,              // [SC] 발사체 발사!
        PROJECTILE_REMOVE_NTF,                  // [SC] 발사체 사라짐..

        DAMAGE_NTF,                             // [SC] 대미지 노티
        STATUS_NTF,                             // [SC] Status Ntf
        STAT_NTF,                               // [SC] 
        NPC_MOVEMENT_NTF,                       // [SC] NPC 움직임 노티
        HEALTH_NTF,                             // [SC] HP 동기화

        ACTIVE_LEVEL_NTF,

        //-
        HERO_REVIVE_REQ,                        // [CS] 히어로 부활 요청
        HERO_REVIVE_ACK,                        //

        //-deck from layton,
        DECK_USE_REQ,                           // [CS] 덱 사용
        DECK_SELECT_REQ,                        // [CS] 덱 선택
        DECK_GEN_REQ,                           // [CS] 랜덤 덱 요청
        DECK_NTF,                               // [SC] Deck 정보

        //-인벤쵸릐, 아이템
        INVENTORY,
        INVENTORY_ITEM_UPDATE_NTF,
        INVENTORY_ITEM_DELETE,
        ITEM_EQUIP,



    }
}
