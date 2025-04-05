using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;
using sprint0Test.Projectiles;
using System;

namespace sprint0Test
{
    public class PlayerProjectileCollisionHandler
    {
        public void HandleCollisionList(List<IProjectile> _active)
        {
            if (_active == null) return;

            foreach (var projectile in _active)
            {
                HandleCollision(projectile);
            }
        }


        public void HandleCollision(IProjectile projectile)
        {
            if (projectile != null)
            {
                if (!projectile.IsFriendly()) {
                    if (CollisionDetectEntity.isTouchingLeft(projectile))
                    {
                        Link.Instance.MoveLeft();
                        Link.Instance.TakeDamage();
                        projectile.Deactivate();
                    }

                    if (CollisionDetectEntity.isTouchingRight(projectile))
                    {
                        Link.Instance.MoveRight();
                        Link.Instance.TakeDamage();
                        projectile.Deactivate();
                    }

                    if (CollisionDetectEntity.isTouchingBottom(projectile))
                    {
                        Link.Instance.MoveDown();
                        Link.Instance.TakeDamage();
                        projectile.Deactivate();
                    }

                    if (CollisionDetectEntity.isTouchingTop(projectile))
                    {
                        Link.Instance.MoveUp();
                        Link.Instance.TakeDamage();
                        projectile.Deactivate();
                    }
                }
            }
        }

    }
}
