using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    /// <summary>
    /// Left and right animation:
    /// - If direction=1 (right), play framesRight;
    /// - If direction=-1 (left), play framesLeft.
    /// </summary>
    public class MovingAnimatedSprite : ISprite
    {
        private Texture2D[] _framesRight;  // Right animation frame
        private Texture2D[] _framesLeft;   // left animation frame

        private Texture2D[] _currentFrames; // The frame array currently being used

        private int _currentFrameIndex;
        private double _elapsedTime;
        private double _frameTime;

        private Vector2 _position;
        private float _scale;

        private float _speed = 100f;
        private int _direction = 1; // 1 = right, -1 = left

        // Moving range (can be adjusted)
        private float _leftBoundary = 200f;
        private float _rightBoundary = 600f;

        public MovingAnimatedSprite(
            Texture2D[] framesRight,
            Texture2D[] framesLeft,
            double frameTime,
            Vector2 position,
            float scale = 1.0f)
        {
            _framesRight = framesRight;
            _framesLeft = framesLeft;

            _frameTime = frameTime;
            _currentFrameIndex = 0;
            _elapsedTime = 0.0;

            // The initial direction is right, so the current frame array is set to _framesRight
            _currentFrames = _framesRight;

            // Align the center of the character image to position
            Texture2D firstFrame = _framesRight[0];
            _position = position - new Vector2(firstFrame.Width / 2f, firstFrame.Height / 2f) * scale;
            _scale = scale;
        }

        public void Update(GameTime gameTime)
        {
            // Update animation frame
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (_elapsedTime >= _frameTime)
            {
                _currentFrameIndex = (_currentFrameIndex + 1) % _currentFrames.Length;
                _elapsedTime = 0.0;
            }

            // move
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position.X += _direction * _speed * delta;

            // Boundary Detection
            if (_position.X > _rightBoundary)
            {
                _position.X = _rightBoundary;
                _direction = -1;//Change to the left
            }
            else if (_position.X < _leftBoundary)
            {
                _position.X = _leftBoundary;
                _direction = 1; //Change to the right
            }

            // Select frame array according to direction
            if (_direction == 1)
            {
                _currentFrames = _framesRight;
            }
            else
            {
                _currentFrames = _framesLeft;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D currentFrame = _currentFrames[_currentFrameIndex];

            spriteBatch.Draw(
                currentFrame,
                _position,
                null,
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















