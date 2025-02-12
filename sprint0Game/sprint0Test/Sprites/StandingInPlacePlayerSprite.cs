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

        public StandingInPlacePlayerSprite (Texture2D texture)
        {
            this.texture = texture;
        }

        /*Player class will start taking care of sprite location and be passed
        to the draw method as a Vector2 location*/
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
           
            sourceRectangle = new Rectangle(28, 0, 28, 36);
            destinationRectangle = new Rectangle(300,
            200, 108, 144);
            
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            }
        public void Update()
        {

        }
    }
}