using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Projectiles;

namespace sprint0Test.Managers
{
    public class ProjectileManager
    {
        private static ProjectileManager _instance;
        public static ProjectileManager Instance => _instance ??= new ProjectileManager();

        private List<IProjectile> activeProjectiles;  // Projectiles currently in use
        private Queue<IProjectile> projectilePool;  // Pool of inactive projectiles
        private const int MAX_PROJECTILES = 100;  // Prevent excessive projectiles

        private ProjectileManager()
        {
            activeProjectiles = new List<IProjectile>();
            projectilePool = new Queue<IProjectile>();  // Initialize pool
        }

        // ✅ Use a pooling system instead of creating new projectiles every time
        public void SpawnProjectile(Vector2 position, Vector2 direction, string projectileType)
        {
            if (activeProjectiles.Count >= MAX_PROJECTILES)
            {
                Console.WriteLine("Max projectile limit reached. Cannot spawn more projectiles.");
                return;
            }

            IProjectile projectile = null;

            // ✅ Reuse a projectile from the pool if available
            if (projectilePool.Count > 0)
            {
                projectile = projectilePool.Dequeue();
                Console.WriteLine($"Reusing projectile from pool: {projectileType}");
            }
            else
            {
                // ✅ Create a new projectile if the pool is empty
                Texture2D fireball = TextureManager.Instance.GetTexture("Fireball");  // Dragon texture
                Texture2D octupus = TextureManager.Instance.GetTexture("Octupus_Projectile");  // Side Texture
                switch (projectileType)
                {
                    case "Fireball":
                        projectile = new Fireball(position, direction, fireball);
                        break;
                    //case "Octupus_Projectile":
                    //    projectile = new Octupus_Projectile(position, direction, octupus);
                    //    break;
                    //case "MagicBeam":
                    //    projectile = new MagicBeam(position, direction, texture);
                    //    break;
                    default:
                        Console.WriteLine($"Unknown projectile type: {projectileType}");
                        return;
                }
            }

            // ✅ Reset projectile properties before reusing
            (projectile as AbstractProjectile)?.Reset(position, direction);

            activeProjectiles.Add(projectile);
            Console.WriteLine($"Spawned {projectileType} at {position}, moving {direction}. Total: {activeProjectiles.Count}");
        }

        public void Update(GameTime gameTime)
        {
            for (int i = activeProjectiles.Count - 1; i >= 0; i--)
            {
                activeProjectiles[i].Update(gameTime);

                // ✅ If projectile is inactive, move it back to the pool
                if (!activeProjectiles[i].IsActive())
                {
                    projectilePool.Enqueue(activeProjectiles[i]);
                    activeProjectiles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectile in activeProjectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
    }
}
