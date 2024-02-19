using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Level9.Expedition.Mobile.Util;

using RM.Common;

namespace ArenaServer.Resource
{
    public partial class ResourceManager : Singleton<ResourceManager>
    {
        private ResourceManager()
        {
        }

        public bool Initialize(string resourceDirectory)
        {
            m_resourceValid.Initialize();
            m_ContentRootDirectory = resourceDirectory; 

            if (false == LoadDBFromFile())
            {
                Debug.Assert(false);
                return false;
            }

            Build();
            return true;
        }

        public void Release()
        {
        }

        public bool LoadDBFromFile()
        {
            if (false == LoadJson<ResJson_Arena>("db_arena.json", Parsing))
            {
                return false;
            }
            //if (false == LoadJson<ResJson_ArenaObject>("db_arenaobject.json", Parsing))
            //{
            //    return false;
            //}
            if (false == LoadJson<ResJson_Collision>("db_Collision.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_Deploy>("db_deploy.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_Map>("db_map.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_Player>("db_player.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_PlayerBaseStat>("db_player_base_stat.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_PlayerStat>("db_player_stat.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_Npc>("db_npc.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_NpcAiState>("db_npc_ai_state.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_NpcAiBag>("db_npc_ai_bag.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_NpcSkillBag>("db_npc_skill_bag.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_Level>("db_level.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_ActiveLevel>("db_level_active.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_SkillAttack>("db_skill_attack.json", Parsing))
            {
                return false;
            }
            if (false == LoadJson<ResJson_SkillProjectile>("db_skill_projectile.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_Deck>("db_deck.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_DeckStat>("db_deck_stat.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_Item>("db_item.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_ItemStat>("db_item_stat.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_ProjectileConstant>("db_projectile_constant.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_ProjectileTarget>("db_projectile_target.json", Parsing))
            {
                return false;
            }

            return true;
        }

        private bool Build()
        {
            if (true == m_jsonNpc.Any(pair => {

                if (false == m_arenaObject.ContainsKey(pair.Key))
                {
                    m_arenaObject.Add(pair.Key, pair.Value);
                    return false;
                }
                return true;//-error, overlapped key
            }))
            {
                return false;
            }
            if (true == m_jsonPlayer.Any(pair => {
                if (false == m_arenaObject.ContainsKey(pair.Key))
                {
                    m_arenaObject.Add(pair.Key, pair.Value);
                    return false;
                }
                return true;//-error, overlapped key
            }))
            {
                return false;
            }

            if (true == m_jsonProjectileConstant.Any(pair => {
                if (false == m_projectile.ContainsKey(pair.Key))
                {
                    m_projectile.Add(pair.Key, new ResData_Projectile(pair.Key, pair.Value.ProjectileType));
                    return false;
                }
                return true;//-error, overlapped key
            }))
            {
                return false;
            }
            if (true == m_jsonProjectileTarget.Any(pair => {
                if (false == m_projectile.ContainsKey(pair.Key))
                {
                    m_projectile.Add(pair.Key, new ResData_Projectile(pair.Key, pair.Value.ProjectileType));
                    return false;
                }
                return true;//-error, overlapped key
            }))
            {
                return false;
            }

            return true;
        }

        public Dictionary<int, ResJson_Deploy> JsonDeploy => m_jsonDeploy;
        
        public Dictionary<int, ResJson_Arena> JsonArena => m_jsonArena;

        public Dictionary<int, ResJson_Player> JsonPlayer => m_jsonPlayer;

        public Dictionary<int, ResJson_SkillAttack> JsonSkillAttack => m_jsonSkillAttack;

        public Dictionary<int, ResJson_SkillProjectile> JsonSkillProjectile => m_jsonSkillProjectile;

        public Dictionary<int, ResJson_Deck> JsonDeck => m_jsonDeck;

        public Dictionary<int, ResJson_Item> JsonItem => m_jsonItem;

        public Dictionary<int, ResData_Projectile> JsonProjectile => m_projectile;

        public Dictionary<int, ResJson_ProjectileConstant> JsonProjectileConstant => m_jsonProjectileConstant;

        public Dictionary<int, ResJson_ProjectileTarget> JsonProjectileTarget => m_jsonProjectileTarget;

        private R FinderEx<R, D>(D dic, int id) where D : Dictionary<int, R>
        {
            R r = default(R);
            if (true == dic.TryGetValue(id, out r))
            {
                return r;
            }

            return default(R);
        }

        private R Finder<R>(Dictionary<int, R> dic, int id)
        {
            R r = default(R);

            if (true == dic.TryGetValue(id, out r))
            {
                return r;
            }

            return default(R);
        }

        public ResJson_Arena FindArena(int jsonID)
        {
            return Finder<ResJson_Arena>(m_jsonArena, jsonID);
        }

        //public ResJson_ArenaObject FindArenaObject(int jsonID)
        //{
        //    return Finder<ResJson_ArenaObject>(m_jsonArenaObject, jsonID);
        //}

        public ResData_ArenaObject FindArenaObject(int arenaObjectID)
        {
            return Finder<ResData_ArenaObject>(m_arenaObject, arenaObjectID);
        }

        public ResJson_Collision FindCollision(int jsonID)
        {
            return Finder<ResJson_Collision>(m_jsonCollision, jsonID);
        }

        public ResJson_Deploy FindDeploy(int jsonID)
        {
            return Finder<ResJson_Deploy>(m_jsonDeploy, jsonID);
        }

        public ResJson_Map FindMap(int jsonID)
        {
            return Finder<ResJson_Map>(m_jsonMap, jsonID);
        }

        public ResJson_Npc FindNpc(int jsonID)
        {
            return Finder<ResJson_Npc>(m_jsonNpc, jsonID);
        }

        public ResJson_Player FindPlayer(int jsonID)
        {
            return Finder<ResJson_Player>(m_jsonPlayer, jsonID);
        }

        public ResJson_NpcAiState FindNpcAiState(int jsonID)
        {
            return Finder<ResJson_NpcAiState>(m_jsonNpcAiState, jsonID);
        }

        public ResJson_NpcAiBag FindNpcAiBag(int jsonID)
        {
            return Finder<ResJson_NpcAiBag>(m_jsonNpcAiBag, jsonID);
        }

        public ResJson_NpcSkillBag FindNpcSkillBag(int jsonID)
        {
            return Finder<ResJson_NpcSkillBag>(m_jsonNpcSkillBag, jsonID);
        }

        public ResJson_Level FindLevel(int level)
        {
            return Finder<ResJson_Level>(m_jsonLevel, level);
        }

        public ResJson_ActiveLevel FindActiveLevel(int level)
        {
            return Finder<ResJson_ActiveLevel>(m_jsonActiveLevel, level);
        }

        public ResJson_PlayerBaseStat FindPlayerStatLevel(PlayerClassType type)
        {
            return Finder<ResJson_PlayerBaseStat>(m_jsonPlayerBaseStat, (int)type);
        }

        public ResJson_PlayerStat FindPlayerStat(int id)
        {
            return Finder<ResJson_PlayerStat>(m_jsonPlayerStat, id);
        }

        public ResJson_Deck FindDeck(int jsonID)
        {
            return Finder<ResJson_Deck>(m_jsonDeck, jsonID);
        }

        public ResJson_DeckStat FindDeckStat(int jsonID)
        {
            return Finder<ResJson_DeckStat>(m_jsonDeckStat, jsonID);
        }

        public ResJson_ItemStat FindItemStat(int jsonID)
        {
            return Finder<ResJson_ItemStat>(m_jsonItemStat, jsonID);
        }

        Dictionary<int, ResJson_Arena> m_jsonArena = new Dictionary<int, ResJson_Arena>();
        Dictionary<int, ResJson_Collision> m_jsonCollision = new Dictionary<int, ResJson_Collision>();
        Dictionary<int, ResJson_Deploy> m_jsonDeploy = new Dictionary<int, ResJson_Deploy>();
        Dictionary<int, ResJson_Map> m_jsonMap = new Dictionary<int, ResJson_Map>();

        Dictionary<int, ResJson_Player> m_jsonPlayer = new Dictionary<int, ResJson_Player>();
        Dictionary<int, ResJson_PlayerBaseStat> m_jsonPlayerBaseStat = new Dictionary<int, ResJson_PlayerBaseStat>();
        Dictionary<int, ResJson_PlayerStat> m_jsonPlayerStat = new Dictionary<int, ResJson_PlayerStat>();

        Dictionary<int, ResJson_Npc> m_jsonNpc = new Dictionary<int, ResJson_Npc>();
        Dictionary<int, ResJson_NpcAiState> m_jsonNpcAiState = new Dictionary<int, ResJson_NpcAiState>();
        Dictionary<int, ResJson_NpcAiBag> m_jsonNpcAiBag = new Dictionary<int, ResJson_NpcAiBag>();
        Dictionary<int, ResJson_NpcSkillBag> m_jsonNpcSkillBag = new Dictionary<int, ResJson_NpcSkillBag>();

        Dictionary<int, ResJson_Level> m_jsonLevel = new Dictionary<int, ResJson_Level>();
        Dictionary<int, ResJson_ActiveLevel> m_jsonActiveLevel = new Dictionary<int, ResJson_ActiveLevel>();

        Dictionary<int, ResJson_SkillAttack> m_jsonSkillAttack = new Dictionary<int, ResJson_SkillAttack>();
        Dictionary<int, ResJson_SkillProjectile> m_jsonSkillProjectile = new Dictionary<int, ResJson_SkillProjectile>();

        Dictionary<int, ResJson_Deck> m_jsonDeck = new Dictionary<int, ResJson_Deck>();
        Dictionary<int, ResJson_DeckStat> m_jsonDeckStat = new Dictionary<int, ResJson_DeckStat>();

        Dictionary<int, ResJson_Item> m_jsonItem = new Dictionary<int, ResJson_Item>();
        Dictionary<int, ResJson_ItemStat> m_jsonItemStat = new Dictionary<int, ResJson_ItemStat>();

        Dictionary<int, ResJson_ProjectileConstant> m_jsonProjectileConstant = new Dictionary<int, ResJson_ProjectileConstant>();
        Dictionary<int, ResJson_ProjectileTarget> m_jsonProjectileTarget = new Dictionary<int, ResJson_ProjectileTarget>();

        //-build container
        Dictionary<int, ResData_ArenaObject> m_arenaObject = new Dictionary<int, ResData_ArenaObject>();
        Dictionary<int, ResData_Projectile> m_projectile = new Dictionary<int, ResData_Projectile>();
    }
}
