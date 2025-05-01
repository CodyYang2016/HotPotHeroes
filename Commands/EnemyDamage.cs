using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Audio;
using sprint0Test.Interfaces;


namespace sprint0Test
{
    class EnemyTakeDamageCommand : ICommand
    {
        public void Execute()
        {
            SoundManager.Instance.PlaySound(SoundList.scream);
            EnemyCommands.EnemyTakeDamage();
        }
    }

}
