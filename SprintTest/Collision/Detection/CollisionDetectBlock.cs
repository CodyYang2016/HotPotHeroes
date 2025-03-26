using System;
using Microsoft.Xna.Framework;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test
{
    public class CollisionDetectBlock
    {
        // Helper method to create the player's collision rectangle
        private static Rectangle GetPlayerRectangle()
        {
            Vector2 linkSize = Link.Instance.GetScaledDimensions();
            return new Rectangle(
                (int)Link.Instance.Position.X,
                (int)Link.Instance.Position.Y,
                (int)linkSize.X,
                (int)linkSize.Y
            );
        }

        // Helper method to create the block's collision rectangle
        private static Rectangle GetBlockRectangle(ICollidable block)
        {
            int width = 16;
            int height = 16;
            float scale = 3f;

            return new Rectangle(
                (int)block.Position.X,
                (int)block.Position.Y,
                (int)(width * scale),
                (int)(height * scale)
            );
        }

        // Check if the player is touching the left side of the block
        public static bool isTouchingLeft(ICollidable block)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetBlockRectangle(block);

            return playerRect.Right > blockRect.Left &&
                playerRect.Left < blockRect.Left &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
        }

        // Check if the player is touching the right side of the block
        public static bool isTouchingRight(ICollidable block)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetBlockRectangle(block);

            return playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Right &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
        }

        // Check if the player is touching the bottom side of the block
        public static bool isTouchingBottom(ICollidable block)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetBlockRectangle(block);

            return playerRect.Top < blockRect.Bottom &&
                playerRect.Bottom > blockRect.Bottom &&
                playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Left;
        }

        // Check if the player is touching the top side of the block
        public static bool isTouchingTop(ICollidable block)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetBlockRectangle(block);

            return playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Top &&
                playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Left;
        }
    }
}
