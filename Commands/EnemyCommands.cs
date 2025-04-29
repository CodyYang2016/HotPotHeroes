using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Managers;
using sprint0Test.Enemy;
namespace sprint0Test
{
    public class EnemyCommands
    {
        public static void NextEnemy()
        {
            EnemyManager.Instance.NextEnemy();
        }

        public static void PreviousEnemy()
        {
            EnemyManager.Instance.PreviousEnemy();
        }

        public static void MoveEnemyLeft()
        {
            IEnemy enemy = EnemyManager.Instance.GetActiveEnemy();
            enemy?.SetPosition(enemy.GetPosition() + new Vector2(-5, 0));
        }

        public static void MoveEnemyRight()
        {
            IEnemy enemy = EnemyManager.Instance.GetActiveEnemy();
            enemy?.SetPosition(enemy.GetPosition() + new Vector2(5, 0));
        }

        public static void EnemyAttack()
        {
            IEnemy enemy = EnemyManager.Instance.GetActiveEnemy();
            enemy?.PerformAttack();
        }

        public static void EnemyTakeDamage()
        {
            IEnemy enemy = EnemyManager.Instance.GetActiveEnemy();
            enemy?.TakeDamage(1);
        }
    }
}
