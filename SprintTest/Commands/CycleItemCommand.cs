using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Link1;

namespace sprint0Test.Commands
{
    public class CycleItemCommand : ICommand
    {
        private int direction; // 1 for forward, -1 for backward

        public CycleItemCommand(int direction)
        {
            this.direction = direction;
        }

        public void Execute()
        {
            Link.Instance.CycleItem(direction);
        }
    }

}
