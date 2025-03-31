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

        public Texture2D spriteTexture;

        public ISprite sprite;

        // BlockSprites
        private BlockSprites blockSprites;

        // ItemFactory
        private ItemFactory itemFactory;

        public List<IItem> itemList;
        public int currentItemIndex;
        public IItem currentItem;

        // Room / Dungeon
        private RoomManager roomManager;
        private float roomScale;

        private PlayerBlockCollisionHandler playerBlockCollisionHandler;
        private PlayerEnemyCollisionHandler playerEnemyCollisionHandler;
        private PlayerItemCollisionHandler playerItemCollisionHandler;
        private EnemyBlockCollisionHandler enemyBlockCollisionHandler;
        private PlayerProjectileCollisionHandler playerProjectileCollisionHandler;
        private ProjectileBlockCollisionHandler projectileBlockCollisionHandler;

        private List<IController> controllerList;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            controllerList = new List<IController>();
            controllerList.Add(new MouseController(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            spriteTexture = Content.Load<Texture2D>("Link1");

            Texture2D dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");
            blockSprites = new BlockSprites(dungeonTexture);

            TextureManager.Instance.LoadContent(this);
            EnemyManager.Instance.SpawnEnemy();

           
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

            roomScale = Math.Min(800f / 256f, 480f / 176f);
            roomManager = new RoomManager(dungeonTexture, roomScale, itemFactory);

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

            playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
            playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
            playerItemCollisionHandler = new PlayerItemCollisionHandler();
            enemyBlockCollisionHandler = new EnemyBlockCollisionHandler();
            playerProjectileCollisionHandler = new PlayerProjectileCollisionHandler();
            projectileBlockCollisionHandler = new ProjectileBlockCollisionHandler();

            Link.Initialize(linkSprite, new Vector2(200, 200));

            controllerList.Add(new KeyboardController(this, Link.Instance, blockSprites));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (roomManager == null || Link.Instance == null)
            {
                base.Update(gameTime);
                return;
            }

            foreach (IController controller in controllerList)
            {
                controller.Update();
            }

            if (sprite != null)
            {
                sprite.Update();
            }

            var items = roomManager.GetCurrentRoomItems();
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.Update(gameTime);
                }
            }
            
            blockSprites.UpdateActiveBlocks();
           
            EnemyManager.Instance.Update(gameTime);
            
            ProjectileManager.Instance.Update(gameTime);
          
            Link.Instance.Update();
           
            playerBlockCollisionHandler.HandleCollisionList(blockSprites._active);
            playerEnemyCollisionHandler.HandleCollision(EnemyManager.Instance.GetActiveEnemy());
            playerItemCollisionHandler.HandleCollisionList(items);
            enemyBlockCollisionHandler.HandleCollisionList(blockSprites._active, EnemyManager.Instance.GetActiveEnemy());
            playerProjectileCollisionHandler.HandleCollisionList(ProjectileManager.Instance.GetActiveProjectiles());
            projectileBlockCollisionHandler.HandleCollisionList(blockSprites._active, ProjectileManager.Instance.GetActiveProjectiles());
         
            Vector2 linkSize = Link.Instance.GetScaledDimensions();
            if (roomManager.IsLinkAtDoor(Link.Instance.Position, linkSize))
            {
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    
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

            
            if (roomManager != null)
            {
                roomManager.DrawRoom(_spriteBatch);
            }

           
            ProjectileManager.Instance.Draw(_spriteBatch);

            
            if (Link.Instance != null)
            {
                Link.Instance.Draw(_spriteBatch);
            }

            
            blockSprites.DrawActiveBlocks(_spriteBatch);

            
            EnemyManager.Instance.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
