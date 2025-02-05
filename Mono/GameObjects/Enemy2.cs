using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mono
{
    public class Enemy2 : Sprite
    {
        int direction = 1; 
        // Constructor for the Enemy3 class, initializing the base Sprite class.
        public Enemy2(Texture2D texture, Vector2 position) : base(texture)
        {
            Position = position;
            // Initialize the animation cycle 
            sourceRectangle = new Rectangle(191, 185, 16, 16); // Frame 1
            animationCycle = new List<Rectangle>
            {
                sourceRectangle
            };

        }

        public override void Update(GameTime gameTime)
        {
             
            // If the position is greater than 50, move left
            if (Position.Y > 50 && direction == 1)
            {
                Position.Y -= 3;  // Move left.
                if (Position.Y <= 50)  // When reaching 50, switch direction
                    direction = 0;
            }

            // If the position is less than 300, move right
            if (Position.Y < 300 && direction == 0)
            {
                Position.Y += 3;  // Move right.
                if (Position.Y >= 300)  // When reaching 300, switch direction
                    direction = 1;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); 
        }
    }
}



// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace Mono
// {
//     public class Enemy2 : GameObject
//     {
//         private Texture2D _texture;
//         public Rectangle SourceRectangle;

//         public Enemy2(Texture2D texture, Vector2 position)
//         {
//             _texture = texture;
//             Position = position;
//             SourceRectangle = new Rectangle(191, 185, 16, 16);
//         }

//         public override void Update(GameTime gameTime)
//         {
//             // Example enemy movement behavior
//             Position.Y += 1; // Move left
//         }

//         public override void Draw(SpriteBatch spriteBatch)
//         {
//             spriteBatch.Draw(_texture, Position, SourceRectangle, Color.Red); // Draw the enemy in red
//         }
//     }
// }
