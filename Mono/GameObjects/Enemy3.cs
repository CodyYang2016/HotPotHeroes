using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mono
{
    public class Enemy3 : Sprite
    {
        int direction = 1; 
        
        public Enemy3(Texture2D texture, Vector2 position) : base(texture)
        {
            Position = position;
            // Initialize the animation cycle
            sourceRectangle = new Rectangle(191, 185, 16, 16); // Frame 1
            animationCycle = new List<Rectangle>
            {
                sourceRectangle, // Frame 1
                new Rectangle(207, 185, 16, 16), // Frame 2
                new Rectangle(223, 185, 16, 16), // Frame 3
                new Rectangle(239, 185, 16, 16), // Frame 4

            };

        }

        public override void Update(GameTime gameTime)
        {
             
            // If the position is greater than 50, move left
            if (Position.X > 50 && direction == 1)
            {
                Position.X -= 3;  // Move left.
                if (Position.X <= 50)  // When reaching 50, switch direction
                    direction = 0;
            }

            // If the position is less than 300, move right
            if (Position.X < 300 && direction == 0)
            {
                Position.X += 3;  // Move right.
                if (Position.X >= 300)  // When reaching 300, switch direction
                    direction = 1;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);  // Call the base Draw method to draw the sprite with animation.
        }
    }
}
