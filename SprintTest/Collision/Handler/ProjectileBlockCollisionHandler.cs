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
    public class ProjectileBlockCollisionHandler
    {
        public void HandleCollisionList(List<IBlock> blocks, List<IProjectile> projectiles)
        {
            foreach (var projectile in projectiles)
            {
                foreach (var block in blocks)
                {
                    HandleCollision(block, projectile);
                }
            }
        }

        public void HandleCollision(IBlock block, IProjectile projectile)
        {
            if (CollisionDetectProjBlock.isTouchingLeft(block, projectile))
            {
                projectile.Deactivate();
            }

            if (CollisionDetectProjBlock.isTouchingRight(block, projectile))
            {
                projectile.Deactivate();
            }

            if (CollisionDetectProjBlock.isTouchingBottom(block, projectile))
            {
                projectile.Deactivate();
            }

            if (CollisionDetectProjBlock.isTouchingTop(block, projectile))
            {
                projectile.Deactivate();
            }
        }

    }
}
