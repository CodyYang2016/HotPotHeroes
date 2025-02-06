using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CSE3902
{
    public class AnimatedSprite : ISprite
    {
        private Texture2D texture;
        private List<Rectangle> frames;
        private float frameTimer;
        private float timer;
        private int presentFrame;
        private Vector2 position;
        public float Scale { get; set; }

        public AnimatedSprite(Texture2D t, List<Rectangle> f, float fT, float s = 1.0f)
        {
            texture = t;
            frames = f;
            frameTimer = fT;
            timer = 0f;
            presentFrame = 0;
            Scale = s;

            // Set position to center of screen (assuming 800x480 screen)
            position = new Vector2(
                (800 - frames[0].Width * Scale) / 2,
                (480 - frames[0].Height * Scale) / 2
            );
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Switch frames based on frameTimer
            if (timer > frameTimer)
            {
                presentFrame++;
                presentFrame %= frames.Count;  // Loop through frames
                timer = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = frames[presentFrame];
            Rectangle destination = new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)(source.Width * Scale),
                (int)(source.Height * Scale)
            );

            spriteBatch.Draw(texture, destination, source, Color.White);
        }
    }
}
