using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM.Net;

namespace ArenaServer.Game
{
    public partial class Arena
    {
        public ProjectileBase AddProjectile(int projectileID, ArenaObject owner, ProjectileParam param)
        {
            var projectile = ProjectileManager.Instance.CreateProjectile(projectileID, this, owner, param);
            projectile.UID = Guid.NewGuid();

            if ((null != projectile) 
            && (false != m_projectiles.TryAdd(projectile.UID, projectile))
            && (false != projectile.Initialize())
            )
            {
                return projectile;
            }

            return null;
        }

        public void RemoveProjectile(Guid uid)
        {
            ProjectileBase remove = null;
            m_projectiles.TryRemove(uid, out remove);

            if (null != remove)
            {
                remove.Destroy();
            }
        }

        ConcurrentDictionary<Guid, ProjectileBase> m_projectiles = null;
    }
}
