using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test;

    public class BlockPush : ISprite
    {
        public Vector2 Position { get; set; }   // Position on the map
        public Rectangle SourceRectangle { get; private set; } // For the sprite's texture
        public float Scale { get; set; }

        private Texture2D _texture;

        // Constructor
        public BlockPush(Texture2D texture, Rectangle sourceRectangle, Vector2 position, float scale = 1.0f)
        {
            _texture = texture;
            Position = position;
            SourceRectangle = sourceRectangle;
            Scale = scale;
        }

        // Update behavior (if applicable, like animation or physics)
        public virtual void Update()
        {
            // Update logic for interactable objects can go here
        }

        // Draw the object sprite on screen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Use the Position and SourceRectangle for drawing
            Rectangle destination = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)(SourceRectangle.Width * Scale),
                (int)(SourceRectangle.Height * Scale)
            );

            spriteBatch.Draw(_texture, destination, SourceRectangle, Color.White);
        }
    }

