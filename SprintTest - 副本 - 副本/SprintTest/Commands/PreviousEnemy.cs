using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Interfaces;


namespace sprint0Test
{
    public class PreviousEnemyCommand : ICommand
    {
        public void Execute()
        {
            EnemyCommands.PreviousEnemy();
        }
    }

}
