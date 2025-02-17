//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using HotpotHeroes.sprint0Game.sprint0Test.Managers;


//namespace HotpotHeroes.sprint0Game.sprint0Test.Enemy
//{
//    public class Moblin : AbstractEnemy
//    {
//        public Moblin(Vector2 startPosition)
//            : base(startPosition, TextureManager.Instance.GetTexture("Goblin"))
//        {
//            detectionRadius = 100f; // Short detection range
//            attackRange = 25f; // Needs to be close to attack
//            health = 5; // Medium health
//        }

//        public override void PerformAttack()
//        {
//            // Moblin does a melee attack
//            if (IsInAttackRange())
//            {
//                Player.Instance.TakeDamage(2);
//            }
//        }
//    }
//}
