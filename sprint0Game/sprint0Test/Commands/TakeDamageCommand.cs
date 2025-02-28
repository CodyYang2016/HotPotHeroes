using System;
using sprint0Test.Interfaces;
using sprint0Test.Link1;

namespace sprint0Test
{
    class TakeDamageCommand : ICommand
    {
        private Game1 myGame;

        public TakeDamageCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            if (Link.Instance != null)
                Link.Instance.TakeDamage();
            else
                Console.WriteLine("Error: Link.Instance is null in TakeDamageCommand.");
        }
    }
}
