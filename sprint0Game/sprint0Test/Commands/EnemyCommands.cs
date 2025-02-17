using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
using sprint0Test.Enemy;
namespace sprint0Test.Commands
{
    public class EnemyCommands
    {
        private static int currentEnemyIndex = 0; // Tracks the currently selected enemy

        /// <summary>
        /// Select the next enemy in the list.
        /// </summary>
        public static void NextEnemy()
        {
            if (EnemyManager.Instance.GetEnemyCount() > 0)
            {
                currentEnemyIndex = (currentEnemyIndex + 1) % EnemyManager.Instance.GetEnemyCount();
            }
        }

        /// <summary>
        /// Select the previous enemy in the list.
        /// </summary>
        public static void PreviousEnemy()
        {
            if (EnemyManager.Instance.GetEnemyCount() > 0)
            {
                currentEnemyIndex = (currentEnemyIndex - 1 + EnemyManager.Instance.GetEnemyCount()) % EnemyManager.Instance.GetEnemyCount();
            }
        }

        /// <summary>
        /// Move the selected enemy left.
        /// </summary>
        public static void MoveEnemyLeft()
        {
            IEnemy enemy = EnemyManager.Instance.GetEnemy(currentEnemyIndex);
            if (enemy != null)
            {
                enemy.SetPosition(enemy.GetPosition() + new Vector2(-5, 0)); // Move left
            }
        }

        /// <summary>
        /// Move the selected enemy right.
        /// </summary>
        public static void MoveEnemyRight()
        {
            IEnemy enemy = EnemyManager.Instance.GetEnemy(currentEnemyIndex);
            if (enemy != null)
            {
                enemy.SetPosition(enemy.GetPosition() + new Vector2(5, 0)); // Move right
            }
        }

        /// <summary>
        /// Make the selected enemy attack.
        /// </summary>
        public static void EnemyAttack()
        {
            IEnemy enemy = EnemyManager.Instance.GetEnemy(currentEnemyIndex);
            enemy?.PerformAttack();
        }

        /// <summary>
        /// Make the selected enemy take damage.
        /// </summary>
        public static void EnemyTakeDamage()
        {
            IEnemy enemy = EnemyManager.Instance.GetEnemy(currentEnemyIndex);
            enemy?.TakeDamage(1);
        }
    }
}

