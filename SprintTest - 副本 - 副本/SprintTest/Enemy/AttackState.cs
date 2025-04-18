using Microsoft.Xna.Framework;

namespace sprint0Test.Enemy
{
    public class AttackState : AbstractEnemyState
    {
        private float attackCooldown = 3.0f; // 🔁 Attack every 3 seconds
        private float attackTimer;

        public AttackState(AbstractEnemy enemy) : base(enemy)
        {
            attackTimer = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            // 🔁 First check if the player is no longer in detection range
            if (!enemy.DetectPlayer())
            {
                enemy.ChangeState(new IdleState(enemy));
                return;
            }

            if (enemy.Health <= 0)
            {
                enemy.ChangeState(new DeadState(enemy));
                return;
            }


            // ✅ Still sees the player, perform attack logic
            attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (attackTimer >= attackCooldown)
            {
                if (enemy.IsInAttackRange())
                {
                    enemy.PerformAttack();
                }

                attackTimer = 0f; // 🔁 Reset the timer
            }
        }
    }
}
