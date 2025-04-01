using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Sprites;

namespace sprint0Test
{
    class SetDispBlockA : ICommand
    {

        private Game1 myGame;

        public SetDispBlockA(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            //myGame.sprite = new BlockSpriteClass(myGame.spriteTexture);
        }
    }
}