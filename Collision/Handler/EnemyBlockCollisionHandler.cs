using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;
using System;

namespace sprint0Test
{
    public class EnemyBlockCollisionHandler
    {
        public void HandleCollisionList(List<IBlock> blocks, List<IEnemy> enemies)//List<IEnemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                foreach (var block in blocks)
                {
                    HandleCollision(block, enemy);
                }
            }
        }

        public void HandleCollision(IBlock block, IEnemy enemy)
        {
            if (enemy != null)
            {
                //Sprint5 Adjustment to collision detection for enemies
                if (CollisionDetectEntities.isTouchingLeft(block, enemy))
                {
                    //Move Enemy Left
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    x = x - 1*1.5f;
                    x = x - 1 * 1.5f;
                    enemy.SetPosition(new Vector2(x, y));
                    
                }

                if (CollisionDetectEntities.isTouchingRight(block, enemy))
                {
                    //Move Enemy Right
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    x = x + 1*1.5f;
                    x = x + 1 * 1.5f;
                    enemy.SetPosition(new Vector2(x, y));
                }

                if (CollisionDetectEntities.isTouchingBottom(block, enemy))
                {
                    //Move Enemy Down
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    y = y + 1*1.5f;
                    y = y + 1 * 1.5f;

                    enemy.SetPosition(new Vector2(x, y));
                }

                if (CollisionDetectEntities.isTouchingTop(block, enemy))
                {
                    //Move Enemy Up
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    y = y - 1*1.5f;
                    y = y - 1 * 1.5f;

                    enemy.SetPosition(new Vector2(x, y));
                }
            }
        }

    }
}
