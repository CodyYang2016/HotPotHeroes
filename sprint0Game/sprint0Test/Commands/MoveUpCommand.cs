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
        private Link myPlayer;


        //LinkSprite linkSprite = new LinkSprite(linkMap);


        public MoveUpCommand(Game1 game, Link player)
        {
            myGame = game;
            myPlayer = player;
            Console.WriteLine(myPlayer == null ? "Error: Link is NULL in Game1!" : "Success: Link is initialized in Game1.");

        }


        public void Execute()
        {
            if (myPlayer != null)
                myPlayer.MoveUp(); 
            else
                Console.WriteLine("Error: myPlayer is null in MoveUpCommand.");
        }
    }
}