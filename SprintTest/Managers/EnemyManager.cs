using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Enemy;

namespace sprint0Test.Managers
{
    public class EnemyManager
    {
        private static EnemyManager _instance;
        public static EnemyManager Instance => _instance ??= new EnemyManager();

        private List<IEnemy> enemyPool; // Pool of all possible enemies
        private IEnemy activeEnemy; // The current active enemy
        private int activeEnemyIndex = 0;

        public EnemyManager()
        {
            enemyPool = new List<IEnemy>();
            Dictionary<string, Texture2D> Octorok_textures = new Dictionary<string, Texture2D>
            {
                { "Octopus_Idle1", TextureManager.Instance.GetTexture("Octopus_Idle1") },
                { "Octopus_Idle2", TextureManager.Instance.GetTexture("Octopus_Idle2") }
            };

            Dictionary<string, Texture2D> Keese_textures = new Dictionary<string, Texture2D>
            {
                { "Bat_1", TextureManager.Instance.GetTexture("Bat_1") },
                { "Bat_2", TextureManager.Instance.GetTexture("Bat_2") }
            };

            Dictionary<string, Texture2D> Aquamentus_textures = new Dictionary<string, Texture2D>
            {
                { "Dragon_Idle1", TextureManager.Instance.GetTexture("Dragon_Idle1") },
                { "Dragon_Idle2", TextureManager.Instance.GetTexture("Dragon_Idle2") }
            };

            Dictionary<string, Texture2D> Moblin_textures = new Dictionary<string, Texture2D>
            {
                { "Goblin_1", TextureManager.Instance.GetTexture("Goblin_1") },
                { "Goblin_2", TextureManager.Instance.GetTexture("Goblin_2") },
                { "Goblin_3", TextureManager.Instance.GetTexture("Goblin_3") },
                { "Goblin_4", TextureManager.Instance.GetTexture("Goblin_4") }
            };

            Dictionary<string, Texture2D> Darknut_textures = new Dictionary<string, Texture2D>
            {
                { "Darknut_Idle_Down_1", TextureManager.Instance.GetTexture("Darknut_Idle_Down_1") },
                { "Darknut_Idle_Down_2", TextureManager.Instance.GetTexture("Darknut_Idle_Down_2") },
                // { "Darknut_Idle_Up_1", TextureManager.Instance.GetTexture("Darknut_Idle_Up_1") },
                // { "Darknut_Idle_Side_1", TextureManager.Instance.GetTexture("Darknut_Idle_Side_1") },
                // { "Darknut_Idle_Side_2", TextureManager.Instance.GetTexture("Darknut_Idle_Side_2") }
            };

            Dictionary<string, Texture2D> Stalfos_textures = new Dictionary<string, Texture2D>
            {
                { "Skeleton", TextureManager.Instance.GetTexture("Skeleton") }
            };
            enemyPool.Add(new Octorok(new Vector2(200, 200), Octorok_textures));
            // ✅ Prevent out-of-range errors when selecting an active enemy
            if (enemyPool.Count > 0)
            {
                activeEnemy = enemyPool[0]; // Default first enemy
            }
            else
            {
                activeEnemy = null; // Handle the case where no enemies are added
                Console.WriteLine("Warning: Enemy pool is empty!");
            }

        }

        public void SpawnEnemy()
        {
            if (enemyPool.Count == 0)
                return;

            activeEnemy = enemyPool[activeEnemyIndex]; // Set the current enemy
        }

        public void NextEnemy()
        {
            if (enemyPool.Count > 0)
            {
                activeEnemyIndex = (activeEnemyIndex + 1) % enemyPool.Count;
                activeEnemy = enemyPool[activeEnemyIndex]; // Set new active enemy
            }
        }

        public void PreviousEnemy()
        {
            if (enemyPool.Count > 0)
            {
                activeEnemyIndex = (activeEnemyIndex - 1 + enemyPool.Count) % enemyPool.Count;
                activeEnemy = enemyPool[activeEnemyIndex]; // Set new active enemy
            }
        }

        public IEnemy GetActiveEnemy()
        {
            return activeEnemy;
        }

        public void Update(GameTime gameTime)
        {
            activeEnemy?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            activeEnemy?.Draw(spriteBatch);


        }
        public IEnemy CreateOctorok(Vector2 position)
        {
            var textures = new Dictionary<string, Texture2D>
    {
        { "Octopus_Idle1", TextureManager.Instance.GetTexture("Octopus_Idle1") },
        { "Octopus_Idle2", TextureManager.Instance.GetTexture("Octopus_Idle2") }
    };
            return new Octorok(position, textures);
        }

        public IEnemy CreateKeese(Vector2 position)
        {
            var textures = new Dictionary<string, Texture2D>
    {
        { "Bat_1", TextureManager.Instance.GetTexture("Bat_1") },
        { "Bat_2", TextureManager.Instance.GetTexture("Bat_2") }
    };
            return new Keese(position, textures);
        }

        public IEnemy CreateAquamentus(Vector2 position)
        {
            var textures = new Texture2D[]
            {
        TextureManager.Instance.GetTexture("Dragon_Idle1"),
        TextureManager.Instance.GetTexture("Dragon_Idle2")
            };
            return new Aquamentus(position, 50.0f); // Note: Aquamentus handles texture loading internally
        }

        public IEnemy CreateMoblin(Vector2 position)
        {
            var textures = new Dictionary<string, Texture2D>
    {
        { "Goblin_1", TextureManager.Instance.GetTexture("Goblin_1") },
        { "Goblin_2", TextureManager.Instance.GetTexture("Goblin_2") },
        { "Goblin_3", TextureManager.Instance.GetTexture("Goblin_3") },
        { "Goblin_4", TextureManager.Instance.GetTexture("Goblin_4") }
    };
            return new Moblin(position, textures);
        }

        public IEnemy CreateDarknut(Vector2 position)
        {
            var textures = new Dictionary<string, Texture2D>
    {
        { "Darknut_Idle_Down_1", TextureManager.Instance.GetTexture("Darknut_Idle_Down_1") },
        { "Darknut_Idle_Down_2", TextureManager.Instance.GetTexture("Darknut_Idle_Down_2") }
    };
            return new Darknut(position, textures);
        }

        public IEnemy CreateStalfos(Vector2 position)
        {
            var textures = new Dictionary<string, Texture2D>
    {
        { "Skeleton", TextureManager.Instance.GetTexture("Skeleton") }
    };
            return new Stalfos(position, textures);
        }

    }
}
