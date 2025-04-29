using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;
using sprint0Test.Sprites;
using sprint0Test.Link1;
using sprint0Test.Audio;

namespace sprint0Test.Commands
{
    class LinkAttackCommand : ICommand
    {

        private Game1 myGame;

        public LinkAttackCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {

            if (Link.Instance != null)
            {
                SoundManager.Instance.PlaySound(SoundList.sword);
                Link.Instance.Attack();
            }
            else
            {
                Console.WriteLine("Error: Link.Instance is null in LinkAttackCommand.");
            }
        }
    }
}