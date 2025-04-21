using System.Collections.Generic;
using sprint0Test.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Interfaces;
using sprint0Test.Items;
using sprint0Test.Sprites;
using sprint0Test.Link1;
using System;
using sprint0Test.Dungeon;
using sprint0Test;
using sprint0Test.Managers;
using System.Diagnostics;
using sprint0Test.Enemy;
using sprint0Test.Room;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
namespace sprint0Test;

public class Game1 : Game
{
    // Game1 Instance
    public static Game1 Instance { get; private set; }

    public enum GameState
    {
        Playing,
        Paused
    }
    private PauseMenu _pauseMenu;
    public GameState _currentGameState = GameState.Playing;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public Texture2D spriteTexture;
    List<IController> controllerList;
    public ISprite sprite;
    private SpriteFont _menuFont;
    private SpriteFont uiFont;
    private ItemFactory itemFactory;
    public List<IItem> itemList;
    public int currentItemIndex;
    private Link Link;
    public IItem currentItem;

    // New Room Manager
    public RoomManager roomManager;
    float roomScale;


    // New Collision Handler
    private MasterCollisionHandler masterCollisionHandler;


    // Pause-related fields
    private bool isPaused;
    private SpriteFont pauseFont;

    private KeyboardState previousKeyboardState;

    // Code for the new Link Health
    private Texture2D heartTexture;
    private Texture2D multiplicationTexture;
    private List<Vector2> heartPositions;
    private List<Vector2> multiplicationPositions;
    private Vector2 bombPosition;
    private Vector2 applePosition;
    private Vector2 crystalPosition;
    private int collisionCount;
    private float maxHearts = 3f;
    private float currentHearts;
    private bool isPlayerDead;
    private float respawnTimer;
    private Vector2 playerRespawnPosition;

