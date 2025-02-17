using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotHeroes.sprint0Game.sprint0Test.Enemy
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using global::HotpotHeroes.sprint0Game.sprint0Test.Managers;

    namespace HotpotHeroes.sprint0Game.sprint0Test.Enemy
    {
        public class Aquamentus : AbstractEnemy
        {
            public Aquamentus(Vector2 startPosition)
                : base(startPosition, TextureManager.Instance.GetTexture("Dragon"))
            {
                detectionRadius = 250f; // Large detection range
                attackRange = 200f; // Attacks from far away
                health = 10; // High health
            }

            public override void PerformAttack()
            {
                // Aquamentus shoots fireballs in a spread pattern
                Vector2 dir = new Vector2(-1,0);
                ProjectileManager.Instance.SpawnProjectile(position, dir, TextureManager.Instance.GetTexture("Dragon_Projectile"));
                ProjectileManager.Instance.SpawnProjectile(position, dir + new Vector2(0.1f, 0), TextureManager.Instance.GetTexture("Dragon_Projectile"));
                ProjectileManager.Instance.SpawnProjectile(position, dir + new Vector2(-0.1f, 0), TextureManager.Instance.GetTexture("Dragon_Projectile"));
            }
        }
    }

}
