using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RM.Common;

namespace ArenaServer.Resource
{
    public partial class ResourceManager
    {
        string m_ContentRootDirectory;

        private bool LoadJson<T>(string fileName, Func<T, bool> funcParsing)
        {
            try
            {
                using (StreamReader stream = new StreamReader(m_ContentRootDirectory + fileName))
                {
                    //var settings = new JsonSerializerSettings
                    //{
                    //    NullValueHandling = NullValueHandling.Ignore,
                    //    MissingMemberHandling = MissingMemberHandling.Ignore
                    //};

                    string strJson = stream.ReadToEnd();
                    List<T> itemList = JsonConvert.DeserializeObject<List<T>>(strJson);

                    foreach (var item in itemList)
                    {
                        //if (typeof(T).Equals(typeof(ResPlist)))                        
                        if (false == funcParsing(item))
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
                return false;
            }

            return true;
        }

        private bool Adder<CONT, KEY, PARAM>(CONT cont, KEY key, PARAM resJson) where CONT : Dictionary<KEY, PARAM>
        {
            if (false == cont.ContainsKey(key))
            {
                cont.Add(key, resJson);
                return true;
            }
            return false;
        }

        private bool Parsing(ResJson_Arena resJson)
        {
            return Adder(m_jsonArena, resJson.ArenaID, resJson);
        }

        //private bool Parsing(ResJson_ArenaObject resJson)
        //{
        //    return Adder(m_jsonArenaObject, resJson.ArenaObjectID, resJson);
        //}

        private bool Parsing(ResJson_Collision resJson)
        {
            return Adder(m_jsonCollision, resJson.ID, resJson);
        }

        private bool Parsing(ResJson_Deploy resJson)
        {
            return Adder(m_jsonDeploy, resJson.DeployID, resJson);
        }

        private bool Parsing(ResJson_Map resJson)
        {
            return Adder(m_jsonMap, resJson.MapID, resJson);
        }

        private bool Parsing(ResJson_Npc resJson)
        {
            return Adder(m_jsonNpc, resJson.ArenaObjectID, resJson);
        }

        private bool Parsing(ResJson_Player resJson)
        {
            return Adder(m_jsonPlayer, resJson.ArenaObjectID, resJson);
        }

        private bool Parsing(ResJson_NpcAiState resJson)
        {
            return Adder(m_jsonNpcAiState, (int)resJson.StateType, resJson);
        }

        private bool Parsing(ResJson_NpcAiBag resJson)
        {
            return Adder(m_jsonNpcAiBag, (int)resJson.BagID, resJson);
        }

        private bool Parsing(ResJson_NpcSkillBag resJson)
        {
            return Adder(m_jsonNpcSkillBag, (int)resJson.ID, resJson);
        }

        private bool Parsing(ResJson_Level resJson)
        {
            return Adder(m_jsonLevel, (int)resJson.Level, resJson);
        }

        private bool Parsing(ResJson_ActiveLevel resJson)
        {
            return Adder(m_jsonActiveLevel, (int)resJson.Level, resJson);
        }

        private bool Parsing(ResJson_PlayerBaseStat resJson)
        {
            return Adder(m_jsonPlayerBaseStat, (int)resJson.ClassType, resJson);
        }

        private bool Parsing(ResJson_PlayerStat resJson)
        {
            if (false == m_resourceValid.IsStat(resJson.StatType))
            {
                return false;
            }

            return Adder(m_jsonPlayerStat, resJson.ID, resJson);
        }

        private bool Parsing(ResJson_SkillAttack resJson)
        {
            return Adder(m_jsonSkillAttack, resJson.SkillID, resJson);
        }

        private bool Parsing(ResJson_SkillProjectile resJson)
        {
            return Adder(m_jsonSkillProjectile, resJson.SkillID, resJson);
        }

        private bool Parsing(ResJson_Deck resJson)
        {
            return Adder(m_jsonDeck, resJson.DeckID, resJson);
        }

        private bool Parsing(ResJson_DeckStat resJson)
        {
            return Adder(m_jsonDeckStat, resJson.ID, resJson);
        }

        private bool Parsing(ResJson_Item resJson)
        {
            return Adder(m_jsonItem, resJson.ID, resJson);
        }

        private bool Parsing(ResJson_ItemStat resJson)
        {
            return Adder(m_jsonItemStat, resJson.ID, resJson);
        }

        private bool Parsing(ResJson_ProjectileConstant resJson)
        {
            return Adder(m_jsonProjectileConstant, resJson.ProjectileID, resJson);
        }

        private bool Parsing(ResJson_ProjectileTarget resJson)
        {
            return Adder(m_jsonProjectileTarget, resJson.ProjectileID, resJson);
        }



    //-
        ResourceValid m_resourceValid = new ResourceValid();
    }    
}
