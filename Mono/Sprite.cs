using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mono
{
    public class Sprite : GameObject
    {
        private Texture2D _texture;
        public Vector2 Position;
        public Rectangle sourceRectangle;

        public List<Rectangle> animationCycle;
        //public Rectangle destinationRectangle;
        private int currentFrame;
        private float timeSinceLastFrame;
        private float timePerFrame;


        public Sprite(Texture2D texture)
        {
            _texture = texture; 
            sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);  // Default to using the full texture
            animationCycle = new List<Rectangle>();
            animationCycle.Add(sourceRectangle);

            currentFrame = 0;
            timeSinceLastFrame = 0f;
            timePerFrame = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            // Accumulate time since last frame update
            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If enough time has passed, move to the next frame
            if (timeSinceLastFrame >= timePerFrame && animationCycle.Count > 0)
            {
                // Increment the frame index, wrapping around when needed
                currentFrame = (currentFrame + 1) % animationCycle.Count;
                sourceRectangle = animationCycle[currentFrame]; // Update the sourceRectangle to the new frame

                timeSinceLastFrame = 0f; // Reset the timer
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, sourceRectangle, Color.White);
        }
    }
}
