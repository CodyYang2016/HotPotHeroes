using System;
using Microsoft.Xna.Framework;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test
{
    public class CollisionDetectEntity
    {
        // Check if the player is touching the left side of the block
        public static bool isTouchingLeft(ICollidable block)
        {
            Rectangle playerRect = DetectionMethods.GetPlayerRectangle();
            Rectangle blockRect = DetectionMethods.GetCollidableRectangle(block);

            return playerRect.Right > blockRect.Left &&
                playerRect.Left < blockRect.Left &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
        }

        // Check if the player is touching the right side of the block
        public static bool isTouchingRight(ICollidable block)
        {
            Rectangle playerRect = DetectionMethods.GetPlayerRectangle();
            Rectangle blockRect = DetectionMethods.GetCollidableRectangle(block);

            return playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Right &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
        }

        // Check if the player is touching the bottom side of the block
        public static bool isTouchingBottom(ICollidable block)
        {
            Rectangle playerRect = DetectionMethods.GetPlayerRectangle();
            Rectangle blockRect = DetectionMethods.GetCollidableRectangle(block);

            return playerRect.Top < blockRect.Bottom &&
                playerRect.Bottom > blockRect.Bottom &&
                playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Left;
        }

        // Check if the player is touching the top side of the block
        public static bool isTouchingTop(ICollidable block)
        {
            Rectangle playerRect = DetectionMethods.GetPlayerRectangle();
            Rectangle blockRect = DetectionMethods.GetCollidableRectangle(block);

            return playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Top &&
                playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Left;
        }
    }
}