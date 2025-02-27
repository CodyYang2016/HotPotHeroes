using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using BlockBranch.sprint0Game.sprint0Test.Projectiles;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
using System;



namespace HotpotHeroes.sprint0Game.sprint0Test.Managers
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

        public void SpawnProjectile(Vector2 position, Vector2 direction)
        {
            if (projectiles.Count >= MAX_PROJECTILES)
            {
                Console.WriteLine("Warning: Max projectile limit reached. Cannot spawn more projectiles.");
                return;
            }

            // Create a Fireball projectile
            projectiles.Add(new Fireball(position, direction));
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
