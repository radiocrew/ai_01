using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace rm_login.Tool.Script
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

        private bool Parsing(ResJson_Map resJson)
        {
            return Adder(m_jsonMap, resJson.MapID, resJson);
        }

        private bool Parsing(ResJson_Player resJson)
        {
            return Adder(m_jsonPlayer, resJson.ArenaObjectID, resJson);
        }

        private bool Parsing(ResJson_Npc resJson)
        {
            return Adder(m_jsonNpc, resJson.ArenaObjectID, resJson);
        }

        private bool Parsing(ResJson_Level resJson)
        {
            return Adder(m_jsonLevel, (int)resJson.Level, resJson);
        }

        private bool Parsing(ResJson_ActiveLevel resJson)
        {
            return Adder(m_jsonActiveLevel, (int)resJson.Level, resJson);
        }

        private bool Parsing(ResJson_TestCommand resJson)
        {
            if (false == m_jsonTestCommand.ContainsKey(resJson.Text))
            {
                m_jsonTestCommand.Add(resJson.Text, resJson.TestCommandType);
                return true; 
            }

            return false;
        }

        private bool Parsing(ResJson_ProjectileConstant resJson)
        {
            if (false == m_jsonProjectileConstant.ContainsKey(resJson.ProjectileID))
            {
                m_jsonProjectileConstant.Add(resJson.ProjectileID, resJson);
                return true;
            }

            return false;
        }

    }
}
