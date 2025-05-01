using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace sprint0Test
{
    public class PauseMenu
    {
        private SpriteFont _font;
        private int _selectedIndex = 0;
        private string[] _options = { "Resume", "Restart", "Quit" };
        private KeyboardState _previousKeyboardState; // Tracks previous state
        public Action<int> OnOptionSelected;

        public PauseMenu(SpriteFont font)
        {
            _font = font;
        }

        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (WasKeyPressed(Keys.Down, currentKeyboardState))
            {
                _selectedIndex = (_selectedIndex + 1) % _options.Length;
            }
            else if (WasKeyPressed(Keys.Up, currentKeyboardState))
            {
                _selectedIndex = (_selectedIndex - 1 + _options.Length) % _options.Length;
            }
            else if (WasKeyPressed(Keys.Enter, currentKeyboardState))
            {
                OnOptionSelected?.Invoke(_selectedIndex);
            }

            _previousKeyboardState = currentKeyboardState; // Save state for next frame
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            Vector2 position = new Vector2(graphicsDevice.Viewport.Width / 2 - 50, graphicsDevice.Viewport.Height / 2 - 50);
            for (int i = 0; i < _options.Length; i++)
            {
                Color color = (i == _selectedIndex) ? Color.Yellow : Color.White;
                spriteBatch.DrawString(_font, _options[i], position, color);
                position.Y += 40;
            }
        }

        private bool WasKeyPressed(Keys key, KeyboardState currentState)
        {
            return currentState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }
    }
}