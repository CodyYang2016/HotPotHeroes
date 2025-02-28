using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Managers;
using sprint0Test.Projectiles;
using System;
using System.Collections.Generic;

namespace sprint0Test.Enemy
{

    
        public class Aquamentus : AbstractEnemy
        {
        public Aquamentus(Vector2 startPosition, Dictionary<string, Texture2D> Aquamentus_textures)
            : base(startPosition, new Texture2D[]
            {
            Aquamentus_textures["Dragon_Idle1"],
            Aquamentus_textures["Dragon_Idle2"]
                // textures["Dragon_Idle3"],
                // textures["Dragon_Idle4"]
            })
        {
                detectionRadius = 250f; // Large detection range
                attackRange = 200f; // Attacks from far away
                health = 10; // High health
                
            }

        public override void PerformAttack()
        {

            // Main fireball direction (left)
            Vector2 baseDirection = new Vector2(-1, 0);

            // Spawn three fireballs with slightly varied directions for a spread effect
            ProjectileManager.Instance.SpawnProjectile(position, baseDirection, TextureManager.Instance.GetTexture("Fireball"));
            ProjectileManager.Instance.SpawnProjectile(position, baseDirection + new Vector2(0.1f, 0), TextureManager.Instance.GetTexture("Fireball"));
            ProjectileManager.Instance.SpawnProjectile(position, baseDirection + new Vector2(-0.1f, 0), TextureManager.Instance.GetTexture("Fireball"));
        }
    }
    

}
