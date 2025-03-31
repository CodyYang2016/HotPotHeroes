using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0Test
{
    public interface IBlock : ICollidable
    {
        void Draw(SpriteBatch spriteBatch);
        void Update();
    }
}
