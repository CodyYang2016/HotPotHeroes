using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using System.Collections.Generic;

namespace sprint0Test.Sprites
{

    class LeftRightAnimatedPlayerSprite : ISprite
    {
        private Texture2D texture;
        private int currentFrame = 0;
        private int totalFrames = 7;    
        private int threes = 0;
        //private Vector2 location; 
        public LeftRightAnimatedPlayerSprite (Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
            if(currentFrame == 0)
            {
                //destinationRectangle = new Rectangle((int)location.X,
                //(int)location.Y, 56, 72);
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(200,
                200, 108, 144);
            }
            else if(currentFrame == 1)
            {
                sourceRectangle = new Rectangle(87, 0, 29, 36);
                destinationRectangle = new Rectangle(250,
                200, 108, 144);
            }
            else if(currentFrame == 2)
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else if(currentFrame == 3)
            {
                sourceRectangle = new Rectangle(87, 0, 29, 36);
                destinationRectangle = new Rectangle(350,
                200, 108, 144);
            }
            else if(currentFrame == 4)
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(400,
                200, 108, 144);
            }
            else if(currentFrame == 5)
            {
                sourceRectangle = new Rectangle(87, 0, 29, 36);
                destinationRectangle = new Rectangle(350,
                200, 108, 144);
            }
            else if(currentFrame == 6)
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else if(currentFrame == 7)
            {
                sourceRectangle = new Rectangle(87, 0, 29, 36);
                destinationRectangle = new Rectangle(250,
                200, 108, 144);
            }
            else
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(200,
                200, 108, 144);
            }
            //spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            //spriteBatch.End();
        }

        public void Update()
        {
            threes++;
            if (threes % 3 == 0)
            {
                currentFrame++;
            }
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }   
        }
    }
}