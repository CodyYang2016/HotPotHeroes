
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
            if (blocks == null || projectiles == null) return;

            foreach (var projectile in projectiles)
            {
                if (projectile == null) continue;

                foreach (var block in blocks)
                {
                    HandleCollision(block, projectile);
                }
            }
        }


        public void HandleCollision(IBlock block, IProjectile projectile)
        {
            if (block != null)
            {
                if (CollisionDetectEntities.isTouchingLeft(block, projectile))
                {
                    projectile.Deactivate();
                }

                if (CollisionDetectEntities.isTouchingRight(block, projectile))
                {
                    projectile.Deactivate();
                }

                if (CollisionDetectEntities.isTouchingBottom(block, projectile))
                {
                    projectile.Deactivate();
                }

                if (CollisionDetectEntities.isTouchingTop(block, projectile))
                {
                    projectile.Deactivate();
                }
            }
        }

    }
}
