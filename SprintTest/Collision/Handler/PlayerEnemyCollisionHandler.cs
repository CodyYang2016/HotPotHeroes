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

        public void HandleCollision(IEnemy enemy)
        {
            if (CollisionDetectEnemy.isTouchingLeft(enemy))
            {
                Link.Instance.MoveLeft();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetectEnemy.isTouchingRight(enemy))
            {
                Link.Instance.MoveRight();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetectEnemy.isTouchingBottom(enemy))
            {
                Link.Instance.MoveDown();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetectEnemy.isTouchingTop(enemy))
            {
                Link.Instance.MoveUp();
                Link.Instance.TakeDamage();
            }
        }

    }
}
