using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HotpotHeroes.sprint0Game.sprint0Test.Projectiles
{
    public class Projectile
    {
        public Vector2 Position { get; private set; }
        private Vector2 velocity;
        private Texture2D texture;
        private float speed = 5f;
        private bool isActive;

        public Projectile(Vector2 startPosition, Vector2 direction, Texture2D texture)
        {
            this.Position = startPosition;
            this.velocity = Vector2.Normalize(direction) * speed;
            this.texture = texture;
            this.isActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!isActive) return;

            // Move the projectile
            Position += velocity;

            // Remove if off-screen (adjust bounds as needed)
            if (Position.X < 0 || Position.X > 800 || Position.Y < 0 || Position.Y > 600)
            {
                isActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
                spriteBatch.Draw(texture, Position, Color.White);
        }

        public bool IsActive()
        {
            return isActive;
        }
    }
}
