using sprint0Test.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test.Projectiles
{
    public class Rock : AbstractProjectile, IProjectile
    {
        public Rock(Vector2 startPosition, Vector2 direction, Texture2D rockTexture)
            : base(startPosition, direction, rockTexture, 250f) // ✅ Adjust speed if needed
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Additional logic if needed (e.g., collision effects, rock breaking on impact)
        }

        // ✅ Ensure Position is accessible through the interface
        public new Vector2 Position
        {
            get => base.Position;
            set => base.Position = value;
        }
    }
}
