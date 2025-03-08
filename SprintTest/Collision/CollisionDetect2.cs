using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;
using sprint0Test;
using sprint0Test.Enemy;

namespace sprint0Test;
public class CollisionDetect2
{


    public static bool isTouchingLeft(Link player, IEnemy block)
    {

        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;
        float xPos = block.GetPosition().X;
        float yPos = block.GetPosition().Y;

        Vector2 linkSize = Link.Instance.Sprite.GetScaledDimensions(); // Get scaled dimensions
        
        Rectangle playerRect = new Rectangle(
            (int)Link.Instance.Position.X,
            (int)Link.Instance.Position.Y,
            (int)linkSize.X,
            (int)linkSize.Y
        ); 

        Rectangle blockRect = new Rectangle(
            (int)(xPos - (width * scale2 / 2)),  // Adjusted for origin
            (int)(yPos - (height * scale2 / 2)), // Adjusted for origin
            (int)(width * scale2),
            (int)(height * scale2)
        );        

        return  playerRect.Right > blockRect.Left &&
                playerRect.Left < blockRect.Left &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
                
    }

    public static bool isTouchingRight(Link player, IEnemy block)
    {
        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;
        float xPos = block.GetPosition().X;
        float yPos = block.GetPosition().Y;

        Vector2 linkSize = Link.Instance.Sprite.GetScaledDimensions(); // Get scaled dimensions
        Rectangle playerRect = new Rectangle(
            (int)Link.Instance.Position.X,
            (int)Link.Instance.Position.Y,
            (int)linkSize.X,
            (int)linkSize.Y
        );

        // Adjust enemy position for collision
        Rectangle blockRect = new Rectangle(
            (int)(xPos - (width * scale2 / 2)),  // Adjusted for origin
            (int)(yPos - (height * scale2 / 2)), // Adjusted for origin
            (int)(width * scale2),
            (int)(height * scale2)
        );

        // Check for collision on the right side when the player is moving right
        return playerRect.Left < blockRect.Right &&
            playerRect.Right > blockRect.Right &&
            playerRect.Bottom > blockRect.Top &&
            playerRect.Top < blockRect.Bottom; 
            //player.Speed.X > 0; // Assuming player is moving right (positive speed)
    }

    public static bool isTouchingBottom(Link player, IEnemy block)
    {
        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;
        float xPos = block.GetPosition().X;
        float yPos = block.GetPosition().Y;

        Vector2 linkSize = Link.Instance.Sprite.GetScaledDimensions(); // Get scaled dimensions
        Rectangle playerRect = new Rectangle(
            (int)Link.Instance.Position.X,
            (int)Link.Instance.Position.Y,
            (int)linkSize.X,
            (int)linkSize.Y
        );
    
        Rectangle blockRect = new Rectangle(
            (int)(xPos - (width * scale2 / 2)),  // Adjusted for origin
            (int)(yPos - (height * scale2 / 2)), // Adjusted for origin
            (int)(width * scale2),
            (int)(height * scale2)
        );

        // Check for collision on the right side when the player is moving right
        return playerRect.Top < blockRect.Bottom &&
            playerRect.Bottom > blockRect.Bottom &&
            playerRect.Left < blockRect.Right &&
            playerRect.Right > blockRect.Left;
           //player.Speed.Y > 0; // Assuming player is moving down (positive speed)
    }

    public static bool isTouchingTop(Link player, IEnemy block)
    {
        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;
        float xPos = block.GetPosition().X;
        float yPos = block.GetPosition().Y;

        Vector2 linkSize = Link.Instance.Sprite.GetScaledDimensions(); // Get scaled dimensions
        Rectangle playerRect = new Rectangle(
            (int)Link.Instance.Position.X,
            (int)Link.Instance.Position.Y,
            (int)linkSize.X,
            (int)linkSize.Y
        );

        Rectangle blockRect = new Rectangle(
            (int)(xPos - (width * scale2 / 2)),  // Adjusted for origin
            (int)(yPos - (height * scale2 / 2)), // Adjusted for origin
            (int)(width * scale2),
            (int)(height * scale2)
        );

        return playerRect.Bottom > blockRect.Top &&
           playerRect.Top < blockRect.Top && 
           playerRect.Left < blockRect.Right &&
           playerRect.Right > blockRect.Left;
            //player.Speed.X > 0; // Assuming player is moving right (positive speed)
            
    }




}
