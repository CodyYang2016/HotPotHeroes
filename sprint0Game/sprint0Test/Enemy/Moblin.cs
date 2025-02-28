using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Managers;
using System.Collections.Generic;
using sprint0Test.Link1;


namespace sprint0Test.Enemy
{
    public class Moblin : AbstractEnemy
    {
        public Moblin(Vector2 startPosition, Dictionary<string, Texture2D> Moblin_textures)
            : base(startPosition, new Texture2D[]
            {
                Moblin_textures["Goblin_1"],
                Moblin_textures["Goblin_2"],
                // Moblin_textures["Goblin_3"],
                // Moblin_textures["Goblin_4"]
            })
        {
            detectionRadius = 100f; // Short detection range
            attackRange = 25f; // Needs to be close to attack
            health = 5; // Medium health
        }

        public override void PerformAttack()
        {
            // Moblin does a melee attack
            if (IsInAttackRange())
            {
                Link.Instance.TakeDamage();
            }
        }
    }
}
