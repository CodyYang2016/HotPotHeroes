using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace sprint0Test.Sprites
{

    class textSprite : ISprite
    {
        private Game1 game;
        //private SpriteFont text;
        private String message;
        private Vector2 location;
        //private Vector2 location; 


        public textSprite (Game1 myGame, String text, Vector2 loc)
        {
            game = myGame;
            location = loc;
            message = text;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
           
            sourceRectangle = new Rectangle(28, 0, 28, 36);
            destinationRectangle = new Rectangle(300,
            200, 108, 144);
            
            //spriteBatch.Begin();
            //spriteBatch.DrawString(spriteFont, text, center, Color.Black, 0f, new Vector2(0, 0), 1f, SpriteEffects.None,);
            //spriteBatch.End();
            }
        public void Update()
        {

        }
    }
}