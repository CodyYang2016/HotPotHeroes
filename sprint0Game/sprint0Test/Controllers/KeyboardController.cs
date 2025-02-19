using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using sprint0Test.Interfaces;
using System.Collections.Generic;

// DISCUSS KEYBOARD STATES

namespace sprint0Test
{

    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> controllerMappings;
        private Game1 myGame;
        private BlockSprites blockSprites;
        private KeyboardState previousState; // Store previous keyboard state


        public KeyboardController(Game1 game, BlockSprites blockSprites)
        {
            myGame = game;
            this.blockSprites = blockSprites;
            controllerMappings = new Dictionary<Keys, ICommand>();
            RegisterCommand();
        }
    
        public void RegisterCommand()
        {
            controllerMappings.Add(Keys.NumPad0, new SetQuitCommand(myGame));
            controllerMappings.Add(Keys.NumPad1, new SetDispFixedSprite(myGame));
            controllerMappings.Add(Keys.NumPad2, new SetDispFixedAnimatedSprite(myGame));
            controllerMappings.Add(Keys.NumPad3, new SetDispUpDownSprite(myGame));
            controllerMappings.Add(Keys.NumPad4, new SetDispLeftRightSprite(myGame));
            controllerMappings.Add(Keys.D0, new SetQuitCommand(myGame));
            controllerMappings.Add(Keys.D1, new SetDispFixedSprite(myGame));
            controllerMappings.Add(Keys.D2, new SetDispFixedAnimatedSprite(myGame));
            controllerMappings.Add(Keys.D3, new SetDispUpDownSprite(myGame));
            controllerMappings.Add(Keys.D4, new SetDispLeftRightSprite(myGame));

            controllerMappings.Add(Keys.Y, new SetBlock(blockSprites));
            controllerMappings.Add(Keys.T, new SetBlock(blockSprites));

            controllerMappings.Add(Keys.O, new PreviousEnemyCommand());
            controllerMappings.Add(Keys.P, new NextEnemyCommand());
            controllerMappings.Add(Keys.L, new EnemyAttackCommand());
            controllerMappings.Add(Keys.J, new MoveEnemyLeftCommand());
            controllerMappings.Add(Keys.K, new MoveEnemyRightCommand());
        }
        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
            Keys[] pressedKeys = currentState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (currentState.IsKeyDown(key) && previousState.IsKeyUp(key)) // Detect new key press
                {
                    controllerMappings[key].Execute();
                }
            }

            previousState = currentState; // Update previous state
        }
    }


}

