using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Managers;
using System;
using System.Collections.Generic;
using sprint0Test.Link1;

namespace sprint0Test.Enemy
{
    public class Moblin : AbstractEnemy
    {
        private Random random;
        private Vector2 movementDirection;
        private float movementSpeed = 1.5f;
        private float changeDirectionCooldown = 2.0f;
        private float currentCooldown = 0f;

        public Moblin(Vector2 startPosition, Dictionary<string, Texture2D> Moblin_textures)
            : base(startPosition, new Texture2D[]
            {
                Moblin_textures["Goblin_1"],
                Moblin_textures["Goblin_2"],
                // Moblin_textures["Goblin_3"],
                // Moblin_textures["Goblin_4"]
            })
        {
            detectionRadius = 100f; // Short detection range
            attackRange = 25f; // Needs to be close to attack
            health = 5; // Medium health
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
            // Moblin does a melee attack
            if (IsInAttackRange())
            {
                Link.Instance.TakeDamage();
            }
        }
    }
}
