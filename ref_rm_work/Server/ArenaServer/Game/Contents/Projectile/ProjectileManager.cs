using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level9.Expedition.Mobile.Util;

using ArenaServer.Resource;

namespace ArenaServer.Game
{
    public class ProjectileManager : Singleton<ProjectileManager>
    {
        public bool Initialize()
        { 
            return true;
        }

        public ProjectileBase CreateProjectile(int projectileID, Arena arena, ArenaObject owner, ProjectileParam projectileParam)
        {
            ResData_Projectile jsonProjectile;
            if (false == ResourceManager.Instance.JsonProjectile.TryGetValue(projectileID, out jsonProjectile))
            {
                return null;
            }

            switch (jsonProjectile.ProjectileType)
            {
                case RM.Common.ProjectileType.Constant:

                    ResJson_ProjectileConstant jsonProjectileConstant;
                    if (true == ResourceManager.Instance.JsonProjectileConstant.TryGetValue(projectileID, out jsonProjectileConstant))
                    {
                        return new ProjectileConstant(arena, owner, projectileParam.JsonSkill, jsonProjectileConstant, projectileParam.Direction);
                    }
                    break;
                case RM.Common.ProjectileType.Target:

                    ResJson_ProjectileTarget jsonProjectileTarget;
                    if (true == ResourceManager.Instance.JsonProjectileTarget.TryGetValue(projectileID, out jsonProjectileTarget))
                    {
                        return new ProjectileTarget(arena, owner, projectileParam.JsonSkill, jsonProjectileTarget);
                    }
                    break;
                default:
                    return null;

            }

            return null;
        }

        private ProjectileManager()
        {
        }

        //-https://stackoverflow.com/questions/24586902/is-it-ok-to-concurrently-read-a-dictionary
        ConcurrentDictionary<int, ProjectileBase> m_projectiles = null;
    }
}
