using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using sprint0Test.Interfaces;
using System.Collections.Generic;

namespace sprint0Test
{

    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> controllerMappings;
        private Game1 myGame;

        public KeyboardController(Game1 game)
        {
            myGame = game;
            controllerMappings = new Dictionary<Keys, ICommand>();
            RegisterCommand();
        }
    
        public void RegisterCommand()
        {

            controllerMappings.Add(Keys.D5, new SetDispItemA(myGame));
            controllerMappings.Add(Keys.D6, new SetDispBlockA(myGame));
            controllerMappings.Add(Keys.Q, new SetQuitCommand(myGame));
            controllerMappings.Add(Keys.R, new RestartGameCommand(myGame));
            controllerMappings.Add(Keys.W, new MoveUpCommand(myGame));
            controllerMappings.Add(Keys.A, new MoveLeftCommand(myGame));
            controllerMappings.Add(Keys.S, new MoveDownCommand(myGame));
            controllerMappings.Add(Keys.D, new MoveRightCommand(myGame));

            controllerMappings.Add(Keys.E, new TakeDamageCommand(myGame));
            controllerMappings.Add(Keys.Z, new LinkAttackCommand(myGame));
            controllerMappings.Add(Keys.M, new UseItemCommand(myGame));

        }
        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            bool currentPressedKey = false;
            bool lastPressedKey = false;
            foreach (Keys key in pressedKeys)
            {
                controllerMappings[key].Execute();
            }
        }
    }


}

