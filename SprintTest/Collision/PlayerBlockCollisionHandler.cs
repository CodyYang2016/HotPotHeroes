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
            if (CollisionDetect.isTouchingLeft(Link.Instance, block))
            {
                Link.Instance.MoveLeft();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetect.isTouchingRight(Link.Instance, block))
            {
                Link.Instance.MoveRight();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetect.isTouchingBottom(Link.Instance, block))
            {
                Link.Instance.MoveDown();
                Link.Instance.TakeDamage();
            }

            if (CollisionDetect.isTouchingTop(Link.Instance, block))
            {
                Link.Instance.MoveUp();
                Link.Instance.TakeDamage();
            }
        }

        public void HandleCollision()
        {
            if (Link.Instance.Position.Y > 250)
            {
                Link.Instance.MoveUp();
                Link.Instance.TakeDamage();
            }
        }
    }
}
