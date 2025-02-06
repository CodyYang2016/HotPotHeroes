using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CSE3902
{
    public class MovingSprite : ISprite
    {
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 position;
        private Vector2 direction;
        public float Scale { get; set; }

        public MovingSprite(Texture2D t, Rectangle sourceR, float s = 1.0f)
        {
            texture = t ?? throw new ArgumentNullException(nameof(t));
            sourceRectangle = sourceR;
            Scale = s;

            // Centering the sprite
            position = new Vector2(
                (800 - sourceRectangle.Width * Scale) / 2,
                (480 - sourceRectangle.Height * Scale) / 2
            );

            // Moving vertically by default
            direction = new Vector2(0f, 2f);
        }

        public void Update(GameTime gameTime)
        {
            // Adjust movement speed with game time
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * deltaTime * 100; // Multiply by speed factor (100)

            // If sprite moves out of vertical bounds, reverse direction
            if (position.Y < 0 || position.Y > 480 - sourceRectangle.Height * Scale)
            {
                direction.Y *= -1;
                position.Y = MathHelper.Clamp(position.Y, 0f, 480f - (sourceRectangle.Height * Scale)); // ✅ FIXED: Using MathHelper.Clamp
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            Rectangle destinationRectangle = new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)(sourceRectangle.Width * Scale),
                (int)(sourceRectangle.Height * Scale)
            );

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
