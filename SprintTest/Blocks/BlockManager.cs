using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0Test
{
    public enum BlockType
    {
        Tile,
        Black,
        Brick,
        Block,
        Sand,
        Ramp,
        Fish,
        Blue,
        Dragon,
        Stair
    }

    public class BlockManager
    {
        private static BlockManager _instance;
        public static BlockManager Instance => _instance ??= new BlockManager();

        // Static texture field, shared by all instances of BlockManager
        private static Texture2D blockTexture;

        // List of all blocks in the room
        private List<IBlock> blocks = new List<IBlock>();

        private Dictionary<BlockType, Rectangle> blockTypeToRectangle = new Dictionary<BlockType, Rectangle>
        {
            { BlockType.Tile, new Rectangle(984, 11, 16, 16) },
            { BlockType.Black, new Rectangle(984, 27, 16, 16) },
            { BlockType.Brick, new Rectangle(984, 45, 16, 16) },
            { BlockType.Block, new Rectangle(1001, 11, 16, 16) },
            { BlockType.Sand, new Rectangle(1001, 27, 16, 16) },
            { BlockType.Ramp, new Rectangle(1001, 45, 16, 16) },
            { BlockType.Fish, new Rectangle(1018, 11, 16, 16) },
            { BlockType.Blue, new Rectangle(1018, 27, 16, 16) },
            { BlockType.Dragon, new Rectangle(1035, 11, 16, 16) },
            { BlockType.Stair, new Rectangle(1035, 27, 16, 16) }
        };


        public BlockManager()
        {
        }

        // Method to create and add a block to the room
        public void CreateBlock(Vector2 position, BlockType blockType, float scale = 3.0f, bool isVisible = true, bool isSolid = true)
        {
            Rectangle blockRectangle = blockTypeToRectangle[blockType];
            Block newBlock = new Block(blockTexture, blockRectangle, position, scale, isVisible, isSolid);

            // Add it to the list of blocks in the room
            blocks.Add(newBlock);
        }

        public void CreateBlockPush(Vector2 position, BlockType blockType, float scale = 3.0f, bool isVisible = true)
        {
            Rectangle blockRectangle = blockTypeToRectangle[blockType];
            BlockPush newBlock = new BlockPush(blockTexture, blockRectangle, position, scale, isVisible);

            // Add it to the list of blocks in the room
            blocks.Add(newBlock);
        }
        
        public void CreateBlockStair(Vector2 position, BlockType blockType, float scale = 3.0f, bool isVisible = true)
        {
            Rectangle blockRectangle = blockTypeToRectangle[blockType];
            BlockStair newBlock = new BlockStair(blockTexture, blockRectangle, position, scale, isVisible);

            // Add it to the list of blocks in the room
            blocks.Add(newBlock);
        }
        public void Clear()
        {
            blocks.Clear();
        }

        // Optional method to add blocks along the perimeter of the room (if needed)
        // public void InitializePerimeter(int roomWidth, int roomHeight, Rectangle sourceRectangle, float scale = 1.0f)
        // {
        //     // Add blocks along the top and bottom edges
        //     for (int x = 0; x < roomWidth; x += sourceRectangle.Width)
        //     {
        //         CreateBlock(new Vector2(x, 0), sourceRectangle, scale); // Top row
        //         CreateBlock(new Vector2(x, roomHeight - sourceRectangle.Height), sourceRectangle, scale); // Bottom row
        //     }

        //     // Add blocks along the left and right edges (excluding the corners since they are already added)
        //     for (int y = sourceRectangle.Height; y < roomHeight - sourceRectangle.Height; y += sourceRectangle.Height)
        //     {
        //         CreateBlock(new Vector2(0, y), sourceRectangle, scale); // Left column
        //         CreateBlock(new Vector2(roomWidth - sourceRectangle.Width, y), sourceRectangle, scale); // Right column
        //     }
        // }

        // Update all blocks in the room (if needed)
        public void Update()
        {
            foreach (var block in blocks)
            {
                block.Update();
            }
        }

        // Draw all blocks in the room (if needed)
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in blocks)
            {
                block.Draw(spriteBatch);
            }
        }

        public static void LoadTexture(Texture2D texture)
        {
            blockTexture = texture;
        }

        public int GetBlocksCount()
        {
            return blocks.Count;
        }

        public List<IBlock> GetActiveBlocks()
        {
            return blocks;
        }

        public void ClearActiveBlocks()
        {
            blocks.Clear();
        }

        // Optional method to remove a block (based on position or reference)
        // public void RemoveBlock(IBlock block)
        // {
        //     blocks.Remove(block);
        // }
    }
}
