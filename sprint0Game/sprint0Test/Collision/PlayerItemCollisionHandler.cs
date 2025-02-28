using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test;

    public class PlayerItemCollisionHandler
    {        
        public void HandleCollisionList(Link player, List<IItem> _active)
        {
            foreach (var block in _active)
            {
                HandleCollision(player, block);
            }
        }

        public void HandleCollision(Link player, IItem block)
        {
            if (CollisionDetect3.isTouchingLeft(player, block))
            {
                player.TakeDamage();
            }

            if (CollisionDetect3.isTouchingRight(player, block))
            {
                player.TakeDamage();
            }

            if (CollisionDetect3.isTouchingBottom(player, block))
            {
                player.TakeDamage();
            }

            if (CollisionDetect3.isTouchingTop(player, block))
            {
                player.TakeDamage();
            }
        }

    }

