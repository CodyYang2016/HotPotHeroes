using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
using System;

namespace sprint0Test.Enemy
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
            Console.WriteLine("Aquamentus attacks!");
                Vector2 dir = new Vector2(-1,0);
                ProjectileManager.Instance.SpawnProjectile(position, dir, TextureManager.Instance.GetTexture("Dragon_Projectile"));
                ProjectileManager.Instance.SpawnProjectile(position, dir + new Vector2(0.1f, 0), TextureManager.Instance.GetTexture("Dragon_Projectile"));
                ProjectileManager.Instance.SpawnProjectile(position, dir + new Vector2(-0.1f, 0), TextureManager.Instance.GetTexture("Dragon_Projectile"));
            }
        }
    

}
