using sprint0Test.Link1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Dungeon;

namespace sprint0Test.Commands
{

    class HordeModeCommand : ICommand
    {
        public RoomManager RoomManager { get; private set; }


        private Game1 myGame;

        public HordeModeCommand(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.RoomManager.LoadRoom("r8c");
        }

    }
}
