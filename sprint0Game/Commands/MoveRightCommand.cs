using System;
using sprint0Test.Interfaces;
using sprint0Test.Link1;

namespace sprint0Test
{
    class MoveRightCommand : ICommand
    {
        private Game1 myGame;

        public MoveRightCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            if (Link.Instance != null)
                Link.Instance.MoveRight();
            else
                Console.WriteLine("Error: Link.Instance is null in MoveRightCommand.");
        }
    }
}
