using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using CSE3902;

namespace CSE3902
{
    public class MouseController : IController
    {
        private Game1 game;

        public MouseController(Game1 game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            var mouseStatus = Mouse.GetState();
            float scaleFactor = 4f;

            // Exit on right-click
            if (mouseStatus.RightButton == ButtonState.Pressed)
            {
                game.Exit();
            }
            else if (mouseStatus.LeftButton == ButtonState.Pressed)
            {
                int width = game.GraphicsDevice.Viewport.Width;
                int height = game.GraphicsDevice.Viewport.Height;

                if (mouseStatus.X < width / 2 && mouseStatus.Y < height / 2)
                {
                    // Top Left - Static Sprite
                    game.SetSprite(new StaticSprite(
                        game.Content.Load<Texture2D>("Sprite"),
                        new Rectangle(0, 10, 16, 18),
                        scaleFactor
                    ));
                }
                else if (mouseStatus.X >= width / 2 && mouseStatus.Y < height / 2)
                {
                    // Top Right - Animated Sprite
                    game.SetSprite(new AnimatedSprite(
                        game.Content.Load<Texture2D>("Sprite"),
                        new List<Rectangle>
                        {
                            new Rectangle(35, 11, 16, 16),
                            new Rectangle(51, 11, 16, 16)
                        },
                        0.1f,
                        scaleFactor
                    ));
                }
                else if (mouseStatus.X < width / 2 && mouseStatus.Y >= height / 2)
                {
                    // Bottom Left - Moving Sprite
                    game.SetSprite(new MovingSprite(
                        game.Content.Load<Texture2D>("Sprite"),
                        new Rectangle(0, 10, 16, 18),
                        scaleFactor
                    ));
                    Console.WriteLine("Clicked!");
                }
                else if (mouseStatus.X >= width / 2 && mouseStatus.Y >= height / 2)
                {
                    // Bottom Right - Moving Animated Sprite
                    game.SetSprite(new MovingAnimatedSprite(
                        game.Content.Load<Texture2D>("Sprite"),
                        new List<Rectangle>
                        {
                            new Rectangle(35, 11, 16, 16),
                            new Rectangle(51, 11, 16, 16)
                        },
                        0.1f,
                        scaleFactor
                    ));
                }
            }
        }
    }
}
