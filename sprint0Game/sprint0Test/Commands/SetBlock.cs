using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0Test.Interfaces;

namespace sprint0Test
{
    class SetBlock : ICommand
    {
        private Game1 myGame;

        public SetBlock(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            // Access the list of game objects
            var _gameObjects = myGame.GetGameObjects();  // Assuming GetGameObjects() returns the list of all game objects

            // Increment the current index and cycle it if needed
            int currentIndex = myGame.GetCurrentIndex();  // Assuming this tracks the current index of the game object
            currentIndex = (currentIndex + 1) % _gameObjects.Count;  // Cycle the index

            // Set the new active object
            myGame.SetActiveList(_gameObjects[currentIndex]);

            // Update the index back to Game1 (if necessary)
            myGame.SetCurrentIndex(currentIndex);
            
        }
    }
}