using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mono
{
    public class Enemy : Sprite
    {
        
        public Enemy(Texture2D texture, Vector2 position) : base(texture)
        {
            Position = position;

            sourceRectangle = new Rectangle(191, 185, 16, 16); // Frame 1
            animationCycle = new List<Rectangle>
            {
                sourceRectangle,
                new Rectangle(207, 185, 16, 16), // Frame 2
                new Rectangle(223, 185, 16, 16), // Frame 3
                new Rectangle(239, 185, 16, 16), // Frame 4

            };

        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); 
        }
    }
}
