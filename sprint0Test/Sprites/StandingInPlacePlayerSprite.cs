using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace sprint0Test.Sprites
{

    class StandingInPlacePlayerSprite : ISprite
    {
        private Texture2D texture;
        //private Vector2 location; 

        public StandingInPlacePlayerSprite (Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
           
            sourceRectangle = new Rectangle(28, 0, 28, 36);
            destinationRectangle = new Rectangle(300,
            200, 108, 144);
            
            //spriteBatch.Begin();
            //spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            //spriteBatch.End();
            }
        public void Update()
        {

        }
    }
}