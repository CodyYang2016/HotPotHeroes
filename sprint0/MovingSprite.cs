using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    public class MovingSprite : ISprite
    {
        private Texture2D _texture;
        private Rectangle _sourceRectangle;
        private Vector2 _position;
        private float _scale;
        private float _speed = 200f; // Pixels/second
        private int _direction = -1; // Initial direction is up (-1 means up, 1 means down)

        private int FrameWidth => _sourceRectangle.Width;
        private int FrameHeight => _sourceRectangle.Height;

        public MovingSprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, float scale = 1.0f)
        {
            _texture = texture;
            _sourceRectangle = sourceRectangle;
            _position = position - new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2) * scale;
            _scale = scale;
        }

        public void Update(GameTime gameTime)
        {
            // Update the Y-axis position according to the direction
            _position.Y += _direction * (float)(_speed * gameTime.ElapsedGameTime.TotalSeconds);

            // Check bounds and reverse direction
            if (_position.Y < 150) //limit range
            {
                _position.Y = 150;
                _direction = 1; 
            }
            else if (_position.Y > 450) 
            {
                _position.Y = 450;
                _direction = -1; 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(
                _texture,
                _position,
                _sourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}









