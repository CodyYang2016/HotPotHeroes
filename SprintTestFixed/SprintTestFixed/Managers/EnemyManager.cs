using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Enemy;

namespace HotpotHeroes.sprint0Game.sprint0Test.Managers
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
            enemyPool = new List<IEnemy>
            {
                new Octorok(new Vector2(100, 100)),
                new Octorok(new Vector2(200, 100)),
                new Aquamentus(new Vector2(300, 100))
                // new Moblin(new Vector2(200, 200)) // Add more enemies as needed
            };

            activeEnemy = enemyPool[0]; // Default first enemy
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
    }
}
