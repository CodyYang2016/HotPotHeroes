using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test;

public class BlockSprites
{
    // Define all the block rectangles
    public Rectangle tile = new Rectangle(984, 11, 16, 16);
    public Rectangle black = new Rectangle(984, 27, 16, 16);
    public Rectangle brick = new Rectangle(984, 45, 16, 16);
    public Rectangle block = new Rectangle(1001, 11, 16, 16);
    public Rectangle sand = new Rectangle(1001, 27, 16, 16);
    public Rectangle ramp = new Rectangle(1001, 45, 16, 16);
    public Rectangle fish = new Rectangle(1018, 11, 16, 16);
    public Rectangle blue = new Rectangle(1018, 27, 16, 16);
    public Rectangle dragon = new Rectangle(1035, 11, 16, 16);
    public Rectangle stair = new Rectangle(1035, 27, 16, 16);

    // The list of game objects
    public List<IBlock> gameObjects = new List<IBlock>();

    // Constructor to initialize blocks
    public BlockSprites(Texture2D dungeonTexture)
    {
        // Add blocks to the game objects list with the texture and rectangle
        gameObjects.Add(new Block(dungeonTexture, tile, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, black, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, brick, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new BlockPush(dungeonTexture, block, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, sand, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, ramp, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, fish, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, blue, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new Block(dungeonTexture, dragon, ObjectType.Rock, new Vector2(100, 80), 3f));
        gameObjects.Add(new BlockStair(dungeonTexture, stair, new Vector2(100, 80), 3f));
    }
}
