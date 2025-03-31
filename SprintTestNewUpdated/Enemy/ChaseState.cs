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
        private float speed = 1.2f;

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
