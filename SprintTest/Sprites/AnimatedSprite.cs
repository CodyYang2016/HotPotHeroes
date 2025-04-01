using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test.Sprites
{
    public class AnimatedSprite : ISprite2
    {
        protected Texture2D spriteSheet;
        protected int frameWidth;
        protected int frameHeight;
        protected int totalFrames;
        protected int currentFrameIndex;
        protected int frameCounter;
        protected int framesPerImage;

        public AnimatedSprite(Texture2D spriteSheet, int totalFrames, int framesPerImage = 8)
        {
            this.spriteSheet = spriteSheet;
            this.totalFrames = totalFrames;
            this.framesPerImage = framesPerImage;
            this.currentFrameIndex = 0;
            this.frameCounter = 0;

            // Calculate frame dimensions
            this.frameWidth = spriteSheet.Width / totalFrames;
            this.frameHeight = spriteSheet.Height;
        }

        public virtual void Update(GameTime gameTime)
        {
            frameCounter++;
            if (frameCounter > framesPerImage)
            {
                frameCounter = 0;
                currentFrameIndex = (currentFrameIndex + 1) % totalFrames;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Extract the correct frame from the sprite sheet
            Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrameIndex, 0, frameWidth, frameHeight);
            spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White);
        }
    }
}
