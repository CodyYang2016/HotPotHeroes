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
using sprint0Test.Audio;
using System.Reflection;
using System.Linq;
namespace sprint0Test;

public class Game1 : Game
{
    // Game1 Instance
    public static Game1 Instance { get; private set; }

    public enum GameState
    { StartMenu, Playing, Options, Paused, Exiting }
    public GameState _currentGameState = GameState.StartMenu;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public Texture2D spriteTexture;
    List<IController> controllerList;
    public ISprite sprite;
    private SpriteFont _menuFont;
    private SpriteFont uiFont;
    private SpriteFont lifeFont;
    private ItemFactory itemFactory;
    public List<IItem> itemList;
    public int currentItemIndex;
    private Link Link;
    public IItem currentItem;

    //HUD Height
    public const int HudHeight = 160;
    public const int RoomHeight = 480;
    public const int RoomWidth = 735;
    //Start Menu
    private Texture2D backgroundTexture;
    private bool isFirstRun = true;
    private String OptionsText = "WASD to Move \nSpace to Attack\nLeft Shift to Dash\nP to Pause\nM to toggle full Map\nU and I to switch between\nusable items\nR to cycle between weapons\n\nESC (Go back)";
    private MenuManager menuManager;

    // New Room Manager
    public RoomManager roomManager;
    public RoomManager RoomManager => roomManager;
    //float roomScale;


    // New Collision Handler
    private MasterCollisionHandler masterCollisionHandler;


    // Pause-related fields
    private bool isPaused;
    private PauseMenu _pauseMenu;

    private KeyboardState previousKeyboardState;

    //shaders
    Effect Darkness;
    private RenderTarget2D sceneRenderTarget;

    //Code for HUD
    private Texture2D heartTexture;
    private int collisionCount;
    private float maxHearts = 3f;
    private float currentHearts;
    private Texture2D multiplicationTexture;
    private List<Vector2> heartPositions = new();
    private List<Vector2> multiplicationPositions;
    private Vector2 bombPosition;
    private Vector2 applePosition;
    private Vector2 crystalPosition;

    // —— God Mode 状态 —— 
    private bool isGodMode = false;
    private double godModeTimer = 0;    // 用于屏幕提示计时
    private double refillTimer = 0;
    private const double RefillDuration = 2.0;

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
        Instance = this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        // 设置窗口尺寸 800x480
        _graphics.PreferredBackBufferWidth = 735;
        //_graphics.PreferredBackBufferHeight = 480;
        _graphics.PreferredBackBufferHeight = 640;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        controllerList = new List<IController>();
        GraphicsDeviceHelper.Device = GraphicsDevice;
        Bomb.OnBombPlanted += (sender, args) =>
        {
            roomManager.CurrentRoom.Items.Add(args.Bomb);
        };
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _menuFont = Content.Load<SpriteFont>("MenuFont");
        _pauseMenu = new PauseMenu(Content.Load<SpriteFont>("MenuFont"));
        _pauseMenu.OnOptionSelected = HandleMenuSelection;
        backgroundTexture = Content.Load<Texture2D>("StartScreen");
        menuManager = new MenuManager(_menuFont, backgroundTexture);

        //SHaders
        AudioManager.Instance.LoadContent(Content);
        AudioManager.Instance.SetSong(SongList.Title);
        SoundManager.Instance.LoadContent(Content);
        ShaderManager.Instance.LoadContent(Content);

        sceneRenderTarget = new RenderTarget2D(
            GraphicsDevice,
            RoomWidth,
            RoomHeight
        );

        masterCollisionHandler = new MasterCollisionHandler(); // Initialize the collision handler
        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");
        TextureManager.Instance.LoadContent(this);
        heartTexture = Content.Load<Texture2D>("heart");
        EnemyManager.Instance.SpawnEnemy();

        itemFactory = new ItemFactory();
        LoadItemTextures();
        RegisterItems();

        ItemManager.Initialize(itemFactory);
        ItemManager.Instance.LoadFromCSV("Content/room_items.csv");
        // Load BlockManager
        BlockManager.LoadTexture(dungeonTexture);

