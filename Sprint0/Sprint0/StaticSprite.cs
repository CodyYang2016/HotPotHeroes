using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902
{
    public class StaticSprite : ISprite
    {
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 point;
        public float Scale { get; set; }

        public StaticSprite(Texture2D texture, Rectangle rectangle, float scale = 1.0f)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");

            this.texture = texture;
            sourceRectangle = rectangle;
            Scale = scale;

            // Precompute the center point
            point = new Vector2(
                (800 - sourceRectangle.Width * Scale) / 2,
                (480 - sourceRectangle.Height * Scale) / 2
            );
        }

        // No updates needed since this is a static sprite
        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
                throw new ArgumentNullException(nameof(spriteBatch), "SpriteBatch cannot be null.");

            Rectangle destination = new Rectangle(
                (int)point.X,
                (int)point.Y,
                (int)(sourceRectangle.Width * Scale),
                (int)(sourceRectangle.Height * Scale)
            );

            spriteBatch.Draw(texture, destination, sourceRectangle, Color.White);
        }
    }
}
