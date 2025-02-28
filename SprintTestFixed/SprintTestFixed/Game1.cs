using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
using sprint0Test.Dungeon;
using sprint0Test.Interfaces;
using sprint0Test.Items;
using sprint0Test.Link1;
using sprint0Test.Sprites;

namespace sprint0Test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // 用于玩家动画显示的纹理和 Sprite
        public Texture2D spriteTexture;
        public ISprite sprite;

        // 物品管理
        /*private ItemFactory itemFactory;
        public List<IItem> itemList;
        public int currentItemIndex;
        public IItem currentItem;*/

        // Link（玩家）
        private Link Link;

        // 控制器列表
        List<IController> controllerList;

        // 新增：房间管理器，用于绘制当前房间和检测门碰撞
        RoomManager roomManager;
        float roomScale;  // 绘制房间时的缩放因子

        // BlockSprites（若你还有需要保留，就留着）
        private BlockSprites blockSprites;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // 设置窗口尺寸 800x480
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            controllerList = new List<IController>();
            // 添加一个 MouseController
            controllerList.Add(new MouseController(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // 1) 加载一个纹理给 spriteTexture
            spriteTexture = Content.Load<Texture2D>("Link1");
            sprite = new FixedAnimatedPlayerSprite(spriteTexture);

            // 2) 加载地牢图集
            var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");

            // 3) 初始化 BlockSprites（若你还在用）
            blockSprites = new BlockSprites(dungeonTexture);

            // 4) 物品 & 敌人逻辑（保留你原本的代码）
            /*
            TextureManager.Instance.LoadContent(this);
            EnemyManager.Instance.SpawnEnemy();

            itemFactory = new ItemFactory();
            itemFactory.RegisterTexture("Heart", Content.Load<Texture2D>("heart"));
            itemFactory.RegisterTexture("Boomerang", Content.Load<Texture2D>("boomerang"));
            itemFactory.RegisterItem("Heart", pos => new Heart(itemFactory.GetTexture("Heart"), pos));
            itemFactory.RegisterItem("Boomerang", pos => new Boomerang(itemFactory.GetTexture("Boomerang"), pos, 1, 8));
            itemList = new List<IItem>
            {
                itemFactory.CreateItem("Heart", new Vector2(200, 200)),
                itemFactory.CreateItem("Boomerang", new Vector2(200, 200))
            };
            currentItemIndex = 1;
            currentItem = itemList[currentItemIndex];
            */

            // 5) 加载 Link 动画用的贴图
            var link1 = Content.Load<Texture2D>("Link1");
            var link2 = Content.Load<Texture2D>("Link2");
            var linkB1 = Content.Load<Texture2D>("LinkB1");
            var linkB2 = Content.Load<Texture2D>("LinkB2");
            var linkL1 = Content.Load<Texture2D>("LinkL1");
            var linkL2 = Content.Load<Texture2D>("LinkL2");
            var linkR1 = Content.Load<Texture2D>("LinkR1");
            var linkR2 = Content.Load<Texture2D>("LinkR2");
            var linkS1 = Content.Load<Texture2D>("LinkS1");
            var linkS2 = Content.Load<Texture2D>("LinkS2");
            var linkS3 = Content.Load<Texture2D>("LinkS3");
            var linkS4 = Content.Load<Texture2D>("LinkS4");
            var linkBS1 = Content.Load<Texture2D>("LinkBS1");
            var linkBS2 = Content.Load<Texture2D>("LinkBS2");
            var linkBS3 = Content.Load<Texture2D>("LinkBS3");
            var linkBS4 = Content.Load<Texture2D>("LinkBS4");
            var linkLS1 = Content.Load<Texture2D>("LinkLS1");
            var linkLS2 = Content.Load<Texture2D>("LinkLS2");
            var linkLS3 = Content.Load<Texture2D>("LinkLS3");
            var linkLS4 = Content.Load<Texture2D>("LinkLS4");
            var linkRS1 = Content.Load<Texture2D>("LinkRS1");
            var linkRS2 = Content.Load<Texture2D>("LinkRS2");
            var linkRS3 = Content.Load<Texture2D>("LinkRS3");
            var linkRS4 = Content.Load<Texture2D>("LinkRS4");
            var linkH = Content.Load<Texture2D>("Linkh");

            // 建立 Link 的动画映射
            Dictionary<(LinkAction, LinkDirection), List<Texture2D>> linkMap = new Dictionary<(LinkAction, LinkDirection), List<Texture2D>>();
            linkMap.Add((LinkAction.Idle, LinkDirection.Down), new List<Texture2D> { link1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Up), new List<Texture2D> { linkB1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Left), new List<Texture2D> { linkL1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Right), new List<Texture2D> { linkR1 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Down), new List<Texture2D> { link1, link2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Up), new List<Texture2D> { linkB1, linkB2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Left), new List<Texture2D> { linkL1, linkL2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Right), new List<Texture2D> { linkR1, linkR2 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Down), new List<Texture2D> { linkS1, linkS2, linkS3, linkS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Up), new List<Texture2D> { linkBS1, linkBS2, linkBS3, linkBS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Left), new List<Texture2D> { linkLS1, linkLS2, linkLS3, linkLS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Right), new List<Texture2D> { linkRS1, linkRS2, linkRS3, linkRS4 });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Down), new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Up), new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Left), new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Right), new List<Texture2D> { linkH });

            LinkSprite linkSprite = new LinkSprite(linkMap);
            Link = new Link(linkSprite, new Vector2(200, 200));

            // 注册键盘控制器
            controllerList.Add(new KeyboardController(this, Link, blockSprites));

            // 6) 初始化房间管理器
            //   原房间尺寸256×176，窗口800×480，计算缩放
            roomScale = Math.Min(800f / 256f, 480f / 176f);
            roomManager = new RoomManager(dungeonTexture, roomScale);
        }

        protected override void Update(GameTime gameTime)
        {
            // 按 ESC 退出
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // 更新控制器
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            // 更新 Link、sprite、物品、敌人等
            sprite.Update();
            //currentItem.Update(gameTime);
            //blockSprites.UpdateActiveBlocks();
            //EnemyManager.Instance.Update(gameTime);
            Link.Update();

            // 如果 Link 走到门的位置，切换到下一个房间
            // 这里简单地把 Link 的碰撞范围当成 (Position, spriteTexture.Width, spriteTexture.Height)
            Vector2 linkSize = new Vector2(spriteTexture.Width, spriteTexture.Height);
            if (roomManager.IsLinkAtDoor(Link.Position, linkSize))
            {
                roomManager.SwitchToNextRoom();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // 1) 绘制当前房间（外墙、内室、门/墙）
            roomManager.DrawRoom(_spriteBatch);

            // 2) 绘制 Link、物品、敌人、Blocks
            Link.Draw(_spriteBatch);
            sprite.Draw(_spriteBatch);
            //currentItem.Draw(_spriteBatch);
            //blockSprites.DrawActiveBlocks(_spriteBatch);
            //EnemyManager.Instance.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}




