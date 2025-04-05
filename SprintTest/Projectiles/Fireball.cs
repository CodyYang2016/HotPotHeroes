using sprint0Test.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Projectiles;

namespace sprint0Test.Projectiles
{
    public class Fireball : AbstractProjectile, IProjectile
    {
        public Fireball(Vector2 startPosition, Vector2 direction, Texture2D fireballTexture)
            : base(startPosition, direction, fireballTexture, 300f)  // ✅ Use the passed texture
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Additional logic if needed (e.g., fireball explosion on impact)
        }

        public new Vector2 Position
        {
            get => base.Position;
            set => base.Position = value;
        }
    }
}
