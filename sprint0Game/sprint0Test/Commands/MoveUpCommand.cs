using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Link1;
using sprint0Test.Sprites;


namespace sprint0Test
{
    class MoveUpCommand : ICommand
    {

        private Game1 myGame;

        public MoveUpCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            if (Link.Instance != null)
                Link.Instance.MoveUp();
            else
                Console.WriteLine("Error: Link.Instance is null in MoveUpCommand.");
        }
    }
}
