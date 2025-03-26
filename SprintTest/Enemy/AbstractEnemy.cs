using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using sprint0Test.Managers;
using sprint0Test.Link1;

namespace sprint0Test.Enemy
{
    public abstract class AbstractEnemy : IEnemy
    {
        public Vector2 position;
        protected int health;
        public int Health => health;
        protected float detectionRadius = 0.2f;
        protected float attackRange = 0.2f;
        protected float scale = 3f;
        protected IEnemyState currentState;

        // Animation properties
        protected Texture2D[] animationFrames;
        protected int currentFrame = 0;
        protected double frameTime = 0.1;
        protected double frameTimer = 0.0;
        public bool IsDead => currentState is DeadState;


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

        public virtual void TakeDamage(int damage)
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

        public bool IsInAttackRange()
        {
            return Vector2.Distance(position, Link.Instance.Position) < attackRange;
        }

        public Vector2 GetDirectionToPlayer()
        {
            // Normalize direction vector to get unit vector pointing towards the player
            Vector2 direction = Link.Instance.Position - position;

            if (direction != Vector2.Zero)
            {
                direction.Normalize(); // Ensures the vector has a length of 1
            }

            return direction;
        }

        public virtual Vector2 GetDimensions()
        {
            if (animationFrames.Length > 0 && animationFrames[currentFrame] != null)
            {
                return new Vector2(animationFrames[currentFrame].Width * scale, animationFrames[currentFrame].Height * scale);
            }
            return Vector2.Zero; // Return (0,0) if no valid texture
        }

        public bool DetectPlayer()
        {
            return Vector2.Distance(position, Link.Instance.Position) <= detectionRadius;
        }




    }
}
