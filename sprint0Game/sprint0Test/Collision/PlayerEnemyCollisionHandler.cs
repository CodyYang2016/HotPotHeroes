using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;

namespace sprint0Test;

    public class PlayerEnemyCollisionHandler
    {        
        public void HandleCollisionList(Link player, List<IEnemy> _active)
        {
            foreach (var block in _active)
            {
                HandleCollision(player, block);
            }
        }

        public void HandleCollision(Link player, IEnemy block)
        {
            if (CollisionDetect2.isTouchingLeft(player, block))
            {
                
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                player.MoveLeft();
                player.TakeDamage();
            }

            if (CollisionDetect2.isTouchingRight(player, block))
            {
                
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                player.MoveRight();
                player.TakeDamage();
            }

            if (CollisionDetect2.isTouchingBottom(player, block))
            {
                //player.sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                player.MoveDown();
                player.TakeDamage();
            }

            if (CollisionDetect2.isTouchingTop(player, block))
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