    public Game1()
    {
        Instance = this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        // 设置窗口尺寸 800x480
        _graphics.PreferredBackBufferWidth = 735;
        _graphics.PreferredBackBufferHeight = 480;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        controllerList = new List<IController>();
        //controllerList.Add(new KeyboardController(this, Link, blockSprites));
        controllerList.Add(new MouseController(this));
        Bomb.OnBombPlanted += (sender, args) =>
        {
            roomManager.CurrentRoom.Items.Add(args.Bomb);
        };
        GraphicsDeviceHelper.Device = GraphicsDevice;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //spriteTexture = Content.Load<Texture2D>("mario2");
        //sprite = new StandingInPlacePlayerSprite(spriteTexture);

        //_menuFont = Content.Load<SpriteFont>("MenuFont");
        //_pauseMenu = new PauseMenu(Content.Load<SpriteFont>("MenuFont"));
        //_pauseMenu.OnOptionSelected = HandleMenuSelection;
        //pauseFont = Content.Load<SpriteFont>("PauseFont"); // Load the font

        masterCollisionHandler = new MasterCollisionHandler(); // Initialize the collision handler


        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");
        TextureManager.Instance.LoadContent(this);
        EnemyManager.Instance.SpawnEnemy();
        itemFactory = new ItemFactory();

        // Load BlockManager
        BlockManager.LoadTexture(dungeonTexture);

        //Register Textures
        // Heart working 
        heartTexture = Content.Load<Texture2D>("heart");
        multiplicationTexture = Content.Load<Texture2D>("multiplication");
        uiFont = Content.Load<SpriteFont>("UIFont");
        itemFactory.RegisterTexture("Heart", Content.Load<Texture2D>("heart"));
        itemFactory.RegisterTexture("Apple", Content.Load<Texture2D>("apple"));
        itemFactory.RegisterTexture("Crystal", Content.Load<Texture2D>("crystal"));
        itemFactory.RegisterTexture("Bomb", Content.Load<Texture2D>("bomb"));

        //itemFactory.RegisterTexture("Boomerang", Content.Load<Texture2D>("boomerang"));

        //Register Item Creation Logic
        itemFactory.RegisterItem("Heart", position => new Heart("Heart", itemFactory.GetTexture("Heart"), position));
        itemFactory.RegisterItem("Apple", position => new Apple("Apple", itemFactory.GetTexture("Apple"), position));
        itemFactory.RegisterItem("Crystal", position => new Crystal("Crystal", itemFactory.GetTexture("Crystal"), position));
        itemFactory.RegisterItem("Bomb", position => new Bomb("Bomb", itemFactory.GetTexture("Bomb"), position));

        //itemFactory.RegisterItem("Boomerang", position => new Boomerang(itemFactory.GetTexture("Boomerang"), position, 1, 8));
        ItemManager.Initialize(itemFactory);
        ItemManager.Instance.LoadFromCSV("Content/room_items.csv");

        // 6) 初始化房间管理器
        //   原房间尺寸256×176，窗口800×480，计算缩放
        roomScale = Math.Min(800f / 256f, 480f / 176f);
        roomManager = new RoomManager();

        // Link update code
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
        Dictionary<(LinkAction, LinkDirection), List<Texture2D>> linkMap =
    new Dictionary<(LinkAction, LinkDirection), List<Texture2D>>();

        // Idle
        linkMap.Add((LinkAction.Idle, LinkDirection.Down), new List<Texture2D> { link1 });
        linkMap.Add((LinkAction.Idle, LinkDirection.Up), new List<Texture2D> { linkB1 });
        linkMap.Add((LinkAction.Idle, LinkDirection.Left), new List<Texture2D> { linkL1 });
        linkMap.Add((LinkAction.Idle, LinkDirection.Right), new List<Texture2D> { linkR1 });

        // Walking
        linkMap.Add((LinkAction.Walking, LinkDirection.Down),
            new List<Texture2D> { link1, link2 });
        linkMap.Add((LinkAction.Walking, LinkDirection.Up),
            new List<Texture2D> { linkB1, linkB2 });
        linkMap.Add((LinkAction.Walking, LinkDirection.Left),
            new List<Texture2D> { linkL1, linkL2 });
        linkMap.Add((LinkAction.Walking, LinkDirection.Right),
            new List<Texture2D> { linkR1, linkR2 });

        // Attacking
        linkMap.Add((LinkAction.Attacking, LinkDirection.Down),
            new List<Texture2D> { linkS1, linkS2, linkS3, linkS4 });
        linkMap.Add((LinkAction.Attacking, LinkDirection.Up),
            new List<Texture2D> { linkBS1, linkBS2, linkBS3, linkBS4 });
        linkMap.Add((LinkAction.Attacking, LinkDirection.Left),
            new List<Texture2D> { linkLS1, linkLS2, linkLS3, linkLS4 });
        linkMap.Add((LinkAction.Attacking, LinkDirection.Right),
            new List<Texture2D> { linkRS1, linkRS2, linkRS3, linkRS4 });

        // Damage
        linkMap.Add((LinkAction.Damaged, LinkDirection.Down),
            new List<Texture2D> { linkH });
        linkMap.Add((LinkAction.Damaged, LinkDirection.Up),
            new List<Texture2D> { linkH });
        linkMap.Add((LinkAction.Damaged, LinkDirection.Left),
            new List<Texture2D> { linkH });
        linkMap.Add((LinkAction.Damaged, LinkDirection.Right),
            new List<Texture2D> { linkH });
        linkMap.Add((LinkAction.UsingItem, LinkDirection.Down), new List<Texture2D> { link1 });
        linkMap.Add((LinkAction.UsingItem, LinkDirection.Up), new List<Texture2D> { linkB1 });
        linkMap.Add((LinkAction.UsingItem, LinkDirection.Left), new List<Texture2D> { linkL1 });
        linkMap.Add((LinkAction.UsingItem, LinkDirection.Right), new List<Texture2D> { linkR1 });

        LinkSprite linkSprite = new LinkSprite(linkMap);
        Console.WriteLine("roomItems: " + (roomManager.GetCurrentRoomItems() != null));
        Console.WriteLine("enemies: " + (EnemyManager.Instance.GetActiveEnemy() != null));
        Console.WriteLine("projectiles: " + (ProjectileManager.Instance.GetActiveProjectiles() != null));
        Console.WriteLine("blocks: " + (BlockManager.Instance.GetActiveBlocks() != null));
        Link.Initialize(linkSprite, new Vector2(200, 200));

        heartPositions = new List<Vector2> {
        new Vector2(10, 10),
        new Vector2(50, 10),
        new Vector2(90, 10)
    };
        currentHearts = maxHearts;
        collisionCount = 0;
        isPlayerDead = false;

        InitializeItemsPositions();

        masterCollisionHandler.HandleCollisions(

            roomManager.GetCurrentRoomItems(),
            roomManager.CurrentRoom.Enemies,
            ProjectileManager.Instance.GetActiveProjectiles(),
            BlockManager.Instance.GetActiveBlocks());


        controllerList.Add(new KeyboardController(this, Link));


    }

