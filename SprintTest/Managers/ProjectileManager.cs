using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // Use a pooling system instead of creating new projectiles every time
        public void SpawnProjectile(Vector2 position, Vector2 direction, string projectileType)
        {
            Debug.WriteLine($"SpawnProjectile() called with type: {projectileType}");

            if (activeProjectiles.Count >= MAX_PROJECTILES)
            {
                Debug.WriteLine("Max projectile limit reached. Cannot spawn more projectiles.");
                return;
            }

            IProjectile projectile = null;

/*            if (projectilePool.Count > 0)
            {
                projectile = projectilePool.Dequeue();
                Debug.WriteLine($"Reusing projectile from pool: {projectileType}");
            }
            else
            {*/
                Texture2D fireball = TextureManager.Instance.GetTexture("Fireball");
                Texture2D rockTexture = TextureManager.Instance.GetTexture("Rock");

                if (fireball == null)
                {
                    Debug.WriteLine("Fireball texture is NULL!");
                    return;
                }

                switch (projectileType)
                {
                    case "Fireball":
                        projectile = new Fireball(position, direction, fireball);
                        Debug.WriteLine($"Created new Fireball at {position}");
                        break;
                    case "Rock":
                        projectile = new Rock(position, direction, rockTexture);
                        Debug.WriteLine($"Created new Rock at {position}");
                        break;
                    case "Sword":
                        projectile = new Sword(position, fireball);
                        Debug.WriteLine($"Created new Sword at {position}");
                        break;
                    default:
                        Debug.WriteLine($"Unknown projectile type: {projectileType}");
                        return;
                }
            // }

            (projectile as AbstractProjectile)?.Reset(position, direction);
            (projectile as AbstractMelee)?.Reset(position, direction);

            activeProjectiles.Add(projectile);
            Debug.WriteLine($"Spawned {projectileType} at {position}, moving {direction}. Active projectiles: {activeProjectiles.Count}");
        }

        public void Update(GameTime gameTime)
        {
            Debug.WriteLine($"Updating projectiles. Active count: {activeProjectiles.Count}");

            for (int i = activeProjectiles.Count - 1; i >= 0; i--)
            {
                activeProjectiles[i].Update(gameTime);
                Debug.WriteLine($"Projectile {i} position: {activeProjectiles[i].Position}");

                if (!activeProjectiles[i].IsActive())
                {
                    Debug.WriteLine($"Projectile {i} became inactive and was removed.");
                    projectilePool.Enqueue(activeProjectiles[i]);
                    activeProjectiles.RemoveAt(i);
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Debug.WriteLine($"Drawing {activeProjectiles.Count} projectiles.");

            foreach (var projectile in activeProjectiles)
            {
                Debug.WriteLine($"Drawing projectile at {projectile.Position}");
                projectile.Draw(spriteBatch);
            }
        }

        public List<IProjectile> GetActiveProjectiles()
        {
            return activeProjectiles;
        }

    }
}
