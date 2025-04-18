using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;




namespace sprint0Test
{
    public interface ICollidable
    {
       // object Position { get; set; }


        Vector2 GetPosition();
        Vector2 GetDimensions();
    
    }
}