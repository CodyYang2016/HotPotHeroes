/*using Microsoft.Xna.Framework;
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
 
    public class GamePadController : IController
    {
        private Dictionary<Buttons, ICommand> continuousCommands;
        private Dictionary<Buttons, ICommand> singlePressCommands;
        private GamePadState previousGamepadState;
        private Link Link;
        private Game1 myGame;
        private BlockSprites blockSprites;

        public GamePadController(Game1 game, Link link, BlockSprites blockSprites)
        {
            myGame = game;
            this.Link = link;
            this.blockSprites = blockSprites;

            continuousCommands = new Dictionary<Buttons, ICommand>();
            singlePressCommands = new Dictionary<Buttons, ICommand>();

            RegisterCommand();
            previousGamePadState = GamePad.GetState(PlayIndex.One); // Initialize previous state
        }

        public void RegisterCommand()
        {
            // Commands that should execute always when held
            continuousCommands.Add(Buttons.LeftThumbStickUp, new MoveUpCommand(myGame));
            continuousCommands.Add(Buttons.LeftThumbStickLeft, new MoveLeftCommand(myGame));
            continuousCommands.Add(Buttons.LeftThumbStickDown, new MoveDownCommand(myGame));
            continuousCommands.Add(Buttons.LeftThumbStickRight, new MoveRightCommand(myGame));

            continuousCommands.Add(Buttons.DPadUp, new MoveUpCommand(myGame));
            continuousCommands.Add(Buttons.DPadLeft, new MoveLeftCommand(myGame));
            continuousCommands.Add(Buttons.DPadDown, new MoveDownCommand(myGame));
            continuousCommands.Add(Buttons.DPadRight, new MoveRightCommand(myGame));

            // Commands that should execute once when key is pressed
            singlePressCommands.Add(Buttons.Back, new QuitCommand(myGame));
            singlePressCommands.Add(Buttons.Start, new QuitCommand(myGame));
            *//*
            singlePressCommands.Add(Keys.O, new PreviousEnemyCommand());
            singlePressCommands.Add(Keys.P, new NextEnemyCommand());
            singlePressCommands.Add(Keys.L, new EnemyAttackCommand());
            singlePressCommands.Add(Keys.Q, new QuitCommand(myGame));
            singlePressCommands.Add(Keys.Z, new LinkAttackCommand(myGame));
            singlePressCommands.Add(Keys.E, new TakeDamageCommand(myGame));
            singlePressCommands.Add(Keys.Y, new SetBlock(blockSprites));
            singlePressCommands.Add(Keys.U, new CycleItemCommand(myGame, -1));
            singlePressCommands.Add(Keys.I, new CycleItemCommand(myGame, 1));
            *//*
            
        }

        public void Update()
        //probably still has bugs, needs a few more cases handled.
        //Deadzone?
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            Buttons[] pressedButtons = currentState.GetPressedKeys();

            if (!currentState.IsConnected){
                return; // Exit if controller isnt connected
            }

            // Handle continuous commands
            foreach (var button in continuousCommands.Keys)
            {
                if (currentState.IsButtonDown(button))
                {
                    continuousCommands[button].Execute();
                }
            }

            // Handle single-Button commands 
            foreach (var button in singlePressCommands.Keys)
            {
                if (currentState.IsButtonDown(button) && previousGamePadState.IsButtonUp(button))
                {
                    singlePressCommands[button].Execute();
                }
            }

            // Update previous state
            previousGamePadState = currentState;
        }
    }
}

*/