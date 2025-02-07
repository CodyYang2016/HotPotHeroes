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

    public class Block : ISprite
    {
        public Vector2 Position { get; set; }   // Position on the map
        public ObjectType Type { get; set; }    // Type of the object (e.g., Staircase, Rock)
        public Rectangle SourceRectangle { get; private set; } // For the sprite's texture
        public float Scale { get; set; }

        private Texture2D _texture;

        // Constructor
        public Block(Texture2D texture, Rectangle sourceRectangle, ObjectType type, Vector2 position, float scale = 1.0f)
        {
            _texture = texture;
            Position = position;
            Type = type;
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

        // Interact method (overridden by specific objects for custom behavior)
        public virtual void Interact()
        {
            switch (Type)
            {
                case ObjectType.Staircase:
                    // Logic for moving to a different room
                    break;
                case ObjectType.Rock:
                    // Logic for pushing the rock
                    break;
                // Add cases for more types of objects
            }
        }
    }

