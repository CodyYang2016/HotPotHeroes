using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;

namespace sprint0Test.Enemy
{
    public abstract class AbstractEnemy : IEnemy
    {
        protected Vector2 position;
        protected int health;
        protected float detectionRadius = 100f;
        protected float attackRange = 30f;
        protected float scale = 3f;
        protected IEnemyState currentState;

        // Animation properties
        protected Texture2D[] animationFrames;
        protected int currentFrame = 0;
        protected double frameTime = 0.1;
        protected double frameTimer = 0.0;

        public AbstractEnemy(Vector2 startPosition, Texture2D[] textures)
        {
            position = startPosition;

            if (textures == null || textures.Length == 0)
            {
                Console.WriteLine("Error: animationFrames array is NULL or EMPTY in " + this.GetType().Name);
                animationFrames = new Texture2D[1]; // Assign a dummy array to prevent null reference issues
            }
            else
            {
                animationFrames = textures;
            }

            health = 3;
            currentState = new AttackState(this);
        }

        public virtual void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
            UpdateAnimation(gameTime);
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= frameTime)
            {
                frameTimer = 0;
                currentFrame = (currentFrame + 1) % animationFrames.Length; // Loop through frames
            }
        }

        public void SetScale(float newScale)
        {
            scale = newScale;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (animationFrames.Length > 0)
            {
                Vector2 origin = new Vector2(animationFrames[currentFrame].Width / 2, animationFrames[currentFrame].Height / 2);
                spriteBatch.Draw(animationFrames[currentFrame], position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                ChangeState(new DeadState(this));
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public virtual void PerformAttack()
        {
            // Overridden in subclasses
        }

        public void Destroy()
        {
            // EnemyManager.Instance.RemoveEnemy(this);
        }
    }
}
