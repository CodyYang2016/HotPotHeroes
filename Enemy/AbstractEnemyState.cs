using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Enemy
{
    public abstract class AbstractEnemyState : IEnemyState
    {
        protected AbstractEnemy enemy;

        public AbstractEnemyState(AbstractEnemy enemy)
        {
            this.enemy = enemy;
        }

        public abstract void Update(GameTime gameTime);
    }
}