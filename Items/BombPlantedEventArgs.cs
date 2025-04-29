using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Items
{
    public class BombPlantedEventArgs : EventArgs
    {
        public Bomb Bomb { get; }

        public BombPlantedEventArgs(Bomb bomb)
        {
            Bomb = bomb;
        }
    }
}
