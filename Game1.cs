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
    { StartMenu, Playing, Options, Paused, Exiting }
    public GameState _currentGameState = GameState.StartMenu;

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

    //Start Menu
    private Texture2D backgroundTexture;
    private bool isFirstRun = true;
    private String OptionsText = "WASD to Move \nSpace to Attack\nLeft Shift to Dash\nP to Pause\n\nESC (Go back)";
    private MenuManager menuManager;

    // New Room Manager
    public RoomManager roomManager;
    //float roomScale;
    public RoomManager RoomManager => roomManager;

    // New Collision Handler
    private MasterCollisionHandler masterCollisionHandler;


    // Pause-related fields
    private PauseMenu _pauseMenu;
    private bool isPaused;

    private KeyboardState previousKeyboardState;

    //shaders
    Effect Darkness;
    private RenderTarget2D sceneRenderTarget;

    // Code for the HUD
    private Texture2D rupeeIcon;
    int rupeeCount = 0;
    private Texture2D heartTexture;
    private List<Vector2> heartPositions = new();
    Vector2 rupeePosition = new Vector2(650, 10);
    private int collisionCount = 0;
    private int maxHearts = 3;
    private int currentHearts;

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
        GraphicsDeviceHelper.Device = GraphicsDevice;
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
        ShaderManager.Instance.LoadContent(Content);

        sceneRenderTarget = new RenderTarget2D(
            GraphicsDevice,
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height);


        masterCollisionHandler = new MasterCollisionHandler(); // Initialize the collision handler
        TextureManager.Instance.LoadContent(this);
        heartTexture = Content.Load<Texture2D>("heart");
        rupeeIcon = Content.Load<Texture2D>("green-rupee");

        itemFactory = new ItemFactory();

        LoadItemTextures();
        RegisterItems();

        EnemyManager.Instance.SpawnEnemy();

        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");

        // Load BlockManager
        BlockManager.LoadTexture(Content.Load<Texture2D>("TileSetDungeon"));

        ResetGameState();
        //roomScale = Math.Min(800f / 256f, 480f / 176f);
        //roomManager = new RoomManager(itemFactory);
    }

    private void LoadItemTextures()
    {
        itemFactory.RegisterTexture("Heart", heartTexture);
        itemFactory.RegisterTexture("RedPotion", Content.Load<Texture2D>("red-potion"));
        itemFactory.RegisterTexture("BluePotion", Content.Load<Texture2D>("blue-potion"));
        itemFactory.RegisterTexture("GreenPotion", Content.Load<Texture2D>("green-potion"));
        itemFactory.RegisterTexture("RedRupee", Content.Load<Texture2D>("red-rupee"));
        itemFactory.RegisterTexture("BlueRupee", Content.Load<Texture2D>("blue-rupee"));
        itemFactory.RegisterTexture("GreenRupee", Content.Load<Texture2D>("green-rupee"));
        itemFactory.RegisterTexture("Apple", Content.Load<Texture2D>("apple"));
        itemFactory.RegisterTexture("Crystal", Content.Load<Texture2D>("crystal"));
    }
    private void RegisterItems()
    {
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
            Link.Initialize(linkSprite, new Vector2(200, 200), roomManager);
            isFirstRun = false;
        }
        else
        {
            Link.Reset(linkSprite, new Vector2(200, 200));
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

    // 立即恢复满血，并启动提示倒计时
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
                ProjectileManager.Instance.Draw(_spriteBatch);
                BlockManager.Instance.Draw(_spriteBatch);

                _spriteBatch.Draw(rupeeIcon, rupeePosition, Color.White);
                _spriteBatch.DrawString(_menuFont, "x " + rupeeCount.ToString(), rupeePosition + new Vector2(rupeeIcon.Width + 5, 0), Color.White);
                var items = roomManager.GetCurrentRoomItems();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        item.Draw(_spriteBatch);
                    }
                }
                if (!isPlayerDead)
                {
                    // Draw hearts based on currentHearts
                    for (int i = 0; i < currentHearts; i++)
                    {
                        _spriteBatch.Draw(heartTexture, heartPositions[i], Color.White);
                        Console.WriteLine($"Drawing heart at position {heartPositions[i]}"); // Debugging heart drawing
                    }

                    Link.Instance.Draw(_spriteBatch); // Draw Link
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

                break;
            case GameState.Options:
                _spriteBatch.DrawString(_menuFont, OptionsText, new Vector2(250, 150), Color.White);
                break;
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
            string winMessage = "You win!\nPress 'Esc' to quit";
            Vector2 size = _menuFont.MeasureString(winMessage);
            Vector2 center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            Vector2 position = center - (size / 2);

            _spriteBatch.DrawString(_menuFont, winMessage, position, Color.White);
        }

        _spriteBatch.End();

        //shaders
        if (_currentGameState == GameState.Playing)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ShaderManager.ApplyShading(
                _spriteBatch,
                sceneRenderTarget,
                GraphicsDevice
            );


        }

        base.Draw(gameTime);
    }

}
