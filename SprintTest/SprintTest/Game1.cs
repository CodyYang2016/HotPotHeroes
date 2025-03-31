using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Commands;
using sprint0Test.Dungeon;
using sprint0Test.Interfaces;
using sprint0Test.Items;
using sprint0Test.Link1;
using sprint0Test.Managers;
using sprint0Test.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace sprint0Test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // 为命令类和其它功能提供公共贴图访问
        public Texture2D spriteTexture;

        // 用于示例的玩家精灵 (若命令类要切换不同的sprite，可用这个)
        public ISprite sprite;

        // BlockSprites
        private BlockSprites blockSprites;

        // ItemFactory
        private ItemFactory itemFactory;

        // 仅为旧的 CycleItemCommand 使用；若不再使用可注释掉
        public List<IItem> itemList;
        public int currentItemIndex;
        public IItem currentItem;

        // Room / Dungeon
        private RoomManager roomManager;
        private float roomScale;

        // 各种碰撞处理器
        private PlayerBlockCollisionHandler playerBlockCollisionHandler;
        private PlayerEnemyCollisionHandler playerEnemyCollisionHandler;
        private PlayerItemCollisionHandler playerItemCollisionHandler;
        private EnemyBlockCollisionHandler enemyBlockCollisionHandler;
        private PlayerProjectileCollisionHandler playerProjectileCollisionHandler;
        private ProjectileBlockCollisionHandler projectileBlockCollisionHandler;

        // 控制器列表
        private List<IController> controllerList;

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
            // 仅鼠标控制器
            controllerList.Add(new MouseController(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // 给 spriteTexture 赋值，用于 SetDispxxxSprite 等命令
            // 如果你没有 "Link1.png"，可换成其他贴图名称
            spriteTexture = Content.Load<Texture2D>("Link1");

            // 1) 加载地牢图集
            Texture2D dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");
            blockSprites = new BlockSprites(dungeonTexture);

            // 2) 初始化 TextureManager 并加载敌人
            TextureManager.Instance.LoadContent(this);
            EnemyManager.Instance.SpawnEnemy();

            // 3) 初始化物品工厂
            itemFactory = new ItemFactory();
            itemFactory.RegisterTexture("Heart", Content.Load<Texture2D>("heart"));
            itemFactory.RegisterTexture("RedPotion", Content.Load<Texture2D>("red-potion"));
            itemFactory.RegisterTexture("BluePotion", Content.Load<Texture2D>("blue-potion"));
            itemFactory.RegisterTexture("GreenPotion", Content.Load<Texture2D>("green-potion"));
            itemFactory.RegisterTexture("RedRupee", Content.Load<Texture2D>("red-rupee"));
            itemFactory.RegisterTexture("BlueRupee", Content.Load<Texture2D>("blue-rupee"));
            itemFactory.RegisterTexture("GreenRupee", Content.Load<Texture2D>("green-rupee"));
            itemFactory.RegisterTexture("Apple", Content.Load<Texture2D>("apple"));
            itemFactory.RegisterTexture("Crystal", Content.Load<Texture2D>("crystal"));
            // itemFactory.RegisterTexture("Boomerang", Content.Load<Texture2D>("boomerang"));

            itemFactory.RegisterItem("Heart", pos => new Heart("Heart", itemFactory.GetTexture("Heart"), pos));
            itemFactory.RegisterItem("RedPotion", pos => new Potion("RedPotion", itemFactory.GetTexture("RedPotion"), pos));
            itemFactory.RegisterItem("BluePotion", pos => new Potion("BluePotion", itemFactory.GetTexture("BluePotion"), pos));
            itemFactory.RegisterItem("GreenPotion", pos => new Potion("GreenPotion", itemFactory.GetTexture("GreenPotion"), pos));
            itemFactory.RegisterItem("RedRupee", pos => new Rupee("RedRupee", itemFactory.GetTexture("RedRupee"), pos));
            itemFactory.RegisterItem("BlueRupee", pos => new Rupee("BlueRupee", itemFactory.GetTexture("BlueRupee"), pos));
            itemFactory.RegisterItem("GreenRupee", pos => new Rupee("GreenRupee", itemFactory.GetTexture("GreenRupee"), pos));
            itemFactory.RegisterItem("Apple", pos => new Apple("Apple", itemFactory.GetTexture("Apple"), pos));
            itemFactory.RegisterItem("Crystal", pos => new Crystal("Crystal", itemFactory.GetTexture("Crystal"), pos));
            // itemFactory.RegisterItem("Boomerang", pos => new Boomerang(itemFactory.GetTexture("Boomerang"), pos, 1, 8));

            // 4) 初始化房间管理器
            roomScale = Math.Min(800f / 256f, 480f / 176f);
            roomManager = new RoomManager(dungeonTexture, roomScale, itemFactory);

            // 5) 构建 Link 动画
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

            // 初始化碰撞处理器
            playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
            playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
            playerItemCollisionHandler = new PlayerItemCollisionHandler();
            enemyBlockCollisionHandler = new EnemyBlockCollisionHandler();
            playerProjectileCollisionHandler = new PlayerProjectileCollisionHandler();
            projectileBlockCollisionHandler = new ProjectileBlockCollisionHandler();

            // 初始化 Link (Singleton)
            Link.Initialize(linkSprite, new Vector2(200, 200));

            // 初始化键盘控制器
            controllerList.Add(new KeyboardController(this, Link.Instance, blockSprites));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // 如果 roomManager 或 Link.Instance 未初始化，先跳过
            if (roomManager == null || Link.Instance == null)
            {
                base.Update(gameTime);
                return;
            }

            // 更新所有控制器
            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            // 如果 sprite 不为 null, 就更新
            if (sprite != null)
            {
                sprite.Update();
            }

            // 更新当前房间物品
            var items = roomManager.GetCurrentRoomItems();
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.Update(gameTime);
                }
            }

            // 更新 Blocks
            blockSprites.UpdateActiveBlocks();

            // 更新敌人
            EnemyManager.Instance.Update(gameTime);

            // 更新投射物
            ProjectileManager.Instance.Update(gameTime);

            // 更新 Link
            Link.Instance.Update();

            // 处理碰撞
            playerBlockCollisionHandler.HandleCollisionList(blockSprites._active);
            playerEnemyCollisionHandler.HandleCollision(EnemyManager.Instance.GetActiveEnemy());
            playerItemCollisionHandler.HandleCollisionList(items);
            enemyBlockCollisionHandler.HandleCollisionList(blockSprites._active, EnemyManager.Instance.GetActiveEnemy());
            playerProjectileCollisionHandler.HandleCollisionList(ProjectileManager.Instance.GetActiveProjectiles());
            projectileBlockCollisionHandler.HandleCollisionList(blockSprites._active, ProjectileManager.Instance.GetActiveProjectiles());

            // 判断 Link 是否在门口
            Vector2 linkSize = Link.Instance.GetScaledDimensions();
            if (roomManager.IsLinkAtDoor(Link.Instance.Position, linkSize))
            {
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // 切换到下一个房间并获取在新房间的生成位置
                    Vector2 spawnPos = roomManager.SwitchToNextRoom();
                    Link.Instance.SetPosition(spawnPos);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // 如果 roomManager 不为空，则绘制当前房间
            if (roomManager != null)
            {
                roomManager.DrawRoom(_spriteBatch);
            }

            // 绘制投射物
            ProjectileManager.Instance.Draw(_spriteBatch);

            // 绘制 Link
            if (Link.Instance != null)
            {
                Link.Instance.Draw(_spriteBatch);
            }

            // 绘制 Blocks
            blockSprites.DrawActiveBlocks(_spriteBatch);

            // 绘制敌人
            EnemyManager.Instance.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
