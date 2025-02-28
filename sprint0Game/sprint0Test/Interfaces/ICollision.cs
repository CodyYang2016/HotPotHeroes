using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using sprint0Test.Link1;


namespace sprint0Test
{
    public interface ICollision
    {
        void HandleCollision(Link player, Block block);
    
    }
}