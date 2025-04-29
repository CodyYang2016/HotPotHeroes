using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using sprint0Test;
using sprint0Test.Projectiles;
using sprint0Test.Enemy;
using sprint0Test.Link1;

namespace sprint0Test
{
    public static class DetectionMethods
    {
        public static Rectangle GetCollidableRectangle(ICollidable collidable)
        {
            // Get common properties
            float width = collidable.GetDimensions().X;
            float height = collidable.GetDimensions().Y;
            float xPos = collidable.GetPosition().X;
            float yPos = collidable.GetPosition().Y;

            // Check if the collidable is an enemy (assuming IEnemy is the interface or base class)
            if (collidable is IEnemy)
            {
                // Adjust for enemy-specific origin (centered around the position)
                return new Rectangle(
                    (int)(xPos - (width / 2)),  // Adjusted for origin
                    (int)(yPos - (height / 2)), // Adjusted for origin
                    (int)width,
                    (int)height
                );
            }
            else
            {
                // Standard block rectangle (no origin adjustment)
                return new Rectangle(
                    (int)xPos,
                    (int)yPos,
                    (int)width,
                    (int)height
                );
            }
        }

        // Helper method to create the player's collision rectangle
        public static Rectangle GetPlayerRectangle()
        {
            Vector2 linkSize = Link.Instance.GetScaledDimensions();
            return new Rectangle(
                (int)Link.Instance.Position.X,
                (int)Link.Instance.Position.Y,
                (int)linkSize.X,
                (int)linkSize.Y
            );
        }
    }
}