        ResetGameState();
        InitializeItemsPositions();
    }

    private void LoadItemTextures()
    {
        multiplicationTexture = Content.Load<Texture2D>("multiplication");
        uiFont = Content.Load<SpriteFont>("UIFont");
        lifeFont = Content.Load<SpriteFont>("LIFEFont");
        itemFactory.RegisterTexture("Heart", Content.Load<Texture2D>("heart"));
        itemFactory.RegisterTexture("Apple", Content.Load<Texture2D>("apple"));
        itemFactory.RegisterTexture("Crystal", Content.Load<Texture2D>("crystal"));
        itemFactory.RegisterTexture("Bomb", Content.Load<Texture2D>("bomb"));
        itemFactory.RegisterTexture("Bow", Content.Load<Texture2D>("bow"));
        itemFactory.RegisterTexture("Sword", Content.Load<Texture2D>("sword"));
    }

    private void RegisterItems()
    {
        itemFactory.RegisterItem("Bow", position => new Bow("Bow", itemFactory.GetTexture("Bow"), position));
        itemFactory.RegisterItem("Sword", position => new Sword("Sword", itemFactory.GetTexture("Sword"), position));
        itemFactory.RegisterItem("Heart", position => new Heart("Heart", itemFactory.GetTexture("Heart"), position));
        itemFactory.RegisterItem("Apple", position => new Apple("Apple", itemFactory.GetTexture("Apple"), position));
        itemFactory.RegisterItem("Crystal", position => new Crystal("Crystal", itemFactory.GetTexture("Crystal"), position));
        itemFactory.RegisterItem("Bomb", position => new Bomb("Bomb", itemFactory.GetTexture("Bomb"), position));
    }

    private void ResetGameState()
    {
        // Clear managers if necessary
        EnemyManager.Instance.Clear();
        ProjectileManager.Instance.Clear();
        BlockManager.Instance.Clear();

        // Reset room and enemies
        roomManager = new RoomManager(itemFactory);
        EnemyManager.Instance.SpawnEnemy();

        // Reset Link
        var linkSprite = new LinkSprite(LinkSprite.CreateDefaultSpriteMap(Content));

        if (isFirstRun)
        {
            Link.Initialize(linkSprite, new Vector2(200, 200));
            Link.Instance.AddItem(new Sword("Sword", itemFactory.GetTexture("Sword"), new Vector2(0, 0)));
            isFirstRun = false;
        }
        else
        {
            Link.Reset(linkSprite, new Vector2(200, 200));
            Link.Instance.AddItem(new Sword("Sword", itemFactory.GetTexture("Sword"), new Vector2(0, 0)));
        }

        //Minimap STuff
        var dot = Content.Load<Texture2D>("dot");
        var Map = Content.Load<Texture2D>("Map");
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

        // Reset player stats
        currentHearts = maxHearts;
        isPlayerDead = false;
        collisionCount = 0;
        respawnTimer = 0;
        InitializeHeartPositions();

        // Reset controllers
        controllerList.Clear();
        controllerList.Add(new KeyboardController(this, Link));
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

    public void RefillHealth()
    {
        // 重置碰撞计数
        collisionCount = 0;
        // 满血
        currentHearts = maxHearts;
        // 重新计算心心位置
        InitializeHeartPositions();

        //如果之前因碰撞落到死亡状态，要复活
        isPlayerDead = false;

        // 启动提示计时
        refillTimer = RefillDuration;

        // 日志输出，方便调试
        Console.WriteLine($"[Cheat] RefillHealth called. currentHearts={currentHearts}, isPlayerDead={isPlayerDead}");
    }

    public void HandlePlayerDamage()
    {
        SoundManager.Instance.PlaySound(SoundList.scream);

        collisionCount++; // Track hits
        totalHits++;
        Console.WriteLine($"Collision Count: {collisionCount}");

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

    public void HandlePlayerHealed(float amount) {
        currentHearts = Math.Min(currentHearts + amount, maxHearts);
        InitializeHeartPositions();
    }

    private void InitializeHeartPositions()
    {
        heartPositions.Clear();  // Reset positions
        float heartSpacing = 40f;
        Vector2 heartPosition = new Vector2(580, 110);

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
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y - HudHeight);

            Link.Instance.SetPosition(mousePosition);

            Console.WriteLine($"Teleported player to {mousePosition}");
        }
    }

    public void ChangeGameState(GameState newState)
    {
        _currentGameState = newState;
        if (newState == GameState.Exiting)
            Exit();
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

    //minimap command
    public void ToggleFullMap()
    {
        showFullMap = !showFullMap;
    }

    private void InitializeItemsPositions() {
        bombPosition = new Vector2(10, 20);
        applePosition = new Vector2(10, 60);
        crystalPosition = new Vector2(10, 100);
        multiplicationPositions = new List<Vector2>();
        multiplicationPositions.Add(new Vector2(50, 25));
        multiplicationPositions.Add(new Vector2(50, 65));
        multiplicationPositions.Add(new Vector2(50, 105));
    }

    private void DrawItems()
    {
        // Static item icons and counts on the left
        _spriteBatch.Draw(itemFactory.GetTexture("Bomb"), bombPosition, null, Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0f);
        _spriteBatch.DrawString(uiFont, Link.Instance.GetItemCount("Bomb").ToString(), new Vector2(90, 20), Color.White);

        _spriteBatch.Draw(itemFactory.GetTexture("Crystal"), crystalPosition, null, Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0f);
        _spriteBatch.DrawString(uiFont, Link.Instance.GetItemCount("Apple").ToString(), new Vector2(90, 60), Color.White);
        _spriteBatch.DrawString(lifeFont, "-LIFE-", new Vector2(580, 30), Color.Red);
        _spriteBatch.Draw(itemFactory.GetTexture("Apple"), applePosition, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        _spriteBatch.DrawString(uiFont, Link.Instance.CrystalCount.ToString(), new Vector2(90, 100), Color.White);

        for (int i = 0; i < 3; i++)
        {
            _spriteBatch.Draw(multiplicationTexture, multiplicationPositions[i], null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
        }

        // Slot A for weapon
        _spriteBatch.Draw(TextureManager.Instance.GetTexture("Item_Slot_A"), new Vector2(265, 20), null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

        // Slot B for item
        _spriteBatch.Draw(TextureManager.Instance.GetTexture("Item_Slot_B"), new Vector2(355, 20), null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

        // Background for item icons
        _spriteBatch.Draw(TextureManager.Instance.GetTexture("Dark_Background"), new Vector2(280, 55), null, Color.White, 0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0f);
        _spriteBatch.Draw(TextureManager.Instance.GetTexture("Dark_Background"), new Vector2(375, 55), null, Color.White, 0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0f);

        // Draw current weapon in Slot A
        IItem weapon = Link.Instance.GetCurrentWeapon();
        if (weapon != null)
        {
            float scale = weapon.name == "Bow" ? 0.25f : 0.2f;
            Vector2 pos = weapon.name == "Sword" ? new Vector2(285, 50) : new Vector2(285, 60);
            _spriteBatch.Draw(itemFactory.GetTexture(weapon.name), pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        // Draw current item in Slot B
        IItem item = Link.Instance.GetCurrentItem();
        if (item != null)
        {
            float scale = item.name == "Apple" ? 0.8f : 0.2f;
            Vector2 pos = item.name == "Apple" ? new Vector2(375, 60) : new Vector2(373, 60);
            _spriteBatch.Draw(itemFactory.GetTexture(item.name), pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
    protected override void Update(GameTime gameTime)
    {
        switch (_currentGameState)
        {
            case GameState.StartMenu:
                menuManager.Update(this);
                break;
            case GameState.Playing:

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

                masterCollisionHandler.HandleCollisions(
                roomManager.GetCurrentRoomItems(),
                roomManager.CurrentRoom.Enemies,
                ProjectileManager.Instance.GetActiveProjectiles(),
                BlockManager.Instance.GetActiveBlocks());
                base.Update(gameTime);
                Vector2 linkSize = Link.Instance.GetScaledDimensions();
                //roomManager.Update(gameTime); // This is crucial    

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
                EnemyManager.Instance.Update(gameTime);
                ProjectileManager.Instance.Update(gameTime);
                Link.Instance.Update(gameTime);

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
                break;
            case GameState.Options:
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    _currentGameState = Game1.GameState.StartMenu;
                }
                break;
            case GameState.Paused:
                _pauseMenu.Update();
                break;
        }

        HandleMouseTeleportation();
        base.Update(gameTime);
        roomManager.CheckDoorTransition();
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_currentGameState == GameState.Playing) { GraphicsDevice.SetRenderTarget(sceneRenderTarget); }
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        switch (_currentGameState)
            {
                case GameState.StartMenu:
                    menuManager.Draw(_spriteBatch, GraphicsDevice);
                    break;
                case GameState.Paused:
                    _pauseMenu.Draw(_spriteBatch, GraphicsDevice);
                    break;
                case GameState.Playing:
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

                    EnemyManager.Instance.Draw(_spriteBatch);
                    break;
                case GameState.Options:
                    _spriteBatch.DrawString(_menuFont, OptionsText, new Vector2(250, 150), Color.White);
                    break;
            }
        _spriteBatch.End();
        if (_currentGameState == GameState.Playing)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(sceneRenderTarget, new Vector2(0, HudHeight), Color.White);
            _spriteBatch.End();

            ShaderManager.ApplyShading(
                _spriteBatch,
                sceneRenderTarget,
                GraphicsDevice
            );

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
            _spriteBatch.Draw(
                TextureManager.Instance.GetTexture("Dark_Background"),
                new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, HudHeight),
                Color.White
            );

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
                Texture2D debugPixel = new Texture2D(GraphicsDevice, 1, 1);
                debugPixel.SetData(new[] { Color.Red });
            }

            if (showFullMap)
            {
                _spriteBatch.Draw(mapTexture, new Rectangle(0, 0, 750, 640), Color.White);

                if (roomMapPositions.TryGetValue(roomManager.CurrentRoom.RoomID, out Point mapPos))
                {
                }
            }
            else if (roomManager.CurrentRoom.RoomID != "r8c")
            {
                _spriteBatch.Draw(mapTexture, new Rectangle(650, 380 + HudHeight, 100, 100), Color.White);

                if (roomMapPositions.TryGetValue(roomManager.CurrentRoom.RoomID, out Point mapPos))
                {
                    int scaledX = (int)(mapPos.X * 100f / 300f);
                    int scaledY = (int)(mapPos.Y * 100f / 300f);
                    _spriteBatch.Draw(dotTexture, new Rectangle(650 + scaledX, 380 + HudHeight + scaledY, 3, 3), Color.Red);
                }
            }

            if (isPaused)
            {
                string pauseText = "Game Paused\nPress 'Tab' to Resume";
                Vector2 textSize = _menuFont.MeasureString(pauseText);
                Vector2 position = new Vector2(
                    (_graphics.PreferredBackBufferWidth - textSize.X) / 2,
                    (_graphics.PreferredBackBufferHeight - textSize.Y) / 2);
                _spriteBatch.DrawString(_menuFont, pauseText, position, Color.White);
            }

            if (isGameOver)
            {
                string loseMessage = "You lose!\nPress 'Esc' to quit";
                Vector2 size = _menuFont.MeasureString(loseMessage);
                Vector2 center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                Vector2 position = center - (size / 2);

                _spriteBatch.DrawString(_menuFont, loseMessage, position, Color.White);
            }

            if (isGameWon)
            {
                AudioManager.Instance.SetSong(SongList.Title);

                string winMessage = "You win!\nPress 'Esc' to quit";
                Vector2 size = _menuFont.MeasureString(winMessage);
                Vector2 center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                Vector2 position = center - (size / 2);

                _spriteBatch.DrawString(_menuFont, winMessage, position, Color.White);
            }

            _spriteBatch.End();


        }

        base.Draw(gameTime);
    }

}