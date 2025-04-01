using System;
using System.Collections.Generic;
using sprint0Test.Interfaces;

namespace sprint0Test
{
    class NextBlock : ICommand
    {
        private BlockSprites blockSprites;

        public NextBlock(BlockSprites blockSprites)
        {
            this.blockSprites = blockSprites;
        }

        public void Execute()
        {
            // Access the list of game objects from BlockSprites
            var _gameObjects = blockSprites.GetGameObjects();

            // Increment the current index and cycle it if needed
            int currentIndex = blockSprites.GetCurrentIndex();  // Use BlockSprites to get the current index
            currentIndex = (currentIndex + 1) % _gameObjects.Count;  // Cycle the index

            // Set the new active object using BlockSprites
            blockSprites.SetActiveList(_gameObjects[currentIndex]);

            // Update the index back to BlockSprites
            blockSprites.SetCurrentIndex(currentIndex);
        }
    }
}
