using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using sprint0Test.Interfaces;
using System.Collections.Generic;
//using sprint0Test.Commands;
using sprint0Test.Link1;

namespace sprint0Test
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> controllerMappings;
        private Link Link;
        private Game1 myGame;
        private BlockSprites blockSprites;

        public KeyboardController(Game1 game, Link link, BlockSprites blockSprites)
        {
            myGame = game;
            this.Link = link;
            this.blockSprites = blockSprites;
            controllerMappings = new Dictionary<Keys, ICommand>();
            RegisterCommand();
        }

        public void RegisterCommand()
        {
            controllerMappings.Add(Keys.O, new PreviousEnemyCommand());
            controllerMappings.Add(Keys.P, new NextEnemyCommand());
            controllerMappings.Add(Keys.L, new EnemyAttackCommand());
            controllerMappings.Add(Keys.J, new MoveEnemyLeftCommand());
            controllerMappings.Add(Keys.K, new MoveEnemyRightCommand());
            controllerMappings.Add(Keys.Q, new QuitCommand(myGame));
            controllerMappings.Add(Keys.W, new MoveUpCommand(myGame, Link));
            controllerMappings.Add(Keys.A, new MoveLeftCommand(myGame, Link));
            controllerMappings.Add(Keys.S, new MoveDownCommand(myGame, Link));
            controllerMappings.Add(Keys.D, new MoveRightCommand(myGame, Link));
            controllerMappings.Add(Keys.Z, new LinkAttackCommand(myGame, Link));
            controllerMappings.Add(Keys.E, new TakeDamageCommand(myGame, Link));
            controllerMappings.Add(Keys.Y, new SetBlock(blockSprites));
            controllerMappings.Add(Keys.T, new SetBlock(blockSprites));
            //controllerMappings.Add(Keys.U, new CycleItemCommand(myGame, -1));
            //controllerMappings.Add(Keys.I, new CycleItemCommand(myGame, 1));
        }

        public void HandleGame1SpecificCommands(KeyboardState kstate)
        {
            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up)) Link.MoveUp();
            else if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down)) Link.MoveDown();
            else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left)) Link.MoveLeft();
            else if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right)) Link.MoveRight();

            if (kstate.IsKeyDown(Keys.Z) || kstate.IsKeyDown(Keys.N)) Link.Attack();
            if (kstate.IsKeyDown(Keys.E)) Link.TakeDamage();
            if (kstate.IsKeyDown(Keys.D1)) Link.SwitchItem(1);
            if (kstate.IsKeyDown(Keys.D2)) Link.SwitchItem(-1);
            if (kstate.IsKeyDown(Keys.X) || kstate.IsKeyDown(Keys.M)) Link.UseItem();
            if (kstate.IsKeyDown(Keys.Q)) myGame.Exit();
        }

        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in pressedKeys)
            {
                if (controllerMappings.ContainsKey(key))
                {
                    controllerMappings[key].Execute();
                }
            }
        }
    }
}
            //The code below worked for stopping infinate loops but broke everything else, 
            //im saving it cause its probably a little thing I can fix and use
           /* KeyboardState currentState = Keyboard.GetState();
            Keys[] pressedKeys = currentState.GetPressedKeys();

            bool handled = false;

            // Process all pressed keys
            foreach (Keys key in pressedKeys)
            {
                if (currentState.IsKeyDown(key) && previousState.IsKeyUp(key)) // Detect new key press
                {
                    // Check if the key exists in controllerMappings
                    if (controllerMappings.TryGetValue(key, out var command))
                    {
                        command.Execute();
                        handled = true; // Mark as handled
                    }
                }
            }

            // Always process Game1-specific hardcoded commands
            HandleGame1SpecificCommands(currentState);

            previousState = currentState; // Update previous state
            */
        //} 

        // Method to handle Game1-specific commands (Hardcoded Inputs)*/
    




