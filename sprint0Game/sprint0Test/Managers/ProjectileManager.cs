using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Projectiles;
using sprint0Test.Managers;
using System;



namespace sprint0Test.Managers
{
    public class ProjectileManager
    {
        private static ProjectileManager _instance;
        public static ProjectileManager Instance => _instance ??= new ProjectileManager();

        private List<IProjectile> projectiles; // Stores all projectile types
        private const int MAX_PROJECTILES = 100; // Prevent excessive projectiles

        private ProjectileManager()
        {
            projectiles = new List<IProjectile>();
        }

        public void SpawnProjectile(Vector2 position, Vector2 direction, Texture2D projectileTexture)
        {
            if (projectiles.Count >= MAX_PROJECTILES)
            {
                Console.WriteLine("Warning: Max projectile limit reached. Cannot spawn more projectiles.");
                return;
            }

            // Create and add a projectile (e.g., Fireball) with the given texture
            projectiles.Add(new Fireball(position, direction, projectileTexture));
        }


        public void Update(GameTime gameTime)
        {
            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }

            // Remove all inactive projectiles
            projectiles.RemoveAll(p => !p.IsActive());
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
