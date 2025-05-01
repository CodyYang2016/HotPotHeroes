using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Link1;

namespace sprint0Test.Commands
{
    public class CycleWeaponCommand : ICommand
    {
        public void Execute()
        {
            Link.Instance.CycleWeapon();
        }
    }

}
