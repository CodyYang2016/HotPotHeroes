
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test;


public class BlockSprites
{
    // Define all the block rectangles
    private Rectangle tile = new Rectangle(984, 11, 16, 16);
    private Rectangle black = new Rectangle(984, 27, 16, 16);
    private Rectangle brick = new Rectangle(984, 45, 16, 16);
    private Rectangle block = new Rectangle(1001, 11, 16, 16);
    private Rectangle sand = new Rectangle(1001, 27, 16, 16);
    private Rectangle ramp = new Rectangle(1001, 45, 16, 16);
    private Rectangle fish = new Rectangle(1018, 11, 16, 16);
    private Rectangle blue = new Rectangle(1018, 27, 16, 16);
    private Rectangle dragon = new Rectangle(1035, 11, 16, 16);
    private Rectangle stair = new Rectangle(1035, 27, 16, 16);

    // The list of game objects
    private List<IBlock> gameObjects = new List<IBlock>();

    public List<IBlock> _active = new List<IBlock>(); // The active game objects list
    
    private int currentIndex = 0; // The current index for managing blocks

    public BlockSprites(Texture2D dungeonTexture)
    {
        // int roomWidth = 14;  // Number of tiles in width
        // int roomHeight = 9; // Number of tiles in height
        // int tileSize = 44;   // Each tile is 48x48 pixels
        // Vector2 roomStart = new Vector2(40, 38); // Starting position of the room

        // // Define door positions (adjust as needed)
        // Vector2 topDoor = new Vector2(roomStart.X + (roomWidth / 2) * tileSize, roomStart.Y);
        // Vector2 bottomDoor = new Vector2(roomStart.X + (roomWidth / 2) * tileSize, roomStart.Y + (roomHeight - 1) * tileSize);
        // Vector2 leftDoor = new Vector2(roomStart.X, roomStart.Y + (roomHeight / 2) * tileSize);
        // Vector2 rightDoor = new Vector2(roomStart.X + (roomWidth - 1) * tileSize, roomStart.Y + (roomHeight / 2) * tileSize);

        // // Add brick blocks around the edges, skipping door positions
        // for (int x = 0; x < roomWidth; x++)
        // {
        //     Vector2 topPos = new Vector2(roomStart.X + x * tileSize, roomStart.Y);
        //     Vector2 bottomPos = new Vector2(roomStart.X + x * tileSize, roomStart.Y + (roomHeight - 1) * tileSize);

        //     if ((topPos != topDoor) && (topPos.X != topDoor.X - tileSize))  // Don't place brick at the top door
        //         _active.Add(new Block(dungeonTexture, tile, topPos, 3f, false));

        //     if (bottomPos != bottomDoor && (bottomPos.X != bottomDoor.X - tileSize)) // Don't place brick at the bottom door
        //         _active.Add(new Block(dungeonTexture, tile, bottomPos, 3f, false));
        // }

        // for (int y = 0; y < roomHeight; y++)
        // {
        //     Vector2 leftPos = new Vector2(roomStart.X, roomStart.Y + y * tileSize);
        //     Vector2 rightPos = new Vector2(roomStart.X + (roomWidth - 1) * tileSize, roomStart.Y + y * tileSize);

        //     if (leftPos != leftDoor && (leftPos.Y != leftDoor.Y - tileSize) && (leftPos.Y != leftDoor.Y + tileSize)) // Don't place brick at the left door
        //         _active.Add(new Block(dungeonTexture, tile, leftPos, 3f, false));

        //     if (rightPos != rightDoor && (leftPos.Y != leftDoor.Y - tileSize) && (leftPos.Y != leftDoor.Y + tileSize)) // Don't place brick at the right door
        //         _active.Add(new Block(dungeonTexture, tile, rightPos, 3f, false));
                
        // }
        //_active.Add(new Block(dungeonTexture, stair, new Vector2(400, 300)));
    }


    // Set the active list of blocks
    public void SetActiveList(IBlock newSprite)
    {
        _active.Clear(); // Optionally clear the _active list or keep adding/removing sprites as needed
        _active.Add(newSprite);
    }

    // Get all game objects (blocks) from the list
    public List<IBlock> GetGameObjects()
    {
        return gameObjects;
    }

    // Get the current index
    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    // Set the current index
    public void SetCurrentIndex(int index)
    {
        currentIndex = index;
    }

    // Optionally, you can add an accessor for the _active list
    public List<IBlock> GetActiveList()
    {
        return _active;
    }

    // Update the active blocks
    public void UpdateActiveBlocks()
    {
        foreach (var block in _active)
        {
            block.Update();
        }
    }

    // Draw the active blocks
    public void DrawActiveBlocks(SpriteBatch spriteBatch)
    {
        foreach (var block in _active)
        {
            block.Draw(spriteBatch);
        }
    }
}
