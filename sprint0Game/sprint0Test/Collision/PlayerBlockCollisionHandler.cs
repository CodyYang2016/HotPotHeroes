using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test;

    public class PlayerBlockCollisionHandler
    {        
        public void HandleCollisionList(Link player, List<IBlock> _active)
        {
            foreach (var block in _active)
            {
                HandleCollision(player, block);
            }
        }

        public void HandleCollision(Link player, IBlock block)
        {
            if (CollisionDetect.isTouchingLeft(player, block))
            {
                
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                player.MoveLeft();
                player.TakeDamage();
            }

            if (CollisionDetect.isTouchingRight(player, block))
            {
                
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                player.MoveRight();
                player.TakeDamage();
            }

            if (CollisionDetect.isTouchingBottom(player, block))
            {
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                player.MoveDown();
                player.TakeDamage();
            }

            if (CollisionDetect.isTouchingTop(player, block))
            {
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                player.MoveUp();
                player.TakeDamage();
            }
        }

        public void HandleCollision(Link player)
        {
            if (player.Position.Y > 250)
            {
                
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                
                player.MoveUp();
                player.TakeDamage();
            }
        }
    }

