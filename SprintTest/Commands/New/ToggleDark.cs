using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Link1;
using sprint0Test.Managers;
using sprint0Test.Sprites;


namespace sprint0Test.Commands
{
    class ToggleDark : ICommand
    {

        private Game1 myGame;

        public ToggleDark(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            ShaderManager.ToggleDarkness();
        }
    }
}
