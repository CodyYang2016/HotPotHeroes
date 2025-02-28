using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test;

namespace sprint0Test
{
    public class BlockSprites
    {
        // 公开 DungeonTexture，让外部也能获取
        public Texture2D DungeonTexture { get; private set; }

        // 原先的矩形字段改为私有，然后通过公共属性公开
        private Rectangle tile = new Rectangle(984, 11, 16, 16);
        public Rectangle TileRect => tile;

        private Rectangle black = new Rectangle(984, 27, 16, 16);
        public Rectangle BlackRect => black;

        private Rectangle brick = new Rectangle(984, 45, 16, 16);
        public Rectangle BrickRect => brick;

        private Rectangle block = new Rectangle(1001, 11, 16, 16);
        public Rectangle BlockRect => block;

        private Rectangle sand = new Rectangle(1001, 27, 16, 16);
        public Rectangle SandRect => sand;

        private Rectangle ramp = new Rectangle(1001, 45, 16, 16);
        public Rectangle RampRect => ramp;

        private Rectangle fish = new Rectangle(1018, 11, 16, 16);
        public Rectangle FishRect => fish;

        private Rectangle blue = new Rectangle(1018, 27, 16, 16);
        public Rectangle BlueRect => blue;

        private Rectangle dragon = new Rectangle(1035, 11, 16, 16);
        public Rectangle DragonRect => dragon;

        private Rectangle stair = new Rectangle(1035, 27, 16, 16);
        public Rectangle StairRect => stair;

        // 全部的游戏对象列表（示例）
        private List<IBlock> gameObjects = new List<IBlock>();

        // 当前激活的对象列表
        private List<IBlock> _active = new List<IBlock>();
        private int currentIndex = 0;

        // 构造函数
        public BlockSprites(Texture2D dungeonTexture)
        {
            DungeonTexture = dungeonTexture;

            // 根据之前的示例，往 gameObjects 中加入若干 Block
            // 这里仅做示例，你可根据需要增减
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

        // 设置当前激活的Block列表（只放一个或多个均可）
        public void SetActiveList(IBlock newSprite)
        {
            _active.Clear();
            _active.Add(newSprite);
        }

        // 获取全部的游戏对象
        public List<IBlock> GetGameObjects()
        {
            return gameObjects;
        }

        // 获取当前索引
        public int GetCurrentIndex()
        {
            return currentIndex;
        }

        // 设置当前索引
        public void SetCurrentIndex(int index)
        {
            currentIndex = index;
        }

        // 可选：获取当前激活列表
        public List<IBlock> GetActiveList()
        {
            return _active;
        }

        // 更新激活的block
        public void UpdateActiveBlocks()
        {
            foreach (var block in _active)
            {
                block.Update();
            }
        }

        // 绘制激活的block
        public void DrawActiveBlocks(SpriteBatch spriteBatch)
        {
            foreach (var block in _active)
            {
                block.Draw(spriteBatch);
            }
        }
    }
}
