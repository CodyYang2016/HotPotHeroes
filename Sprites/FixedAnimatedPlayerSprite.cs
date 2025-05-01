using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using System.Collections.Generic;

namespace sprint0Test.Sprites
{

    class FixedAnimatedPlayerSprite : ISprite
    {
        private Texture2D texture;
        private int currentFrame = 0;
        private int totalFrames = 4;
        private int threes = 0;
        //private Vector2 location; 
        public FixedAnimatedPlayerSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
            if (currentFrame == 0)
            {

                //destinationRectangle = new Rectangle((int)location.X,
                //(int)location.Y, 56, 72);
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else if (currentFrame == 1)
            {
                sourceRectangle = new Rectangle(87, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else if (currentFrame == 2)
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else if (currentFrame == 3)
            {
                sourceRectangle = new Rectangle(87, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
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