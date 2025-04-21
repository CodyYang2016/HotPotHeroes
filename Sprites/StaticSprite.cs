using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace sprint0Test.Sprites
{
    public class StaticSprite : ISprite2
    {
        private Texture2D texture;
        private float scale;
        public StaticSprite(Texture2D texture, float scale = 1.0f)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture), "❌ Tried to create a StaticSprite with a null texture!");

            this.texture = texture;
            this.scale = scale;
        }

        public void Update(GameTime gameTime)
        {
            // Static sprites don’t need animation updates
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(
        texture,
        position,
        null,
        Color.White,
        0f,
        new Vector2(texture.Width / 2f, texture.Height / 2f),
        scale,
        SpriteEffects.None,
        0f
    );
        }
    }
}