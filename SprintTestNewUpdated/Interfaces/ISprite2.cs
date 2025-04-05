using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;



namespace sprint0Test
{
    public interface ISprite2
    {
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Update(GameTime gameTime);

    }
}