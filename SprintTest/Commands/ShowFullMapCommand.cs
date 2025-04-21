using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using sprint0Test.Interfaces;

namespace sprint0Test.Commands
{
    public class ShowFullMapCommand : ICommand
    {
        private Game1 myGame;

        public ShowFullMapCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.ToggleFullMap();
        }
    }
}
