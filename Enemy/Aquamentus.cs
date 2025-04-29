using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using sprint0Test.Managers;
using sprint0Test.Link1;

namespace sprint0Test.Enemy
{
    public class Aquamentus : AbstractEnemy
    {
        private float attackCooldown = 1.0f; // Attack every 3 seconds
        private float currentCooldown = 0f; // Timer to track attack cooldown
        private float speed = 0.5f; // Sprint5 Movement speed
        private float upperLimit;
        private float lowerLimit;
        private int direction = 1; // 1 for down, -1 for up
        private int health = 30;

        public Aquamentus(Vector2 startPosition, float movementRange)
            : base(startPosition, new Texture2D[]
            {
                TextureManager.Instance.GetTexture("Dragon_Idle1"),
                TextureManager.Instance.GetTexture("Dragon_Idle2")
            })
        {
            attackRange = 100f; // Set custom attack range for Aquamentus
            upperLimit = startPosition.Y - movementRange;
            lowerLimit = startPosition.Y + movementRange;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // ✅ Always decrement cooldown, even if it's below 0
            currentCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentCooldown <= 0f)
            {
                if (IsInAttackRange())
                {
                    PerformAttack();
                    currentCooldown = attackCooldown;
                }
            }

            // Move up and down
            position.Y += speed * direction;
            if (position.Y >= lowerLimit || position.Y <= upperLimit)
            {
                direction *= -1; // Reverse direction
            }
        }







        public override void PerformAttack()
        {

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