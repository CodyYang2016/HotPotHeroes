using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotHeroes.sprint0Game.sprint0Test.Enemy
{
    public interface IEnemyState
    {
        void Update(GameTime gameTime);
    }
}
