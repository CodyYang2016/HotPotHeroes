using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Enemy
{
    public class IdleState : AbstractEnemyState
    {
        private float patrolTimer;
        private float patrolDuration = 2f; // 2 seconds before switching direction
        private Vector2 patrolDirection;

        public IdleState(AbstractEnemy enemy) : base(enemy)
        {
            patrolDirection = new Vector2(1, 0); // Move right initially
        }

        public override void Update(GameTime gameTime)
        {
            patrolTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (patrolTimer > patrolDuration)
            {
                patrolTimer = 0f;
                patrolDirection *= -1; // Reverse direction
            }

            enemy.position += patrolDirection * 0.5f; // Move slightly
            if (enemy.DetectPlayer())
            {
                enemy.ChangeState(new ChaseState(enemy));
            }
        }
    }
}
