using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;
using System.Diagnostics;
using sprint0Test.Projectiles;

namespace sprint0Test;
public class CollisionDetectProjEnemy
{

    private static Rectangle GetProjRectangle(IProjectile projectile)
    {
        float Width = 8;
        float Height = 8;
        float xPos = projectile.Position.X;
        float yPos = projectile.Position.Y;
        
        return new Rectangle(
            (int)(xPos),  
            (int)(yPos), 
            (int)Width,
            (int)Height
        );
    }

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

    public static bool isTouchingLeft(IEnemy enemy, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetEnemyRectangle(enemy);

        return enemyRect.Right > blockRect.Left &&
            enemyRect.Left < blockRect.Left &&
            enemyRect.Bottom > blockRect.Top &&
            enemyRect.Top < blockRect.Bottom;
    }

    public static bool isTouchingRight(IEnemy enemy, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetEnemyRectangle(enemy);

        // Check for collision on the right side when the player is moving right
        return enemyRect.Left < blockRect.Right &&
            enemyRect.Right > blockRect.Right &&
            enemyRect.Bottom > blockRect.Top &&
            enemyRect.Top < blockRect.Bottom; 
    }

    public static bool isTouchingBottom(IEnemy enemy, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetEnemyRectangle(enemy);

        // Check for collision on the right side when the player is moving right
        return enemyRect.Top < blockRect.Bottom &&
            enemyRect.Bottom > blockRect.Bottom &&
            enemyRect.Left < blockRect.Right &&
            enemyRect.Right > blockRect.Left;
    }

    public static bool isTouchingTop(IEnemy enemy, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetEnemyRectangle(enemy);

        return enemyRect.Bottom > blockRect.Top &&
           enemyRect.Top < blockRect.Top && 
           enemyRect.Left < blockRect.Right &&
           enemyRect.Right > blockRect.Left;
            
    }
}
