using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Projectiles
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class AbstractProjectile : IProjectile
    {
        protected Vector2 position;
        protected Vector2 direction;
        protected Texture2D texture;
        protected bool isActive;
        protected float speed;
        protected float lifetime; // Time before projectile disappears

        public AbstractProjectile(Vector2 startPosition, Vector2 direction, Texture2D texture, float speed, float lifetime = 5.0f)
        {
            this.position = startPosition;
            this.direction = direction;
            this.texture = texture;
            this.speed = speed;
            this.lifetime = lifetime;
            this.isActive = true; // Active by default
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!isActive) return; // Skip update if inactive

            // Move the projectile
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Reduce lifetime
            lifetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lifetime <= 0)
            {
                Deactivate();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (isActive && texture != null)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

        public void Deactivate()
        {
            isActive = false;
        }

        public bool IsActive()
        {
            return isActive;
        }
    }

}
