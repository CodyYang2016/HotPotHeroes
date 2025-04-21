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
using System.Linq;
using sprint0Test.Audio;
using System.Reflection;

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
    private ItemFactory itemFactory;
    public List<IItem> itemList;
    public int currentItemIndex;
    private Link Link;

    public IItem currentItem;

    // New Room Manager
    public RoomManager roomManager;
    float roomScale;
    public RoomManager RoomManager => roomManager;

    // New Collision Handler
    private MasterCollisionHandler masterCollisionHandler;


    // Pause-related fields
    private bool isPaused;
    private SpriteFont pauseFont;

    private KeyboardState previousKeyboardState;
    
    //shaders
    Effect Darkness;
    private RenderTarget2D sceneRenderTarget;

    // Code for the new Link Health
    private Texture2D heartTexture;
    private List<Vector2> heartPositions;
    private int collisionCount = 0;
    private int maxHearts = 3;
    private int currentHearts;

    // God Mode
    private bool isGodMode = false;
    private double godModeTimer = 0;    // 用于屏幕提示计时


    private bool isPlayerDead;
    private float respawnTimer;
    private Vector2 playerRespawnPosition;

    //private int collisionCount = 0;
    private int totalHits = 0;
    private int deathCount = 0;
    private bool isGameOver = false;
    private bool isGameWon = false;


    // Minimap Stuff
    private bool showFullMap = false;
    private Texture2D mapTexture;
    private Texture2D dotTexture;
    private Dictionary<string, Point> roomMapPositions;

    public Game1()
    {
        Instance = this; // Set the static instance
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

        GraphicsDeviceHelper.Device = GraphicsDevice;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //spriteTexture = Content.Load<Texture2D>("mario2");
        //sprite = new StandingInPlacePlayerSprite(spriteTexture);

        _menuFont = Content.Load<SpriteFont>("MenuFont");
        _pauseMenu = new PauseMenu(Content.Load<SpriteFont>("MenuFont"));
        _pauseMenu.OnOptionSelected = HandleMenuSelection;
        pauseFont = Content.Load<SpriteFont>("PauseFont"); // Load the font


        //SHaders
        AudioManager.Instance.LoadContent(Content);

        AudioManager.Instance.SetSong(SongList.Dungeon);

        ShaderManager.Instance.LoadContent(Content);
        //Darkness = Content.Load<Effect>("Darkness");

        sceneRenderTarget = new RenderTarget2D(
            GraphicsDevice,
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height);


        masterCollisionHandler = new MasterCollisionHandler(); // Initialize the collision handler


        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");
        TextureManager.Instance.LoadContent(this);
        itemFactory = new ItemFactory();

        // Load BlockManager
        BlockManager.LoadTexture(dungeonTexture);

        //Register Textures
        // Heart working 
        heartTexture = Content.Load<Texture2D>("heart");

        itemFactory.RegisterTexture("Heart", Content.Load<Texture2D>("heart"));
        itemFactory.RegisterTexture("RedPotion", Content.Load<Texture2D>("red-potion"));
        itemFactory.RegisterTexture("BluePotion", Content.Load<Texture2D>("blue-potion"));
        itemFactory.RegisterTexture("GreenPotion", Content.Load<Texture2D>("green-potion"));
        itemFactory.RegisterTexture("RedRupee", Content.Load<Texture2D>("red-rupee"));
        itemFactory.RegisterTexture("BlueRupee", Content.Load<Texture2D>("blue-rupee"));
        itemFactory.RegisterTexture("GreenRupee", Content.Load<Texture2D>("green-rupee"));
        itemFactory.RegisterTexture("Apple", Content.Load<Texture2D>("apple"));
        itemFactory.RegisterTexture("Crystal", Content.Load<Texture2D>("crystal"));
        itemFactory.RegisterTexture("Bomb", Content.Load<Texture2D>("bomb"));

        //itemFactory.RegisterTexture("Boomerang", Content.Load<Texture2D>("boomerang"));

        //Register Item Creation Logic
        itemFactory.RegisterItem("Heart", position => new Heart("Heart", itemFactory.GetTexture("Heart"), position));
        itemFactory.RegisterItem("RedPotion", position => new Potion("RedPotion", itemFactory.GetTexture("RedPotion"), position));
        itemFactory.RegisterItem("BluePotion", position => new Potion("BluePotion", itemFactory.GetTexture("BluePotion"), position));
        itemFactory.RegisterItem("GreenPotion", position => new Potion("GreenPotion", itemFactory.GetTexture("GreenPotion"), position));
        itemFactory.RegisterItem("RedRupee", position => new Rupee("RedRupee", itemFactory.GetTexture("RedRupee"), position));
        itemFactory.RegisterItem("BlueRupee", position => new Rupee("BlueRupee", itemFactory.GetTexture("BlueRupee"), position));
        itemFactory.RegisterItem("GreenRupee", position => new Rupee("GreenRupee", itemFactory.GetTexture("GreenRupee"), position));
        itemFactory.RegisterItem("Apple", position => new Apple("Apple", itemFactory.GetTexture("Apple"), position));
        itemFactory.RegisterItem("Crystal", position => new Crystal("Crystal", itemFactory.GetTexture("Crystal"), position));
        itemFactory.RegisterItem("Bomb", position => new Bomb("Bomb", itemFactory.GetTexture("Bomb"), position));

        //itemFactory.RegisterItem("Boomerang", position => new Boomerang(itemFactory.GetTexture("Boomerang"), position, 1, 8));


        // 6) 初始化房间管理器
        //   原房间尺寸256×176，窗口800×480，计算缩放
        roomScale = Math.Min(800f / 256f, 480f / 176f);
        roomManager = new RoomManager(itemFactory);

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

        var dot = Content.Load<Texture2D>("dot");
        var Map = Content.Load<Texture2D>("Map");
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

        LinkSprite linkSprite = new LinkSprite(linkMap);
        Console.WriteLine("roomItems: " + (roomManager.GetCurrentRoomItems() != null));
        Console.WriteLine("enemies: " + (EnemyManager.Instance.GetActiveEnemy() != null));
        Console.WriteLine("projectiles: " + (ProjectileManager.Instance.GetActiveProjectiles() != null));
        Console.WriteLine("blocks: " + (BlockManager.Instance.GetActiveBlocks() != null));
        Link.Initialize(linkSprite, new Vector2(200, 200), roomManager);
        heartPositions = new List<Vector2> {
        new Vector2(10, 10),
        new Vector2(50, 10),
        new Vector2(90, 10)
    };
        currentHearts = maxHearts;
        collisionCount = 0;
        isPlayerDead = false;

        masterCollisionHandler.HandleCollisions(

            roomManager.GetCurrentRoomItems(),
            roomManager.CurrentRoom.Enemies,
            ProjectileManager.Instance.GetActiveProjectiles(),
            BlockManager.Instance.GetActiveBlocks());


        controllerList.Add(new KeyboardController(this, Link));

        //Minimap STuff
        mapTexture = Map;
        dotTexture = dot;

        roomMapPositions = new Dictionary<string, Point>
        {
            ["r1b"] = new Point(70, 270),
            ["r1c"] = new Point(120, 270),
            ["r1d"] = new Point(170, 270),
            ["r2c"] = new Point(120, 230),
            ["r3b"] = new Point(70, 170),
            ["r3c"] = new Point(120, 170),
            ["r3d"] = new Point(170, 170),
            ["r4a"] = new Point(20, 120),
            ["r4b"] = new Point(70, 120),
            ["r4c"] = new Point(120, 120),
            ["r4d"] = new Point(170, 120),
            ["r4e"] = new Point(220, 120),
            ["r5c"] = new Point(120, 70),
            ["r5e"] = new Point(220, 70),
            ["r5f"] = new Point(270, 70),
            ["r6b"] = new Point(70, 20),
            ["r6c"] = new Point(120, 20),
            // ❌ exclude r8c (horde)
        };



    }

    public void ToggleGodMode()
    {
        isGodMode = !isGodMode;
        Link.Instance.IsInvulnerable = isGodMode;

        // 倍增或恢复移动速度（私有字段 speed）
        float baseSpeed = 2f, godSpeed = baseSpeed * 2;
        typeof(Link)
          .GetField("speed", BindingFlags.NonPublic | BindingFlags.Instance)
          .SetValue(Link.Instance, isGodMode ? godSpeed : baseSpeed);

        godModeTimer = 3.0;  // 提示持续 3 秒
    }


    public void HandlePlayerDamage()
    {
        collisionCount++;
        totalHits++;
        Console.WriteLine($"Collision Count: {collisionCount}, Total Hits: {totalHits}");

        TryRemoveHeart();
        CheckDeath();
        CheckGameOver();
    }

    private void TryRemoveHeart()
    {
        if (collisionCount % 2 == 0 && currentHearts > 0)
        {
            currentHearts--;
            InitializeHeartPositions(); // Update heart UI
            Console.WriteLine($"Heart lost! Current Hearts: {currentHearts}");
        }
    }

    private void CheckDeath()
    {
        if (collisionCount >= 6 && !isPlayerDead)
        {
            isPlayerDead = true;
            playerRespawnPosition = Link.Instance.Position;
            respawnTimer = 2f;
            currentHearts = maxHearts;
            InitializeHeartPositions();

            Console.WriteLine("Player is dead! Respawning in 2 seconds.");

            collisionCount = 0;
            deathCount++;
        }
    }

    private void CheckGameOver()
    {
        if (deathCount >= 2 && !isGameOver)
        {
            isGameOver = true;
            Console.WriteLine("Game Over: You Lose!");
        }
    }




    // Heart Helper Methods
    private void InitializeHeartPositions()
    {
        heartPositions.Clear();  // Reset positions
        float heartSpacing = 30f;
        Vector2 heartPosition = new Vector2(10, 10);

        for (int i = 0; i < currentHearts; i++)  // Draw only current hearts
        {
            heartPositions.Add(heartPosition);
            heartPosition.X += heartSpacing;
        }
    }

    private void HandleMouseTeleportation()
    {
        MouseState mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            Link.Instance.SetPosition(mousePosition);

            Console.WriteLine($"Teleported player to {mousePosition}");
        }
    }

    private void HandleMenuSelection(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0: // Resume
                _currentGameState = GameState.Playing;
                break;
            case 1: // Restart
                RestartGame();
                break;
            case 2: // Quit
                Exit();
                break;
        }
    }
    private void RestartGame()
    {
        Initialize();
        _currentGameState = GameState.Playing;
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

        // If paused, do not update game logic
        if (isPaused)
            return;

        // Toggle pause only when Tab is pressed once
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyUp(Keys.Tab) && !isGameOver && !isGameWon)
        {
            isPaused = !isPaused;
        }
        previousKeyboardState = keyboardState; // Store state for next frame

        // If paused, do not update game logic
        if (isPaused || isGameOver || isGameWon)
            return;

        roomManager.Update(gameTime); // ✅ This is crucial

        if (!isGameWon && roomManager.CurrentRoom != null && roomManager.CurrentRoom.RoomID == "r5e")
        {
            // Check if Aquamentus has been defeated
            var aquamentusStillExists = roomManager.CurrentRoom.Enemies
                .OfType<Aquamentus>()
                .Any();

            if (!aquamentusStillExists)
            {
                isGameWon = true;
            }
        }

        foreach (IController controller in controllerList)
        {
            controller.Update();
        }
        // sprite.Update();
        foreach (var item in roomManager.GetCurrentRoomItems())
        {
            item.Update(gameTime);
        }
        ProjectileManager.Instance.Update(gameTime);
        //Sprint5 update link's damage
        Link.Instance.Update(gameTime);
        roomManager.Update(gameTime); // ✅ This is crucial    



        // Player Dead Animation
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

        HandleMouseTeleportation();

        base.Update(gameTime);
        roomManager.CheckDoorTransition();
    }

    protected override void Draw(GameTime gameTime)
    {
        //shaders
        GraphicsDevice.SetRenderTarget(sceneRenderTarget);
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        if (_currentGameState == GameState.Playing)
        {
            roomManager.Draw(_spriteBatch);
            BlockManager.Instance.Draw(_spriteBatch);
            ProjectileManager.Instance.Draw(_spriteBatch);


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
        }
        else if (_currentGameState == GameState.Paused)
        {
            _pauseMenu.Draw(_spriteBatch, GraphicsDevice);
        }

        if (!isPlayerDead)
        {
            for (int i = 0; i < currentHearts; i++)
            {
                _spriteBatch.Draw(heartTexture, heartPositions[i], Color.White);
                Console.WriteLine($"Drawing heart at position {heartPositions[i]}");
            }

            Link.Instance.Draw(_spriteBatch);
        }

        // 显示 God Mode 提示文字
        if (godModeTimer > 0)
        {
            string msg = isGodMode
                ? "GOD MODE ACTIVATED!"
                : "GOD MODE Closed!";
            Vector2 size = _menuFont.MeasureString(msg);
            Vector2 pos = new Vector2(
                (_graphics.PreferredBackBufferWidth - size.X) / 2,
                20);
            _spriteBatch.DrawString(_menuFont, msg, pos, Color.Yellow);
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

        // ✅ ✅ ✅ Move minimap/fullmap drawing *before* End()
        if (showFullMap)
        {
            _spriteBatch.Draw(mapTexture, new Rectangle(0, 0, 800, 600), Color.White);

            if (roomMapPositions.TryGetValue(roomManager.CurrentRoom.RoomID, out Point mapPos))
            {
            }
        }
        else if (roomManager.CurrentRoom.RoomID != "r8c")
        {
            _spriteBatch.Draw(mapTexture, new Rectangle(650, 380, 100, 100), Color.White);

            if (roomMapPositions.TryGetValue(roomManager.CurrentRoom.RoomID, out Point mapPos))
            {
                int scaledX = (int)(mapPos.X * 100f / 300f);
                int scaledY = (int)(mapPos.Y * 100f / 300f);
                _spriteBatch.Draw(dotTexture, new Rectangle(650 + scaledX, 380 + scaledY, 3, 3), Color.Red);
            }
        }

        if (isGameOver)
        {
            string loseMessage = "You lose!\nPress 'Esc' to quit";
            Vector2 size = pauseFont.MeasureString(loseMessage);
            Vector2 center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            Vector2 position = center - (size / 2);

            _spriteBatch.DrawString(pauseFont, loseMessage, position, Color.White);
        }

        if (isGameWon)
        {
            string winMessage = "You win!\nPress 'Esc' to quit";
            Vector2 size = pauseFont.MeasureString(winMessage);
            Vector2 center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            Vector2 position = center - (size / 2);

            _spriteBatch.DrawString(pauseFont, winMessage, position, Color.White);
        }
        _spriteBatch.End(); // ✅ End only after all drawing

        //shaders
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        ShaderManager.ApplyShading(
            _spriteBatch,
            sceneRenderTarget,
            GraphicsDevice
        );


        base.Draw(gameTime);
    }

    //minimap command
    public void ToggleFullMap()
    {
        showFullMap = !showFullMap;
    }



}
