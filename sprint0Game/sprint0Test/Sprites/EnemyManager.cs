using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0Test.Sprites
{
    public class EnemyManager
    {
        private List<Enemy> enemies;
        private int currentEnemyIndex;

        public EnemyManager(List<Texture2D> enemyTextures)
        {
            enemies = new List<Enemy>();
            for (int i = 0; i < enemyTextures.Count; i++)
            {
                enemies.Add(new Enemy(enemyTextures[i], new Vector2(100 + (i * 100), 200), 3));
            }
            currentEnemyIndex = 0;
        }

        public void NextEnemy()
        {
            currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
        }

        public void PreviousEnemy()
        {
            currentEnemyIndex = (currentEnemyIndex - 1 + enemies.Count) % enemies.Count;
        }

        public void Update(GameTime gameTime)
        {
            if (enemies.Count > 0)
            {
                enemies[currentEnemyIndex].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enemies.Count > 0)
            {
                enemies[currentEnemyIndex].Draw(spriteBatch);
            }
        }
    }
}
