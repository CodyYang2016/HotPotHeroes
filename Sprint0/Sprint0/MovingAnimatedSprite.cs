using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902
{
    public class MovingAnimatedSprite : ISprite
    {
        private Texture2D texture;
        private List<Rectangle> frames;
        private float frameTimer;
        private float timer;
        private int presentFrame;
        private Vector2 point;
        private Vector2 direction;
        public float Scale { get; set; }

        public MovingAnimatedSprite(Texture2D t, List<Rectangle> f, float fT, float s = 1.0f)
        {
            if (f == null || f.Count == 0)
                throw new ArgumentException("Frame list cannot be null or empty");

            texture = t;
            frames = f;
            frameTimer = fT;
            timer = 0f;
            presentFrame = 0;
            Scale = s;

            // Set initial position (centered)
            point = new Vector2(
                (800 - frames[0].Width * Scale) / 2,
                (480 - frames[0].Height * Scale) / 2
            );

            // Moving right initially
            direction = new Vector2(2, 0);
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Switch frames when timer exceeds frameTimer
            if (timer > frameTimer)
            {
                presentFrame++;
                presentFrame %= frames.Count;
                timer = 0f;
            }

            // Move sprite left/right
            point += direction;

            // Reverse direction at boundaries
            if (point.X < 0 || point.X > 800 - frames[presentFrame].Width * Scale)
            {
                direction.X *= -1;
                point.X = Math.Clamp(point.X, 0, 800 - frames[presentFrame].Width * Scale);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = frames[presentFrame];

            Rectangle destination = new Rectangle(
                (int)point.X,
                (int)point.Y,
                (int)(source.Width * Scale),
                (int)(source.Height * Scale)
            );

            spriteBatch.Draw(texture, destination, source, Color.White);
        }
    }
}
