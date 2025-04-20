using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test
{
    public abstract class AbstractBlock : IBlock
    {
        public Vector2 Position { get; protected set; }
        public Rectangle SourceRectangle { get; protected set; }
        protected Texture2D _texture;
        protected float scale;
        public bool IsVisible { get; set; }
        protected bool isSolid;
        public AbstractBlock(Texture2D texture, Vector2 position, Rectangle sourceRectangle, float scale = 1.0f, bool isVisible = true, bool isSolid = true)
        {
            _texture = texture;
            Position = position;
            SourceRectangle = sourceRectangle;
            this.scale = scale;
            IsVisible = isVisible;
            this.isSolid = isSolid;
        }

        public virtual void Update()
        {
            // Default update logic, can be overridden
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                Rectangle destination = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(SourceRectangle.Width * scale),
                    (int)(SourceRectangle.Height * scale)
                );

                spriteBatch.Draw(_texture, destination, SourceRectangle, Color.White);
            }
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public virtual Vector2 GetDimensions()
        {
            return new Vector2(SourceRectangle.Width * scale, SourceRectangle.Height * scale);
        }

        public bool IsSolid()
        {
            return isSolid;
        }
    }
}