using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using sprint0Test.Interfaces;
using sprint0Test.Link1;
using sprint0Test.Sprites;




namespace sprint0Test.Commands
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
