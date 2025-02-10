using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Sprint0
{
    public class KeyboardController : IController
    {
        private readonly Dictionary<Keys, Action> _keyMappings;

        public KeyboardController()
        {
            _keyMappings = new Dictionary<Keys, Action>();
        }

        public void RegisterCommand(Keys key, Action action)
        {
            _keyMappings[key] = action;
        }

        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            foreach (var mapping in _keyMappings)
            {
                if (state.IsKeyDown(mapping.Key))
                {
                    mapping.Value?.Invoke();
                }
            }
        }
    }
}
