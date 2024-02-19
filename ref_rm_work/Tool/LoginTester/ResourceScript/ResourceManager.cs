using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using RM.Common;

namespace rm_login.Tool.Script
{
    public partial class ResourceManager
    {
        static private readonly Lazy<ResourceManager> s_lazy = new Lazy<ResourceManager>(() => new ResourceManager());
        static public ResourceManager Instance { get { return s_lazy.Value; } }

        public bool Initialize()
        {
            m_ContentRootDirectory = "..\\..\\..\\..\\..\\Deploy\\Server\\ServerData\\";

            bool ret = LoadDBFromFile();

            string retStr = (true == ret) ? ("completed") : ("failed");
            Console.WriteLine("[{0}] Resource manager Initialize {1}", System.DateTime.Now.ToString("hh:mm:ss.fff"), retStr);
            if (false == ret)
            {
                Debug.Assert(false);
            }
            return ret;
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

            if (false == LoadJson<ResJson_Map>("db_map.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_Player>("db_player.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_Npc>("db_npc.json", Parsing))
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

            if (false == LoadJson<ResJson_TestCommand>("db_test_command.json", Parsing))
            {
                return false;
            }

            if (false == LoadJson<ResJson_ProjectileConstant>("db_projectile_constant.json", Parsing))
            {
                return false;
            }


            return true;
        }

        public Dictionary<int, ResJson_Arena> JsonArena => m_jsonArena;

        public Dictionary<int, ResJson_Map> JsonMap => m_jsonMap;

        public Dictionary<int, ResJson_Player> JsonPlayer => m_jsonPlayer;

        public Dictionary<int, ResJson_Npc> JsonNpc => m_jsonNpc;

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

        public ResJson_Map FindMap(int jsonID)
        {
            return Finder<ResJson_Map>(m_jsonMap, jsonID);
        }

        public ResJson_Player FindPlayer(int arenaObjectID)
        {
            return Finder<ResJson_Player>(m_jsonPlayer, arenaObjectID);
        }

        public ResJson_Npc FindNpc(int arenaObjectID)
        {
            return Finder<ResJson_Npc>(m_jsonNpc, arenaObjectID);
        }

        public ResJson_Level FindLevel(int level)
        {
            return Finder<ResJson_Level>(m_jsonLevel, level);
        }

        public ResJson_ActiveLevel FindActiveLevel(int level)
        {
            return Finder<ResJson_ActiveLevel>(m_jsonActiveLevel, level);
        }

        public TestCommandType FindTestCommand(string text)
        {
            TestCommandType ret = TestCommandType.None;
            if (true == m_jsonTestCommand.TryGetValue(text, out ret))
            {
                return ret;
            }

            return TestCommandType.None;
        }

        public ResJson_ProjectileConstant FindProjectileConstant(int projectileID)
        {
            return Finder<ResJson_ProjectileConstant>(m_jsonProjectileConstant, projectileID);
        }

        Dictionary<int, ResJson_Arena> m_jsonArena = new Dictionary<int, ResJson_Arena>();
        Dictionary<int, ResJson_Map> m_jsonMap = new Dictionary<int, ResJson_Map>();
        Dictionary<int, ResJson_Player> m_jsonPlayer = new Dictionary<int, ResJson_Player>();
        Dictionary<int, ResJson_Npc> m_jsonNpc = new Dictionary<int, ResJson_Npc>();
        Dictionary<int, ResJson_Level> m_jsonLevel = new Dictionary<int, ResJson_Level>();
        Dictionary<int, ResJson_ActiveLevel> m_jsonActiveLevel = new Dictionary<int, ResJson_ActiveLevel>();
        Dictionary<string, TestCommandType> m_jsonTestCommand = new Dictionary<string, TestCommandType>();
        Dictionary<int, ResJson_ProjectileConstant> m_jsonProjectileConstant = new Dictionary<int, ResJson_ProjectileConstant>();

    }
}
