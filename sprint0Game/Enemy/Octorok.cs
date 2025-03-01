using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Managers;

namespace sprint0Test.Enemy
{
    public class Octorok : AbstractEnemy
    {
        public Octorok(Vector2 startPosition, Dictionary<string, Texture2D> Octorok_textures)
            : base(startPosition, new Texture2D[]
            {
            Octorok_textures["Octopus_Idle1"],
            Octorok_textures["Octopus_Idle2"]
            })
        {

            {
                detectionRadius = 150f; // Detects player from medium distance
                attackRange = 100f; // Attacks from range
            }
        }

        public override void PerformAttack()
        {
            // Octorok shoots a projectile
            ProjectileManager.Instance.SpawnProjectile(
                position, 
                GetDirectionToPlayer(),
                "Octupus_Projectile" // 🔹 Uses correct projectile texture
            );
        }
    }
 }
