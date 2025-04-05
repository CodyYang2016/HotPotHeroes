
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test;


public class Perimeter
{
    public List<IBlock> perimeter = new List<IBlock>();

    public Perimeter(Texture2D dungeonTexture)
    {
        int roomWidth = 28;  // Number of tiles in width
        int roomHeight = 9; // Number of tiles in height
        int tileSize = 44;   // Each tile is 48x48 pixels
        Vector2 roomStart = new Vector2(40, 38); // Starting position of the room

        // Define door positions (adjust as needed)
        Vector2 topDoor = new Vector2(roomStart.X + (roomWidth / 2) * tileSize, roomStart.Y);
        Vector2 bottomDoor = new Vector2(roomStart.X + (roomWidth / 2) * tileSize, roomStart.Y + (roomHeight - 1) * tileSize);
        Vector2 leftDoor = new Vector2(roomStart.X, roomStart.Y + (roomHeight / 2) * tileSize);
        Vector2 rightDoor = new Vector2(roomStart.X + (roomWidth - 1) * tileSize, roomStart.Y + (roomHeight / 2) * tileSize);

        // Add brick blocks around the edges, skipping door positions
        for (int x = 0; x < roomWidth; x++)
        {
            Vector2 topPos = new Vector2(roomStart.X + x * tileSize, roomStart.Y);
            Vector2 bottomPos = new Vector2(roomStart.X + x * tileSize, roomStart.Y + (roomHeight - 1) * tileSize);

            if (topPos != topDoor)  // Don't place brick at the top door
                BlockManager.Instance.CreateBlock(topPos, BlockType.Tile, 3f, false);
                //perimeter.Add(BlockManager.Instance.CreateBlock(topPos, BlockType.Tile, 3f, false));

            if (bottomPos != bottomDoor) // Don't place brick at the bottom door
                BlockManager.Instance.CreateBlock(topPos, BlockType.Tile, 3f, false);
        };

        for (int y = 0; y < roomHeight; y++)
        {
            Vector2 leftPos = new Vector2(roomStart.X, roomStart.Y + y * tileSize);
            Vector2 rightPos = new Vector2(roomStart.X + (roomWidth - 1) * tileSize, roomStart.Y + y * tileSize);

            if (leftPos != leftDoor) // Don't place brick at the left door
                BlockManager.Instance.CreateBlock(leftPos, BlockType.Tile, 3f, false);

            if (rightPos != rightDoor)  // Don't place brick at the right door
                BlockManager.Instance.CreateBlock(rightPos, BlockType.Tile, 3f, false);
                
        };
    }

}
