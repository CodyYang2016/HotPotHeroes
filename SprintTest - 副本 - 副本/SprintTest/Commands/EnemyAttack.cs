using sprint0Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sprint0Test
{
    class EnemyAttackCommand : ICommand
    {
        public void Execute()
        {
            EnemyCommands.EnemyAttack();
        }
    }

}
