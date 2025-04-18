using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using sprint0Test.Interfaces;
using System.Collections.Generic;
using sprint0Test.Commands;
using sprint0Test.Link1;
using System.Linq;


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
        // 用于收集按键历史，检测康米码
        private readonly List<Keys> cheatBuffer = new List<Keys>();
        private static readonly Keys[] konami = new[]
        {
            Keys.Up, Keys.Up,
            Keys.Down, Keys.Down,
            Keys.Left, Keys.Right,
            Keys.Left, Keys.Right,
            Keys.B,   Keys.A,
            Keys.B,   Keys.A
         };
        // 补血秘籍序列
        private static readonly Keys[] refillSeq = new[]
        {
           Keys.Left, Keys.Left,
           Keys.Right, Keys.Right,
           Keys.B,    Keys.A
         };

        //private BlockSprites blockSprites;

        public KeyboardController(Game1 game, Link link)
        {
            myGame = game;
            this.Link = link;


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
            singlePressCommands.Add(Keys.O, new PreviousEnemyCommand());
            singlePressCommands.Add(Keys.P, new NextEnemyCommand());
            singlePressCommands.Add(Keys.L, new EnemyAttackCommand());
            singlePressCommands.Add(Keys.Q, new QuitCommand(myGame));
            singlePressCommands.Add(Keys.Z, new LinkAttackCommand(myGame));
            singlePressCommands.Add(Keys.E, new TakeDamageCommand(myGame));
            singlePressCommands.Add(Keys.U, new CycleItemCommand(myGame, -1));
            singlePressCommands.Add(Keys.I, new CycleItemCommand(myGame, 1));
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
            // —— 康米码检测 —— //
            foreach (var key in pressedKeys)
            {
                if (previousKeyboardState.IsKeyUp(key))
                {
                    cheatBuffer.Add(key);
                    if (cheatBuffer.Count > konami.Length)
                        cheatBuffer.RemoveAt(0);
                    if (cheatBuffer.SequenceEqual(konami))
                    {
                        Game1.Instance.ToggleGodMode();
                        cheatBuffer.Clear();
                    }
                    else if (cheatBuffer.SequenceEqual(refillSeq))
                    {
                        Game1.Instance.RefillHealth();
                        cheatBuffer.Clear();
                    }
                }
            }


            // Update previous state
            previousKeyboardState = currentState;
        }
    }
}

