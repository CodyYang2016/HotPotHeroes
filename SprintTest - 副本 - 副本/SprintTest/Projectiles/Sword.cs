using sprint0Test.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Projectiles;

namespace sprint0Test.Projectiles
{
    public class Sword : AbstractMelee, IProjectile
    {
        public Sword(Vector2 startPosition, Texture2D fireballTexture)
            : base(startPosition, fireballTexture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public new Vector2 Position
        {
            get => base.Position;
            set => base.Position = value;
        }
    }
}