    public void HandlePlayerDamage()
    {
        collisionCount++; // Track hits
        Console.WriteLine($"Collision Count: {collisionCount}");

       if (collisionCount % 2 == 0 && currentHearts > 0)
       {
        currentHearts--;
        InitializeHeartPositions(); // Update heart UI
        Console.WriteLine($"Heart lost! Current Hearts: {currentHearts}");
       }
            if (collisionCount >= 6)
            {
                isPlayerDead = true;
                playerRespawnPosition = Link.Instance.Position; // Save respawn point
                respawnTimer = 3f;
                collisionCount = 0;
                currentHearts = maxHearts; // Restore full hearts
                InitializeHeartPositions(); // Update heart UI
                Console.WriteLine("Player is dead! Respawning in 3 seconds.");
            }
    }

    public void HandlePlayerHealed(float amount) {
        currentHearts = Math.Min(currentHearts + amount, maxHearts);
        InitializeHeartPositions();
    }

    private void InitializeHeartPositions()
    {
        heartPositions.Clear();  // Reset positions
        float heartSpacing = 40f;
        Vector2 heartPosition = new Vector2(10, 10);

        for (int i = 0; i < currentHearts; i++)  // Draw only current hearts
        {
            heartPositions.Add(heartPosition);
            heartPosition.X += heartSpacing;
        }
    }

    private void InitializeItemsPositions() {
        bombPosition = new Vector2(10, 360);
        applePosition = new Vector2(10, 400);
        crystalPosition = new Vector2(10, 440);
        multiplicationPositions = new List<Vector2>();
        multiplicationPositions.Add(new Vector2(50,365));
        multiplicationPositions.Add(new Vector2(50, 405));
        multiplicationPositions.Add(new Vector2(50, 445));
    }

    private void RestartGame()
    {
        Initialize();
        _currentGameState = GameState.Playing;
    }

    private void DrawItems() {
        _spriteBatch.Draw(
                itemFactory.GetTexture("Bomb"),
                bombPosition,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                0.15f,
                SpriteEffects.None,
        0f
            );
        _spriteBatch.DrawString(uiFont,Link.Instance.GetItemCount("Bomb").ToString(), new Vector2(90, 360), Color.White);
        _spriteBatch.Draw(
            itemFactory.GetTexture("Crystal"),
            crystalPosition,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            0.15f,
            SpriteEffects.None,
            0f
        );
        _spriteBatch.DrawString(uiFont, Link.Instance.GetItemCount("Apple").ToString(), new Vector2(90, 400), Color.White);
        _spriteBatch.Draw(
            itemFactory.GetTexture("Apple"),
            applePosition,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            0.5f,
            SpriteEffects.None,
            0f
        );
        _spriteBatch.DrawString(uiFont, Link.Instance.CrystalCount.ToString(), new Vector2(90, 440), Color.White);
        for (int i = 0; i < 3; i++) {
            _spriteBatch.Draw(
            multiplicationTexture,
            multiplicationPositions[i],
            null,
            Color.White,
            0f,
            Vector2.Zero,
            1.5f,
            SpriteEffects.None,
            0f
        );
        }
        _spriteBatch.Draw(
            TextureManager.Instance.GetTexture("Item_Slot"),
            new Vector2(120, 365),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            4f,
            SpriteEffects.None,
            0f
        );
        _spriteBatch.Draw(
            TextureManager.Instance.GetTexture("Dark_Background"),
            new Vector2(140, 400),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            4.0f,
            SpriteEffects.None,
            0f
        );
        string name = Link.Instance.CurrentSelectedItemName;
        if (name == "Bomb")
        {
            _spriteBatch.Draw(
            itemFactory.GetTexture(name),
            new Vector2(138, 405),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            0.2f,
            SpriteEffects.None,
            0f
        );
        }
        else if (name == "Apple") {
            _spriteBatch.Draw(
            itemFactory.GetTexture(name),
            new Vector2(138, 405),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            0.8f,
            SpriteEffects.None,
            0f
        );
        }
    }
    protected override void Update(GameTime gameTime)
    {
        if (_currentGameState == GameState.Paused)
        {
            _pauseMenu.Update();
            return;

        }
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        masterCollisionHandler.HandleCollisions(
            roomManager.GetCurrentRoomItems(),
            roomManager.CurrentRoom.Enemies,
            ProjectileManager.Instance.GetActiveProjectiles(),
            BlockManager.Instance.GetActiveBlocks());
        base.Update(gameTime);
        Vector2 linkSize = Link.Instance.GetScaledDimensions();
        roomManager.Update(gameTime); // ✅ This is crucial    


        // Toggle pause only when Tab is pressed once
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyUp(Keys.Tab))
        {
            isPaused = !isPaused;
        }
        previousKeyboardState = keyboardState; // Store state for next frame

