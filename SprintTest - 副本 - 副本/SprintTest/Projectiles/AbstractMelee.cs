using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Projectiles;

namespace sprint0Test.Projectiles
{
    public abstract class AbstractMelee : IProjectile
    {
        protected Vector2 position;  // Backing field for Position
        protected Vector2 direction;
        protected Texture2D texture;
        protected bool isActive;
        protected bool isFriendly;
        protected float lifetime;

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public AbstractMelee(Vector2 startPosition, Texture2D texture, float lifetime = 1.0f, bool isFriendly = true)
        {
            this.position = startPosition;
            this.texture = texture;
            this.lifetime = lifetime;
            this.isActive = true;
            this.isFriendly = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!isActive) return;

            //position += direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
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

        public bool IsFriendly()
        {
            return isFriendly;
        }

        public void Reset(Vector2 newPosition, Vector2 newDirection)
        {
            Position = newPosition;
            direction = newDirection;
            lifetime = 1.0f;  // Reset lifetime
            isActive = true;  // Reactivate projectile
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public virtual Vector2 GetDimensions()
        {
            return new Vector2(8, 8);
        }
    }
}
