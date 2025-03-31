using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test;

    public enum ObjectType
    {
        Staircase,
        Rock,
        Other,
        // Add more object types as needed (e.g., Door, Chest, etc.)
    }

    public class Block : IBlock
    {
        public Vector2 Position { get; set; }   // Position on the map
        public Rectangle SourceRectangle { get; private set; } // For the sprite's texture
        public float Scale { get; set; }
        public bool IsVisible { get; set; }  // Flag to indicate if the block is visible

        private Texture2D _texture;

        public Block(Texture2D texture, Rectangle sourceRectangle, Vector2 position, float scale = 1.0f, bool isVisible = true)
        {
            _texture = texture;
            Position = position;
            SourceRectangle = sourceRectangle;
            Scale = scale;
            IsVisible = isVisible;
        }

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

            // if (IsVisible)
            // {
                spriteBatch.Draw(_texture, destination, SourceRectangle, Color.White);
            // }
        }

    }

