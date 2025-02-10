using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    public class AnimatedSprite : ISprite
    {
        private Texture2D[] _frames; // Storing multiple frame textures
        private int _currentFrameIndex;
        private double _elapsedTime;
        private double _frameTime;   // Control the duration of each frame

        private Vector2 _position;
        private float _scale;

        public AnimatedSprite(
            Texture2D[] frames,
            double frameTime,
            Vector2 position,
            float scale = 1.0f)
        {
            _frames = frames;
            _frameTime = frameTime;

            _currentFrameIndex = 0;
            _elapsedTime = 0.0;

            // Align the character image center to the specified position
            Texture2D firstFrame = _frames[0];
            _position = position - new Vector2(firstFrame.Width / 2f, firstFrame.Height / 2f) * scale;
            _scale = scale;
        }

        public void Update(GameTime gameTime)
        {
            // Cumulative time, used to determine whether to switch to the next frame
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (_elapsedTime >= _frameTime)
            {
                // Cut to next frame
                _currentFrameIndex = (_currentFrameIndex + 1) % _frames.Length;
                _elapsedTime = 0.0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the current frame texture directly
            Texture2D currentFrame = _frames[_currentFrameIndex];

            spriteBatch.Draw(
                currentFrame,
                _position,
                null,
                Color.White,
                0f,
                Vector2.Zero, // No special origin required
                _scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}










