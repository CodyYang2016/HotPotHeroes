using sprint0Test.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Interfaces;


namespace HotpotHeroes.sprint0Game.sprint0Test.Commands
{
    public class PreviousEnemyCommand : ICommand
    {
        public void Execute()
        {
            EnemyCommands.PreviousEnemy();
        }
    }

}
