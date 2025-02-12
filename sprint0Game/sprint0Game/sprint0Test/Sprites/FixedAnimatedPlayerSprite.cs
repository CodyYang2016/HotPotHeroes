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
        public FixedAnimatedPlayerSprite (Texture2D texture)
        {
            this.texture = texture;
        }

        /*Player class will start taking care of sprite location and be passed
        to the draw method as a Vector2 location*/
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
            if(currentFrame == 0)
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else if(currentFrame == 1)
            {
                sourceRectangle = new Rectangle(87, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
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
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            else
            {
                sourceRectangle = new Rectangle(58, 0, 29, 36);
                destinationRectangle = new Rectangle(300,
                200, 108, 144);
            }
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
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