using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using sprint0Test.Managers;
using sprint0Test.Link1;

namespace sprint0Test.Enemy
{
    public class Aquamentus : AbstractEnemy
    {
        private float attackCooldown = 3.0f; // Attack every 3 seconds
        private float currentCooldown = 0f; // Timer to track attack cooldown

        public Aquamentus(Vector2 startPosition)
            : base(startPosition, new Texture2D[]
            {
                TextureManager.Instance.GetTexture("Dragon_Idle1"),
                TextureManager.Instance.GetTexture("Dragon_Idle2")
            })
        {
            attackRange = 200f; // Set custom attack range for Aquamentus
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Reduce cooldown timer
            if (currentCooldown > 0)
            {
                currentCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Attack if Link is in range and cooldown is over
            if (IsInAttackRange() && currentCooldown <= 0)
            {
                PerformAttack();
                currentCooldown = attackCooldown; // Reset cooldown
            }
        }

        public override void PerformAttack()
        {
            Console.WriteLine("Aquamentus is attacking!");

            // Get direction to Link
            Vector2 directionToLink = GetDirectionToPlayer();

            // Adjust to Aquamentus' fireball pattern (slightly varied directions)
            Vector2[] attackDirections = new Vector2[]
            {
                directionToLink,                      // Center shot
                directionToLink + new Vector2(0.1f, 0), // Slightly right
                directionToLink + new Vector2(-0.1f, 0) // Slightly left
            };

            // Spawn fireballs
            foreach (var direction in attackDirections)
            {
                ProjectileManager.Instance.SpawnProjectile(position, direction, "Fireball");
            }
        }
    }
}
