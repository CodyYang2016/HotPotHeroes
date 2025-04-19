using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Enemy
{
    public class RetreatState : AbstractEnemyState
    {
        private float retreatDuration = 1f;
        private float retreatTimer;
        private Vector2 retreatDirection;

        public RetreatState(AbstractEnemy enemy) : base(enemy)
        {
            retreatDirection = -enemy.GetDirectionToPlayer(); // Move opposite to the player
        }

        public override void Update(GameTime gameTime)
        {
            retreatTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (retreatTimer < retreatDuration)
            {
                enemy.position += retreatDirection * 0.8f; // Move away
            }
            else
            {
                if (enemy.DetectPlayer())
                {
                    enemy.ChangeState(new ChaseState(enemy));
                }
                else
                {
                    enemy.ChangeState(new IdleState(enemy));
                }
            }
        }
    }
}
