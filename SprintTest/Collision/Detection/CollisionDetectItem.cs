using System;
using Microsoft.Xna.Framework;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;

namespace sprint0Test
{
    public class CollisionDetectItem
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

        // Helper method to create the item's collision rectangle
        private static Rectangle GetItemRectangle(IItem item)
        {
            int width = 16;
            int height = 16;
            float scale = 3f;

            float xPos = item.Position.X;
            float yPos = item.Position.Y;

            return new Rectangle(
                (int)(xPos),
                (int)(yPos),
                (int)(width * scale),
                (int)(height * scale)
            );
        }

        // Check if the player is touching the left side of the item
        private static bool isTouchingLeft(IItem item)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetItemRectangle(item);

            return playerRect.Right > blockRect.Left &&
                playerRect.Left < blockRect.Left &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
        }

        // Check if the player is touching the right side of the item
        private static bool isTouchingRight(IItem item)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetItemRectangle(item);

            return playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Right &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
        }

        // Check if the player is touching the bottom side of the item
        private static bool isTouchingBottom(IItem item)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetItemRectangle(item);

            return playerRect.Top < blockRect.Bottom &&
                playerRect.Bottom > blockRect.Bottom &&
                playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Left;
        }

        // Check if the player is touching the top side of the item
        private static bool isTouchingTop(IItem item)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle blockRect = GetItemRectangle(item);

            return playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Top &&
                playerRect.Left < blockRect.Right &&
                playerRect.Right > blockRect.Left;
        }

        public static bool isTouching(IItem item) {
            return (isTouchingBottom(item) || isTouchingTop(item) || isTouchingLeft(item) || isTouchingRight(item));
        }
    }
}
