using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sprint0Test.Sprites
{
    public class Enemy
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private bool isAnimating;
        private float animationTimer;
        private float animationInterval = 0.2f;
        private int currentFrame;
        private int totalFrames;

        public Enemy(Texture2D texture, Vector2 startPosition, int totalFrames)
        {
            this.texture = texture;
            this.position = startPosition;
            this.velocity = new Vector2(2, 0); // Example movement speed
            this.isAnimating = true;
            this.animationTimer = 0f;
            this.currentFrame = 0;
            this.totalFrames = totalFrames;
        }

        public void Update(GameTime gameTime)
        {
            // Update movement
            position += velocity;
            if (position.X > 800 || position.X < 0) // Example screen bounds
                velocity *= -1;

            // Update animation
            if (isAnimating)
            {
                animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animationTimer > animationInterval)
                {
                    currentFrame = (currentFrame + 1) % totalFrames;
                    animationTimer = 0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
