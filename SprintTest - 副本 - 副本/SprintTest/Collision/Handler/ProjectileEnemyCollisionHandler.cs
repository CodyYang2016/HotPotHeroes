using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;
using System;
using sprint0Test.Projectiles;

namespace sprint0Test
{
    public class ProjectileEnemyCollisionHandler
    {
        public void HandleCollisionList(List<IEnemy> enemies, List<IProjectile> projectiles)
        {
            foreach (var projectile in projectiles)
            {
                foreach (var enemy in enemies)
                {
                    HandleCollision(enemy, projectile);
                }
            }
        }

        public void HandleCollision(IEnemy enemy, IProjectile projectile)
        {
            if (projectile.IsFriendly()) {
                if (CollisionDetectEntities.isTouchingLeft(enemy, projectile))
                {
                    projectile.Deactivate();
                    enemy.TakeDamage(5);
                }

                if (CollisionDetectEntities.isTouchingRight(enemy, projectile))
                {
                    projectile.Deactivate();
                    enemy.TakeDamage(5);
                }

                if (CollisionDetectEntities.isTouchingBottom(enemy, projectile))
                {
                    projectile.Deactivate();
                    enemy.TakeDamage(5);
                }

                if (CollisionDetectEntities.isTouchingTop(enemy, projectile))
                {
                    projectile.Deactivate();
                    enemy.TakeDamage(5);
                }
            }
        }

    }
}
