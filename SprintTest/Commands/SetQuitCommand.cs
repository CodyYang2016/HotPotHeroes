using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;

namespace sprint0Test
{
    class SetQuitCommand : ICommand
    {
        private Game1 myGame;

        public SetQuitCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.Exit();
        }
    }
}