using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace sprint0Test.Projectiles
{
    public interface IProjectile : ICollidable
    {
        Vector2 Position { get; set; } // ✅ Add this line
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Deactivate();
        bool IsActive();

        bool IsFriendly();
    }
}
