using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using HotpotHeroes.sprint0Game.sprint0Test.Projectiles;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;

namespace HotpotHeroes.sprint0Game.sprint0Test.Managers
{
    public class ProjectileManager
    {
        private static ProjectileManager _instance;
        public static ProjectileManager Instance => _instance ??= new ProjectileManager();

        private List<Projectile> projectiles;

        private ProjectileManager()
        {
            projectiles = new List<Projectile>();
        }

        public void SpawnProjectile(Vector2 position, Vector2 direction, Texture2D texture)
        {
            projectiles.Add(new Projectile(position, direction, texture));
        }

        public void Update(GameTime gameTime)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(gameTime);

                // Remove inactive projectiles
                if (!projectiles[i].IsActive())
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
    }
}
