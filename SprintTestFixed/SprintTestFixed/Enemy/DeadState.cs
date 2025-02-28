using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Enemy
{
    public class DeadState : AbstractEnemyState
    {
        public DeadState(AbstractEnemy enemy) : base(enemy) { }

        public override void Update(GameTime gameTime)
        {
            enemy.Destroy(); // Remove enemy from the game
        }
    }
}
