using System;
using Microsoft.Xna.Framework;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;

namespace sprint0Test
{
    public class CollisionDetectEnemy
    {
        // Helper method to create the player's collision rectangle
        private static Rectangle GetPlayerRectangle()
        {
            Vector2 linkSize = Link.Instance.GetScaledDimensions();
            return new Rectangle(
                (int)Link.Instance.Position.X,
                (int)Link.Instance.Position.Y,
                (int)linkSize.X,
                (int)linkSize.Y
            );
        }

        // Helper method to create the enemy's collision rectangle
        private static Rectangle GetEnemyRectangle(IEnemy enemy)
        {
            float enemyWidth = enemy.GetDimensions().X;
            float enemyHeight = enemy.GetDimensions().Y;
            float xPos = enemy.GetPosition().X;
            float yPos = enemy.GetPosition().Y;
            
            return new Rectangle(
                (int)(xPos - (enemyWidth / 2)),  // Adjusted for origin
                (int)(yPos - (enemyHeight / 2)), // Adjusted for origin
                (int)enemyWidth,
                (int)enemyHeight
            );
        }

        // Check if the player is touching the left side of the enemy
        public static bool isTouchingLeft(IEnemy enemy)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle enemyRect = GetEnemyRectangle(enemy);

            return playerRect.Right > enemyRect.Left &&
                playerRect.Left < enemyRect.Left &&
                playerRect.Bottom > enemyRect.Top &&
                playerRect.Top < enemyRect.Bottom;
        }

        // Check if the player is touching the right side of the enemy
        public static bool isTouchingRight(IEnemy enemy)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle enemyRect = GetEnemyRectangle(enemy);

            return playerRect.Left < enemyRect.Right &&
                playerRect.Right > enemyRect.Right &&
                playerRect.Bottom > enemyRect.Top &&
                playerRect.Top < enemyRect.Bottom;
        }

        // Check if the player is touching the bottom side of the enemy
        public static bool isTouchingBottom(IEnemy enemy)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle enemyRect = GetEnemyRectangle(enemy);

            return playerRect.Top < enemyRect.Bottom &&
                playerRect.Bottom > enemyRect.Bottom &&
                playerRect.Left < enemyRect.Right &&
                playerRect.Right > enemyRect.Left;
        }

        // Check if the player is touching the top side of the enemy
        public static bool isTouchingTop(IEnemy enemy)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle enemyRect = GetEnemyRectangle(enemy);

            return playerRect.Bottom > enemyRect.Top &&
                playerRect.Top < enemyRect.Top &&
                playerRect.Left < enemyRect.Right &&
                playerRect.Right > enemyRect.Left;
        }
    }
}
