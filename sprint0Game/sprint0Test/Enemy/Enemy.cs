using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sprint0Test.Enemy
{
    public class Enemy
    {
        // look into extracting currentFrame, totalFrames, texture
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
            position = startPosition;
            velocity = new Vector2(2, 0); //change
            isAnimating = true;
            animationTimer = 0f;
            currentFrame = 0;
            this.totalFrames = totalFrames;
        }

        public void Update(GameTime gameTime)
        {

            position += velocity;
            if (position.X > 800 || position.X < 0)  //change
                velocity *= -1;


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
