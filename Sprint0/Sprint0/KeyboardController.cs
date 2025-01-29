using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using CSE3902;

namespace CSE3902
{
    public class KeyboardController : IController
    {
        private Texture2D marioTexture;
        private Game1 game;
        private float scaleFactor = 4f;

        public KeyboardController(Game1 game)
        {
            this.game = game;
            marioTexture = game.Content.Load<Texture2D>("Sprite"); // Load once
        }

        public void Update(GameTime gameTime) // Ensure it matches IController
        {
            var keyboardStatus = Keyboard.GetState();

            if (keyboardStatus.IsKeyDown(Keys.D1))
            {
                game.SetSprite(new StaticSprite(marioTexture, new Rectangle(0, 10, 16, 18), scaleFactor));
            }
            else if (keyboardStatus.IsKeyDown(Keys.D2))
            {
                game.SetSprite(new AnimatedSprite(
                    marioTexture,
                    new List<Rectangle>
                    {
                        new Rectangle(35, 11, 16, 16),
                        new Rectangle(51, 11, 16, 16)                    
                    },
                    0.1f,
                    scaleFactor
                ));
            }
            else if (keyboardStatus.IsKeyDown(Keys.D3))
            {
                game.SetSprite(new MovingSprite(marioTexture, new Rectangle(0, 10, 16, 18), scaleFactor));
                Console.WriteLine("Pressed 3!");
            }
            else if (keyboardStatus.IsKeyDown(Keys.D4))
            {
                game.SetSprite(new MovingAnimatedSprite(
                    marioTexture,
                    new List<Rectangle>
                    {
                        new Rectangle(35, 11, 16, 16),
                        new Rectangle(51, 11, 16, 16)
                    },
                    0.1f,
                    scaleFactor
                ));
            }
            else if (keyboardStatus.IsKeyDown(Keys.D0))
            {
                game.Exit();
            }

        }
    }
}
