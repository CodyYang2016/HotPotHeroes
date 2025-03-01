using sprint0Test.Projectiles;
using sprint0Test.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test.Projectiles
{
    public class Fireball : AbstractProjectile, IProjectile
    {
        public Fireball(Vector2 startPosition, Vector2 direction, Texture2D Fireball)

            : base(startPosition, direction, TextureManager.Instance.GetTexture("Fireball"), 300f)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Additional logic if needed (e.g., fireball explosion on impact)
        }
    }

}
