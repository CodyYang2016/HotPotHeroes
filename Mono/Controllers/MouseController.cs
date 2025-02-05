using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mono
{
    public class MouseController : IController
    {
        //private Sprite _sprite; // Reference to the sprite
        private Game1 _game;

        public MouseController(Game1 game)  // , Sprite sprite)
        {
            //_sprite = sprite;
            _game = game;
        }

        public void Update()
        {
            var mstate = Mouse.GetState();

            // Move the sprite to the mouse position
            //_sprite.Position = new Vector2(mstate.X, mstate.Y);

            int screenWidth = _game.GraphicsDevice.Viewport.Width;
            int screenHeight = _game.GraphicsDevice.Viewport.Height;

            int midX = screenWidth / 2;
            int midY = screenHeight / 2;

            // Move sprite to the mouse position
            // _sprite.Position = new Vector2(mstate.X, mstate.Y);

            // Left mouse button
            if (mstate.RightButton == ButtonState.Pressed)
            {
                _game.Exit();
            }

            if (mstate.LeftButton == ButtonState.Pressed && mstate.X < midX && mstate.Y < midY)
            {
                _game.Scene1();
            }

            if (mstate.LeftButton == ButtonState.Pressed && mstate.X > midX && mstate.Y > midY)
            {
                _game.Scene4();
            }

            if (mstate.LeftButton == ButtonState.Pressed && mstate.X < midX && mstate.Y > midY)
            {
                _game.Scene3();
            }

            if (mstate.LeftButton == ButtonState.Pressed && mstate.X > midX && mstate.Y < midY)
            {
                _game.Scene2();
            }

        }
    }
}
