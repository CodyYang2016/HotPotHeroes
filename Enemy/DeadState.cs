
using Microsoft.Xna.Framework;

namespace sprint0Test.Enemy
{
    public class DeadState : AbstractEnemyState
    {
        private bool hasTriggeredDeath = false;

        public DeadState(AbstractEnemy enemy) : base(enemy) { }

        public override void Update(GameTime gameTime)
        {
            if (!hasTriggeredDeath)
            {
                enemy.Destroy(); // Optional: play sound, animation, drop loot
                hasTriggeredDeath = true;
            }

            // Dead enemies don't move, attack, or animate further
            // You could also skip UpdateAnimation() if needed
        }
    }
}
