using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Link1;
using sprint0Test.Sprites;

namespace sprint0Test
{
    class MoveRightCommand : ICommand
    {

        private Game1 myGame;
        private Link myPlayer;


        //LinkSprite linkSprite = new LinkSprite(linkMap);



        public MoveRightCommand(Game1 game, Link player)
        {
            myGame = game;
            myPlayer = player;
        }


        public void Execute()
        {
            if (myPlayer != null)
                myPlayer.MoveRight(); 
            else
                Console.WriteLine("Error: myPlayer is null in MoveRightCommand.");
        }
    }
}