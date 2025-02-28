using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test.Sprites
{
    public class StaticSprite : ISprite2
    {
        private Texture2D texture;

        public StaticSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            // Static sprites don’t need animation updates
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
