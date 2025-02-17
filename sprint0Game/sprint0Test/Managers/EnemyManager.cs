using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Enemy;

namespace HotpotHeroes.sprint0Game.sprint0Test.Managers
{
    public class EnemyManager
    {
        private static EnemyManager _instance;
        public static EnemyManager Instance => _instance ??= new EnemyManager();

        private List<IEnemy> enemies;

        public EnemyManager()
        {
            enemies = new List<IEnemy>();
        }

        public void AddEnemy(IEnemy enemy)
        {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            enemies.Remove(enemy);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
        public int GetEnemyCount()
        {
            return enemies.Count;
        }

        public IEnemy GetEnemy(int index)
        {
            if (index >= 0 && index < enemies.Count)
            {
                return enemies[index];
            }
            return null;
        }
    }
}
