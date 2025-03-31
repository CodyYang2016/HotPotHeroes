using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Sprites;

namespace sprint0Test.Items
{

public class Boomerang : IItem
    {
        private RotatingAnimatedSprite sprite;
        public Vector2 Position { get; private set; }
        private Vector2 startPosition;
        private Vector2 velocity;
        private bool isReturning;
        private bool isActive;

        private bool isCollected;
        public bool IsCollected => isCollected;

        public Boomerang(Texture2D spriteSheet, Vector2 startPosition, int totalFrames, int framesPerImage = 8)
        {
            this.sprite = new RotatingAnimatedSprite(spriteSheet, totalFrames, framesPerImage);
            this.Position = startPosition;
            this.startPosition = startPosition;
            this.velocity = new Vector2(3, 0); // Moves right initially
            this.isReturning = false;
            this.isActive = true; // Starts inactive
        }

        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                sprite.Update(gameTime); // Update rotation and animation

                // Move in a straight line first
                Position += velocity;

                // If it moves too far, start returning
                if (!isReturning && Vector2.Distance(Position, startPosition) > 1000)
                {
                    isReturning = true;
                    velocity = -velocity; // Reverse direction
                }

                // If it returns to the start, stop moving
                if (isReturning && Vector2.Distance(Position, startPosition) < 5)
                {
                    isActive = false; // Stop moving
                    Position = startPosition;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }

        public void Use()
        {
            if (!isActive) // Only throw if not already thrown
            {
                isActive = true;
                isReturning = false;
                velocity = new Vector2(3, 0); // Re-initialize forward motion
            }
        }
    }
}
