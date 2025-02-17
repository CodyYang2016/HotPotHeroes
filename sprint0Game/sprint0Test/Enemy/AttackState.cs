using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Enemy
{
    public class AttackState : AbstractEnemyState
    {
        private float attackCooldown = 1f;
        private float attackTimer;

        public AttackState(AbstractEnemy enemy) : base(enemy)
        {
            attackTimer = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (attackTimer >= attackCooldown)
            {
                enemy.PerformAttack(); // This method should handle damage logic
                // enemy.ChangeState(new RetreatState(enemy));
            }
        }
    }
}
