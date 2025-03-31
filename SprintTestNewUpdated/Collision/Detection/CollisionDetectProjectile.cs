using System;
using Microsoft.Xna.Framework;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;
using sprint0Test.Projectiles;

namespace sprint0Test
{
    public class CollisionDetectProjectile
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

        // Helper method to create the projectile's collision rectangle
        private static Rectangle GetProjectileRectangle(IProjectile projectile)
        {
            float projWidth = 8;
            float projHeight = 8;
            float xPos = projectile.Position.X;
            float yPos = projectile.Position.Y;
            
            return new Rectangle(
                (int)(xPos), //- (projWidth / 2)),  // Adjusted for origin
                (int)(yPos), //- (projHeight / 2)), // Adjusted for origin
                (int)projWidth,
                (int)projHeight
            );
        }

        // Check if the player is touching the left side of the projectile
        public static bool isTouchingLeft(IProjectile projectile)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle projRect = GetProjectileRectangle(projectile);

            return playerRect.Right > projRect.Left &&
                playerRect.Left < projRect.Left &&
                playerRect.Bottom > projRect.Top &&
                playerRect.Top < projRect.Bottom;
        }

        // Check if the player is touching the right side of the projectile
        public static bool isTouchingRight(IProjectile projectile)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle projRect = GetProjectileRectangle(projectile);

            return playerRect.Left < projRect.Right &&
                playerRect.Right > projRect.Right &&
                playerRect.Bottom > projRect.Top &&
                playerRect.Top < projRect.Bottom;
        }

        // Check if the player is touching the bottom side of the projectile
        public static bool isTouchingBottom(IProjectile projectile)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle projRect = GetProjectileRectangle(projectile);

            return playerRect.Top < projRect.Bottom &&
                playerRect.Bottom > projRect.Bottom &&
                playerRect.Left < projRect.Right &&
                playerRect.Right > projRect.Left;
        }

        // Check if the player is touching the top side of the projectile
        public static bool isTouchingTop(IProjectile projectile)
        {
            Rectangle playerRect = GetPlayerRectangle();
            Rectangle projRect = GetProjectileRectangle(projectile);

            return playerRect.Bottom > projRect.Top &&
                playerRect.Top < projRect.Top &&
                playerRect.Left < projRect.Right &&
                playerRect.Right > projRect.Left;
        }
    }
}
