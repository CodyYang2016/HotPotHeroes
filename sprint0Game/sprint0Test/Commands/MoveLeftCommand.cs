using System;
using Microsoft.Xna.Framework;
using sprint0Test.Interfaces;
using sprint0Test.Link1;

namespace sprint0Test
{
    class MoveLeftCommand : ICommand
    {
        private Game1 myGame;

        public MoveLeftCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            if (Link.Instance != null)
                Link.Instance.MoveLeft();
            else
                Console.WriteLine("Error: Link.Instance is null in MoveLeftCommand.");
        }
    }
}
