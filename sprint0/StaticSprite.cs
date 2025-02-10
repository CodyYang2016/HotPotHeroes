using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    public class StaticSprite : ISprite
    {
        private Texture2D _texture;
        private Rectangle _sourceRectangle;
        private Vector2 _position;
        private float _scale;

        public StaticSprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, float scale = 1.0f)
        {
            _texture = texture;
            _sourceRectangle = sourceRectangle;
            _position = position - new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2) * scale;
            _scale = scale;
        }

        public void Update(GameTime gameTime)
        {
            // No update logic for static sprite
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _sourceRectangle, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}

