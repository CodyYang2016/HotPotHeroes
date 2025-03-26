using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test
{
    public class PlayerBlockCollisionHandler
    {
        public void HandleCollisionList(List<IBlock> _active)
        {
            foreach (var block in _active)
            {
                HandleCollision(block);
            }
        }

        public void HandleCollision(IBlock block)
        {
            if (CollisionDetectBlock.isTouchingLeft(block))
            {
                Link.Instance.MoveLeft();
                //Link.Instance.TakeDamage();
            }

            if (CollisionDetectBlock.isTouchingRight(block))
            {
                Link.Instance.MoveRight();
                //Link.Instance.TakeDamage();
            }

            if (CollisionDetectBlock.isTouchingBottom(block))
            {
                Link.Instance.MoveDown();
                //Link.Instance.TakeDamage();
            }

            if (CollisionDetectBlock.isTouchingTop(block))
            {
                Link.Instance.MoveUp();
                //Link.Instance.TakeDamage();
            }
        }

    }
}
