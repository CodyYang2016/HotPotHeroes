using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
using System;

namespace sprint0Test.Enemy
{

    
        public class Aquamentus : AbstractEnemy
        {
            public Aquamentus(Vector2 startPosition)
                : base(startPosition, new Texture2D[]
                {
                    TextureManager.Instance.GetTexture("Dragon_Idle1"),
                    TextureManager.Instance.GetTexture("Dragon_Idle2"),
                  //TextureManager.Instance.GetTexture("Dragon_Idle3"),
                  //TextureManager.Instance.GetTexture("Dragon_Idle4")


                })
            {
                detectionRadius = 250f; // Large detection range
                attackRange = 200f; // Attacks from far away
                health = 10; // High health
                
            }

        public override void PerformAttack()
        {
            Console.WriteLine("Aquamentus attacks!");

            // Main fireball direction (left)
            Vector2 baseDirection = new Vector2(-1, 0);

            // Spawn three fireballs with slightly varied directions for a spread effect
            ProjectileManager.Instance.SpawnProjectile(position, baseDirection);
            ProjectileManager.Instance.SpawnProjectile(position, baseDirection + new Vector2(0.1f, 0));
            ProjectileManager.Instance.SpawnProjectile(position, baseDirection + new Vector2(-0.1f, 0));
        }
    }
    

}
