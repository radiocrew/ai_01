using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class ArenaMap
    {
        public ArenaMap()
        {
        }

        public bool Initialize(int mapID, Arena arena)
        {
            var resMap = ResourceManager.Instance.FindMap(mapID);
            if (null == resMap)
            {
                return false;
            }

            if (false == resMap.IsValid())
            {
                return false;
            }

            m_mapID = resMap.MapID;
            m_width = resMap.Size.Width;
            m_height = resMap.Size.Height;

            m_gridMap = new GridMap(resMap.Grid.Width, resMap.Grid.Height);

            m_DeployManager = new DeployManager(arena);
            if (false == m_DeployManager.Initialize(resMap.DeployList))
            {
                return false;
            }

            return true;
        }

        public UnityEngine.Vector2 RandomPoint(UnityEngine.Rect intersect)
        {
            //-intersect, you can do some..
            //

            System.Random rand = new System.Random();
            return new Vector2((float)rand.Next(0, Width), (float)rand.Next(0, Height));
        }

        public void OnUpdate(float dt)
        {
        }

        public bool IsValid(float x, float y)
        {
            if ((0 > x) || (0 > y))
            {
                return false;
            }

            if ((x >= m_width) || (y >= m_height))
            {
                return false;
            }

            return true;
        }

        public GridMap GridMap { get => m_gridMap; }

        public int ID => m_mapID;

        public int Width => m_width;

        public int Height => m_height;

        int m_mapID = 0;
        int m_width = 0;
        int m_height = 0;

        GridMap m_gridMap;
        DeployManager m_DeployManager;
    }
}
