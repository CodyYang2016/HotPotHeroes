using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;



namespace sprint0Test
{
    public interface IBlock : ICollidable
    {
        public Vector2 Position { get; }

        public Rectangle SourceRectangle { get; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
        bool IsSolid();
    }
}