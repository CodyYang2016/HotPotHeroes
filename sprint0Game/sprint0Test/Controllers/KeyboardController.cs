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
        private Dictionary<Keys, ICommand> controllerMappings;
        private Game1 myGame;
        private BlockSprites blockSprites;
        private KeyboardState previousState; // Store previous keyboard state
        private Link Link;
        Dictionary<(LinkAction, LinkDirection), List<Texture2D>> map = new Dictionary<(LinkAction, LinkDirection), List<Texture2D>>();
        public enum LinkDirection1
        {
            Up, Down, Left, Right
        }

        public enum LinkAction1
        {
            Idle, Walking, Attacking, Damaged, UsingItem
        }

        public LinkAction1 CurrentAction { get; private set; }
        public LinkDirection1 CurrentDirection { get; private set; }


        public KeyboardController(Game1 game, BlockSprites blockSprites)
        {
            myGame = game;
            this.blockSprites = blockSprites;
            controllerMappings = new Dictionary<Keys, ICommand>();
            RegisterCommand();
            LinkSprite linkSprite = new LinkSprite(map);

            Link = new Link(linkSprite, new Vector2(200, 200));
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
            controllerMappings.Add(Keys.U, new CycleItemCommand(myGame, -1));
            controllerMappings.Add(Keys.I, new CycleItemCommand(myGame, 1));
        }
        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
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
        }

        // Method to handle Game1-specific commands (Hardcoded Inputs)
        private void HandleGame1SpecificCommands(KeyboardState kstate)
        {
            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up)) Link.MoveUp();
            else if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down)) Link.MoveDown();
            else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left)) Link.MoveLeft();
            else if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right)) Link.MoveRight();
            // else Link.Stop();

            if (kstate.IsKeyDown(Keys.Z) || kstate.IsKeyDown(Keys.N)) Link.Attack();
            if (kstate.IsKeyDown(Keys.E)) Link.TakeDamage();

            if (kstate.IsKeyDown(Keys.D1)) Link.SwitchItem(1);
            if (kstate.IsKeyDown(Keys.D2)) Link.SwitchItem(-1);

            if (kstate.IsKeyDown(Keys.X) || kstate.IsKeyDown(Keys.M)) Link.UseItem();

            if (kstate.IsKeyDown(Keys.Q)) myGame.Exit(); // Quit game
        }

    }


}