        // If paused, do not update game logic
        if (isPaused)
            return;

        foreach (IController controller in controllerList)
        {
            controller.Update();
        }
        // sprite.Update();
        foreach (var item in roomManager.GetCurrentRoomItems())
        {
            item.Update(gameTime);
        }

        EnemyManager.Instance.Update(gameTime);
        ProjectileManager.Instance.Update(gameTime);
        Link.Instance.Update();

        if (isPlayerDead)
        {
            respawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (respawnTimer <= 0)
            {
                isPlayerDead = false;
                collisionCount = 0;
                currentHearts = maxHearts; // Reset to 3 hearts
                Link.Instance.SetPosition(playerRespawnPosition);
                InitializeHeartPositions(); // Refresh heart display
            }
            return;
        }

        base.Update(gameTime);
        roomManager.CheckDoorTransition();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        if (_currentGameState == GameState.Playing)
        {
            roomManager.Draw(_spriteBatch);
            ProjectileManager.Instance.Draw(_spriteBatch);
            BlockManager.Instance.Draw(_spriteBatch);


            var items = roomManager.GetCurrentRoomItems();
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.Draw(_spriteBatch);
                }
            }

            if (Link.Instance != null)
            {
                Link.Instance.Draw(_spriteBatch);
            }
            else
            {
                Console.WriteLine("Error: Link.Instance is null in Draw()!");
            }

            EnemyManager.Instance.Draw(_spriteBatch);
        }
        else if (_currentGameState == GameState.Paused)
        {
            _pauseMenu.Draw(_spriteBatch, GraphicsDevice);
        }

        if (!isPlayerDead)
        {
            for (int i = 0; i < (int)currentHearts; i++)
            {
                _spriteBatch.Draw(heartTexture, heartPositions[i], Color.White);
            }

            if (currentHearts % 1 != 0)
            {
                int halfIndex = (int)currentHearts;
                Rectangle sourceRect = new Rectangle(0, 0, heartTexture.Width / 2, heartTexture.Height);
                _spriteBatch.Draw(heartTexture, heartPositions[halfIndex], sourceRect, Color.White);
            }

            DrawItems();

            Link.Instance.Draw(_spriteBatch); // Draw Link
        }

        if (isPaused)
        {
            string pauseText = "Game Paused\nPress 'Tab' to Resume";
            Vector2 textSize = pauseFont.MeasureString(pauseText);
            Vector2 position = new Vector2(
                (_graphics.PreferredBackBufferWidth - textSize.X) / 2,
                (_graphics.PreferredBackBufferHeight - textSize.Y) / 2);
            _spriteBatch.DrawString(pauseFont, pauseText, position, Color.White);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
}
