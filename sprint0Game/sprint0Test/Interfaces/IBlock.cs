using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;



namespace sprint0Test
{
    public interface IBlock
    {

        public Vector2 Position { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
    
    }
}