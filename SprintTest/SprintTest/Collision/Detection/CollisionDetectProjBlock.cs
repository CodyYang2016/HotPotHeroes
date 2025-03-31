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
public class CollisionDetectProjBlock
{

    private static Rectangle GetProjRectangle(IProjectile projectile)
    {
        float enemyWidth = 8;
        float enemyHeight = 8;
        float xPos = projectile.Position.X;
        float yPos = projectile.Position.Y;
        
        return new Rectangle(
            (int)(xPos),  
            (int)(yPos), 
            (int)enemyWidth,
            (int)enemyHeight
        );
    }

    private static Rectangle GetBlockRectangle(IBlock block)
    {
        int blockWidth = 16;
        int blockHeight = 16;
        float scale = 3f;

        return new Rectangle(
            (int)block.Position.X,
            (int)block.Position.Y,
            (int)(blockWidth * scale),
            (int)(blockHeight * scale)
        );
    }

    public static bool isTouchingLeft(IBlock block, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetBlockRectangle(block);

        return enemyRect.Right > blockRect.Left &&
            enemyRect.Left < blockRect.Left &&
            enemyRect.Bottom > blockRect.Top &&
            enemyRect.Top < blockRect.Bottom;
    }

    public static bool isTouchingRight(IBlock block, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetBlockRectangle(block);

        // Check for collision on the right side when the player is moving right
        return enemyRect.Left < blockRect.Right &&
            enemyRect.Right > blockRect.Right &&
            enemyRect.Bottom > blockRect.Top &&
            enemyRect.Top < blockRect.Bottom; 
    }

    public static bool isTouchingBottom(IBlock block, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetBlockRectangle(block);

        // Check for collision on the right side when the player is moving right
        return enemyRect.Top < blockRect.Bottom &&
            enemyRect.Bottom > blockRect.Bottom &&
            enemyRect.Left < blockRect.Right &&
            enemyRect.Right > blockRect.Left;
    }

    public static bool isTouchingTop(IBlock block, IProjectile projectile)
    {
        Rectangle enemyRect = GetProjRectangle(projectile);
        Rectangle blockRect = GetBlockRectangle(block);

        return enemyRect.Bottom > blockRect.Top &&
           enemyRect.Top < blockRect.Top && 
           enemyRect.Left < blockRect.Right &&
           enemyRect.Right > blockRect.Left;
            
    }
}
