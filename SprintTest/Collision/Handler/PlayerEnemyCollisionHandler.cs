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
            foreach (var enemy in _active)
            {
                HandleCollision(enemy);
            }
        }

        public void HandleCollision(IEnemy enemy)
        {
            if (enemy != null)
            {

                if (CollisionDetectEntity.isTouchingLeft(enemy))
                {
                    Link.Instance.MoveLeft();
                    Link.Instance.TakeDamage();
                    //Move Enemy Right
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    x = x + 1 * 1.5f;
                    enemy.SetPosition(new Vector2(x, y));
                }

                if (CollisionDetectEntity.isTouchingRight(enemy))
                {
                    Link.Instance.MoveRight();
                    Link.Instance.TakeDamage();
                    //Move Enemy Left
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    x = x - 1 * 1.5f;
                    enemy.SetPosition(new Vector2(x, y));
                }

                if (CollisionDetectEntity.isTouchingBottom(enemy))
                {
                    Link.Instance.MoveDown();
                    Link.Instance.TakeDamage();
                    //Move Enemy Down
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    y = y - 1 * 1.5f;
                    enemy.SetPosition(new Vector2(x, y));
                }

                if (CollisionDetectEntity.isTouchingTop(enemy))
                {
                    Link.Instance.MoveUp();
                    Link.Instance.TakeDamage();
                    //Move Enemy Down
                    float x = enemy.GetPosition().X;
                    float y = enemy.GetPosition().Y;
                    y = y + 1 * 1.5f;
                    enemy.SetPosition(new Vector2(x, y));
                }
            }
        }

    }
}