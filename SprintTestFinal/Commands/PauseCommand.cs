using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;

namespace sprint0Test
{
    class PauseCommand : ICommand
    {
        private Game1 myGame;

        public PauseCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {

            if (myGame._currentGameState == Game1.GameState.Playing)
            {
                    myGame._currentGameState = Game1.GameState.Paused;
            }
            else if (myGame._currentGameState == Game1.GameState.Paused)
            {
                    myGame._currentGameState = Game1.GameState.Playing;
            }
        }
    }
}