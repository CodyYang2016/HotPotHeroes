using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace sprint0Test.Commands
{
    public class CycleItemCommand : ICommand
    {
        private Game1 game;
        private int direction; // 1 for forward, -1 for backward

        public CycleItemCommand(Game1 game, int direction)
        {
            this.game = game;
            this.direction = direction;
        }

        public void Execute()
        {
            if (game.itemList.Count == 0) return;
            // Move forward or backward in item list
            game.currentItemIndex = (game.currentItemIndex + direction + game.itemList.Count) % game.itemList.Count;
            game.currentItem = game.itemList[game.currentItemIndex];
        }
    }

}
*/