using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Enemy
{
    public class ChaseState : AbstractEnemyState
    {
        //Sprint5 Movement Speed Adjustment
        private float speed = 0.6f;

        public ChaseState(AbstractEnemy enemy) : base(enemy) { }

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = enemy.GetDirectionToPlayer();
            enemy.position += direction * speed;

            if (enemy.IsInAttackRange())
            {
                enemy.ChangeState(new AttackState(enemy));
            }
            else if (!enemy.DetectPlayer())
            {
                enemy.ChangeState(new IdleState(enemy));
            }
        }
    }

}
