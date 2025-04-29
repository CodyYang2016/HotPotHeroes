using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;
using System.Diagnostics;

namespace sprint0Test;
public class CollisionDetectEntities
{
    // Check if the B is touching the left side of the A
    public static bool isTouchingLeft(ICollidable blockA, ICollidable blockB)
    {
        Rectangle RectB = DetectionMethods.GetCollidableRectangle(blockB);
        Rectangle RectA = DetectionMethods.GetCollidableRectangle(blockA);

        return RectB.Right > RectA.Left &&
            RectB.Left < RectA.Left &&
            RectB.Bottom > RectA.Top &&
            RectB.Top < RectA.Bottom;
    }

    // Check if the B is touching the right side of the A
    public static bool isTouchingRight(ICollidable blockA, ICollidable blockB)
    {
        Rectangle RectB = DetectionMethods.GetCollidableRectangle(blockB);
        Rectangle RectA = DetectionMethods.GetCollidableRectangle(blockA);

        // Check for collision on the right side when the player is moving right
        return RectB.Left < RectA.Right &&
            RectB.Right > RectA.Right &&
            RectB.Bottom > RectA.Top &&
            RectB.Top < RectA.Bottom;
    }

    // Check if the B is touching the bottom side of the A
    public static bool isTouchingBottom(ICollidable blockA, ICollidable blockB)
    {
        Rectangle RectB = DetectionMethods.GetCollidableRectangle(blockB);
        Rectangle RectA = DetectionMethods.GetCollidableRectangle(blockA);

        // Check for collision on the right side when the player is moving right
        return RectB.Top < RectA.Bottom &&
            RectB.Bottom > RectA.Bottom &&
            RectB.Left < RectA.Right &&
            RectB.Right > RectA.Left;
    }

    // Check if the B is touching the top side of the A
    public static bool isTouchingTop(ICollidable blockA, ICollidable blockB)
    {
        Rectangle RectB = DetectionMethods.GetCollidableRectangle(blockB);
        Rectangle RectA = DetectionMethods.GetCollidableRectangle(blockA);

        return RectB.Bottom > RectA.Top &&
           RectB.Top < RectA.Top &&
           RectB.Left < RectA.Right &&
           RectB.Right > RectA.Left;

    }
}