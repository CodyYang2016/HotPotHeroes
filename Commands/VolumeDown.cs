﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Audio;
using sprint0Test.Interfaces;
using sprint0Test.Link1;
using sprint0Test.Managers;
using sprint0Test.Sprites;


namespace sprint0Test.Commands
{
    class VolumeDown : ICommand
    {

        private Game1 myGame;

        public VolumeDown(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            AudioManager.VolumeDown();
        }
    }
}