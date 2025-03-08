using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Managers;

namespace sprint0Test.Enemy
{
    public class Octorok : AbstractEnemy
    {
        private Random random;
        private Vector2 movementDirection;
        private float movementSpeed = 1.5f;
        private float changeDirectionCooldown = 2.0f;
        private float currentCooldown = 0f;

        public Octorok(Vector2 startPosition, Dictionary<string, Texture2D> Octorok_textures)
            : base(startPosition, new Texture2D[]
            {
                Octorok_textures["Octopus_Idle1"],
                Octorok_textures["Octopus_Idle2"]
            })
        {
            detectionRadius = 150f; // Detects player from medium distance
            attackRange = 100f; // Attacks from range
            random = new Random();
            SetRandomDirection();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Move in the current direction
            position += movementDirection * movementSpeed;

            // Decrease cooldown
            currentCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentCooldown <= 0)
            {
                SetRandomDirection();
                currentCooldown = changeDirectionCooldown;
            }
        }

        private void SetRandomDirection()
        {
            float angle = (float)(random.NextDouble() * Math.PI * 2);
            movementDirection = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
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
