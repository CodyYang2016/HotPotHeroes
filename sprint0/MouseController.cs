using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    public class MouseController : IController
    {
        private readonly Action[] _actions;

        public MouseController(Action[] actions)
        {
            _actions = actions;
        }

        public void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();

            // Check left mouse click
            if (state.LeftButton == ButtonState.Pressed)
            {
                int screenWidth = 800; // Replace with your screen width
                int screenHeight = 480; // Replace with your screen height

                if (state.X < screenWidth / 2 && state.Y < screenHeight / 2)
                    _actions[0]?.Invoke(); // Top-left (Quad1)
                else if (state.X >= screenWidth / 2 && state.Y < screenHeight / 2)
                    _actions[1]?.Invoke(); // Top-right (Quad2)
                else if (state.X < screenWidth / 2 && state.Y >= screenHeight / 2)
                    _actions[2]?.Invoke(); // Bottom-left (Quad3)
                else if (state.X >= screenWidth / 2 && state.Y >= screenHeight / 2)
                    _actions[3]?.Invoke(); // Bottom-right (Quad4)
            }

            // Check right mouse click
            if (state.RightButton == ButtonState.Pressed)
            {
                _actions[4]?.Invoke(); // Quit
            }
        }
    }
}
