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
            attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (attackTimer >= attackCooldown)
            {
                enemy.PerformAttack();
                attackTimer = 0f; // 🔁 Reset the timer so it happens again after 3 seconds
            }
        }
    }
}
