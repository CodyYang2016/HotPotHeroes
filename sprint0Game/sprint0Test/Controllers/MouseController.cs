using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using sprint0Test.Interfaces;
using System.Collections.Generic;


namespace sprint0Test
{

    public class MouseController : IController
    {
        private List<ICommand> controllerMappings;
        private Game1 myGame;
        Rectangle quad1 = new Rectangle(0, 0, 400, 240);
        Rectangle quad2 = new Rectangle(400, 0, 400, 240);
        Rectangle quad3 = new Rectangle(0, 240, 400, 240);
        Rectangle quad4 = new Rectangle(400, 240, 400, 240);


        public MouseController(Game1 game)
        {
            myGame = game;
            controllerMappings = new List<ICommand>();
            RegisterCommand();
        }
    
        public void RegisterCommand()
        {
            controllerMappings.Add(new SetQuitCommand(myGame));
            controllerMappings.Add(new SetDispFixedSprite(myGame));
            controllerMappings.Add(new SetDispFixedAnimatedSprite(myGame));
            controllerMappings.Add(new SetDispUpDownSprite(myGame));
            controllerMappings.Add(new SetDispLeftRightSprite(myGame));

        }
        public void Update()
        {
            MouseState currentMouseState = Mouse.GetState();
			Point pos = currentMouseState.Position;

            if (currentMouseState.LeftButton == ButtonState.Pressed) LeftClick(pos);
            if (currentMouseState.RightButton == ButtonState.Pressed) RightClick(pos);
        }


        private void RightClick (Point pos)
        {
            this.controllerMappings[0].Execute();
        }
        private void LeftClick (Point pos)
        {
            if (quad1.Contains(pos))
            {
                this.controllerMappings[1].Execute();
            }
            else if (quad2.Contains(pos))
            {
                this.controllerMappings[2].Execute();
            }
            else if (quad3.Contains(pos))
            {
                this.controllerMappings[3].Execute();
            }
            else if (quad4.Contains(pos))
            {
                 this.controllerMappings[4].Execute();
            }

        }
    }


}

