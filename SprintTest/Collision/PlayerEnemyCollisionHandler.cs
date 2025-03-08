using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;

namespace sprint0Test
{
    public class PlayerEnemyCollisionHandler
    {
        public void HandleCollisionList(List<IEnemy> _active)
        {
            foreach (var block in _active)
            {
                HandleCollision(block);
            }
        }

        public void HandleCollision(IEnemy block)
        {
            if (CollisionDetect2.isTouchingLeft(Link.Instance, block))
            {
                Link.Instance.MoveLeft();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetect2.isTouchingRight(Link.Instance, block))
            {
                Link.Instance.MoveRight();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetect2.isTouchingBottom(Link.Instance, block))
            {
                Link.Instance.MoveDown();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetect2.isTouchingTop(Link.Instance, block))
            {
                Link.Instance.MoveUp();
                Link.Instance.TakeDamage();
            }
        }

    }
}
