
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Projectiles;

namespace sprint0Test.Projectiles
{
    public class Arrow : AbstractProjectile, IProjectile
    {
        public Arrow(Vector2 startPosition, Vector2 direction, Texture2D texture)
            : base(startPosition, direction, texture, speed: 250f, lifetime: 5.0f)
        {
            isFriendly = true;
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (delta <= 0) return;
            position += direction * speed * delta;
            lifetime -= delta;

            if (lifetime <= 0)
            {
                Deactivate();
            }
        }

        public new Vector2 Position
        {
            get => base.Position;
            set => base.Position = value;
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    if (isActive && texture != null)
        //    {
        //        float rotation = (float)System.Math.Atan2(direction.Y, direction.X);
        //        spriteBatch.Draw(texture, position, null, Color.White, rotation,
        //            new Vector2(texture.Width / 2f, texture.Height / 2f), 0.4f, SpriteEffects.None, 0f);
        //    }
        //}
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive && texture != null)
            {
                float rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2;

                Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

                spriteBatch.Draw(
                    texture,
                    position, 
                    null,
                    Color.White,
                    rotation,
                    origin,
                    0.2f,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
