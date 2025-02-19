using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;

namespace sprint0Test.Enemy
{
    public class Octorok : AbstractEnemy
    {
        public Octorok(Vector2 startPosition)
            : base(startPosition, new Texture2D[]
        {
            TextureManager.Instance.GetTexture("Octopus_Idle1"),
            TextureManager.Instance.GetTexture("Octopus_Idle2")
        })
        {
            detectionRadius = 150f; // Detects player from medium distance
            attackRange = 100f; // Attacks from range
        }

        public override void PerformAttack()
        {
            // Octorok shoots a projectile
            ProjectileManager.Instance.SpawnProjectile(
                position,position,
                // GetDirectionToPlayer(),
                TextureManager.Instance.GetTexture("Octopus_Projectile") // 🔹 Uses correct projectile texture
            );
        }
    }
}
