using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test;
public class CollisionDetect
{

    public static bool isTouchingLeft(Link player, IBlock block)
    {

        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;
        Rectangle playerRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, (int)(width * scale), (int)(height * scale));
        Rectangle blockRect = new Rectangle((int)block.Position.X, (int)block.Position.Y, (int)(width * scale2), (int)(height * scale2));
        

        // return  playerRect.Right + player.Speed > blockRect.Left &&
        return  playerRect.Right > blockRect.Left &&
                playerRect.Left < blockRect.Left &&
                playerRect.Bottom > blockRect.Top &&
                playerRect.Top < blockRect.Bottom;
                
    }

    public static bool isTouchingRight(Link player, IBlock block)
    {
        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;

        Rectangle playerRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, (int)(width * scale), (int)(height * scale));
        
        Rectangle blockRect = new Rectangle((int)block.Position.X, (int)block.Position.Y, (int)(width * scale2), (int)(height * scale2));

        // Check for collision on the right side when the player is moving right
        return playerRect.Left < blockRect.Right &&
            playerRect.Right > blockRect.Right &&
            playerRect.Bottom > blockRect.Top &&
            playerRect.Top < blockRect.Bottom; 
            //player.Speed.X > 0; // Assuming player is moving right (positive speed)
    }

    public static bool isTouchingBottom(Link player, IBlock block)
    {
        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;

        Rectangle playerRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, (int)(width * scale), (int)(height * scale));
        
        Rectangle blockRect = new Rectangle((int)block.Position.X, (int)block.Position.Y, (int)(width * scale2), (int)(height * scale2));

        // Check for collision on the right side when the player is moving right
        return playerRect.Top < blockRect.Bottom &&
            playerRect.Bottom > blockRect.Bottom &&
            playerRect.Left < blockRect.Right &&
            playerRect.Right > blockRect.Left;
           //player.Speed.Y > 0; // Assuming player is moving down (positive speed)
    }

    public static bool isTouchingTop(Link player, IBlock block)
    {
        int width = 16;
        int height = 16;
        float scale = 2f;
        float scale2 = 3f;

        Rectangle playerRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, (int)(width * scale), (int)(height * scale));
        
        Rectangle blockRect = new Rectangle((int)block.Position.X, (int)block.Position.Y, (int)(width * scale2), (int)(height * scale2));

        // Check for collision on the right side when the player is moving right
        
        return playerRect.Bottom > blockRect.Top &&
           playerRect.Top < blockRect.Top && 
           playerRect.Left < blockRect.Right &&
           playerRect.Right > blockRect.Left;
            //player.Speed.X > 0; // Assuming player is moving right (positive speed)
            
    }




}
