using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test.Sprites
{

    public class RotatingAnimatedSprite : AnimatedSprite
    {
        private float rotationAngle;

        public RotatingAnimatedSprite(Texture2D spriteSheet, int totalFrames, int framesPerImage = 8)
            : base(spriteSheet, totalFrames, framesPerImage)
        {
            rotationAngle = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            rotationAngle += 0.2f; // Rotate slightly every frame
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrameIndex, 0, frameWidth, frameHeight);

            // Draw with rotation
            spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White,
                             rotationAngle, new Vector2(frameWidth / 2, frameHeight / 2), 1.0f,
                             SpriteEffects.None, 0);
        }
    }
}
