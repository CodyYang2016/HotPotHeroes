using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using sprint0Test.Interfaces;
using System.Collections.Generic;
using sprint0Test.Commands;
using sprint0Test.Link1;

// DISCUSS KEYBOARD STATES

namespace sprint0Test
{
 
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> continuousCommands;
        private Dictionary<Keys, ICommand> singlePressCommands;
        private KeyboardState previousKeyboardState;
        private Link Link;
        private Game1 myGame;
        private BlockSprites blockSprites;

        public KeyboardController(Game1 game, Link link, BlockSprites blockSprites)
        {
            myGame = game;
            this.Link = link;
            this.blockSprites = blockSprites;

            continuousCommands = new Dictionary<Keys, ICommand>();
            singlePressCommands = new Dictionary<Keys, ICommand>();

            RegisterCommand();
            previousKeyboardState = Keyboard.GetState(); // Initialize previous state
        }

        public void RegisterCommand()
        {
            // Commands that should execute always when held
            continuousCommands.Add(Keys.W, new MoveUpCommand(myGame));
            continuousCommands.Add(Keys.A, new MoveLeftCommand(myGame));
            continuousCommands.Add(Keys.S, new MoveDownCommand(myGame));
            continuousCommands.Add(Keys.D, new MoveRightCommand(myGame));

            // Commands that should execute once when key is pressed

            singlePressCommands.Add(Keys.F2, new NextEnemyCommand());
            singlePressCommands.Add(Keys.F1, new PreviousEnemyCommand());
            
            singlePressCommands.Add(Keys.E, new LinkAttackCommand(myGame));
            singlePressCommands.Add(Keys.R, new TakeDamageCommand(myGame));
            singlePressCommands.Add(Keys.F3, new SetBlock(blockSprites));
            singlePressCommands.Add(Keys.F4, new CycleItemCommand(myGame, -1));
            singlePressCommands.Add(Keys.F5, new CycleItemCommand(myGame, 1));

            singlePressCommands.Add(Keys.Q, new QuitCommand(myGame));
            singlePressCommands.Add(Keys.P, new PauseCommand(myGame));

        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
            Keys[] pressedKeys = currentState.GetPressedKeys();

            // Handle continuous commands 
            foreach (Keys key in pressedKeys)
            {
                if (continuousCommands.TryGetValue(key, out var command))
                {
                    command.Execute();
                }
            }

            // Handle single click commands 
            foreach (Keys key in pressedKeys)
            {
                if (singlePressCommands.TryGetValue(key, out var command) &&
                    previousKeyboardState.IsKeyUp(key)) //only run once per press
                {
                    command.Execute();
                }
            }

            // Update previous state
            previousKeyboardState = currentState;
        }
    }
}

