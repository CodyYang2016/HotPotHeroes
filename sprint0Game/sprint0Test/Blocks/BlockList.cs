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
    public List<Block> temp = new List<Block>(); // Temp list
    private int currentIndex = 0; // The current index for managing blocks

    // Constructor to initialize blocks
    public BlockSprites(Texture2D dungeonTexture)
    {
        // Add blocks to the game objects list with the texture and rectangle
        gameObjects.Add(new Block(dungeonTexture, tile, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, black, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, brick, ObjectType.Rock, new Vector2(100, 80), 3f));
        //gameObjects.Add(new BlockPush(dungeonTexture, block, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, sand, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, ramp, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, fish, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, blue, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, dragon, ObjectType.Rock, new Vector2(100, 80), 3f));
        //gameObjects.Add(new BlockStair(dungeonTexture, stair, new Vector2(100, 80), 3f));
    }

    // Set the active list of blocks
    public void SetActiveList(IBlock newSprite)
    {
        _active.Clear(); // Optionally clear the _active list or keep adding/removing sprites as needed
        _active.Add(newSprite);
        temp.Clear();
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
