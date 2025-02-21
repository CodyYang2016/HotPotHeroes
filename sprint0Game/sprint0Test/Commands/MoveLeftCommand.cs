using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using sprint0Test.Interfaces;
using sprint0Test.Link1;
using sprint0Test.Sprites;



namespace sprint0Test
{
    class MoveLeftCommand : ICommand
    {

        private Game1 myGame;
        private Link myPlayer;



        public MoveLeftCommand(Game1 game, Link player)
        {
            myGame = game;
            myPlayer = player;
        }

        public void Execute()
        {
            if (myPlayer != null)
                myPlayer.MoveLeft(); 
            else
                Console.WriteLine("Error: myPlayer is null in MoveLeftCommand.");
        }
    }
}