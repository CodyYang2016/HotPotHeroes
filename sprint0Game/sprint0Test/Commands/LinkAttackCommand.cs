using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Sprites;

namespace sprint0Test
{
    class LinkAttackCommand : ICommand
    {

        private Game1 myGame;
        private Link myPlayer;

        public LinkAttackCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myPlayer.Attack();
            //myGame.sprite = new BlockSpriteClass(myGame.spriteTexture);
        }
    }
}