using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Sprites;

namespace sprint0Test
{
    class SetDispFixedAnimatedSprite : ICommand
    {
        private Game1 myGame;

        public SetDispFixedAnimatedSprite(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            // ÐèÒª myGame.spriteTexture ´æÔÚ
            if (myGame.spriteTexture != null)
            {
                myGame.sprite = new FixedAnimatedPlayerSprite(myGame.spriteTexture);
            }
        }
    }
}