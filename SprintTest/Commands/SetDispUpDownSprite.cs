using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Sprites;

namespace sprint0Test
{
    class SetDispUpDownSprite : ICommand
    {

        private Game1 myGame;

        public SetDispUpDownSprite(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.sprite = new UpDownAnimatedPlayerSprite(myGame.spriteTexture);
        }
    }
}