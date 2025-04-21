using System.Collections.Generic;
using sprint0Test.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Interfaces;
using sprint0Test.Items;
using sprint0Test.Link1;
using System;
using sprint0Test.Room;
namespace sprint0Test;

public class Game1 : Game
{
    // Game1 Instance
    public static Game1 Instance { get; private set; }

    public enum GameState
    {StartMenu, Playing, Options, Paused, Exiting}
    public GameState _currentGameState = GameState.StartMenu;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _menuFont;

    private PauseMenu _pauseMenu;
    private MenuManager menuManager;
    private Texture2D backgroundTexture;
    public Texture2D spriteTexture;

    List<IController> controllerList = new();
    private ItemFactory itemFactory;
    private RoomManager roomManager;
    private MasterCollisionHandler masterCollisionHandler;
    private Texture2D heartTexture;
    private Texture2D rupeeIcon;
    private List<Vector2> heartPositions = new();
    private int maxHearts = 3, currentHearts = 3, collisionCount = 0;
    int rupeeCount = 0;
    Vector2 rupeePosition = new Vector2(650, 10);
    private bool isPlayerDead;
    private float respawnTimer;
    private Vector2 playerRespawnPosition;

    private bool isPaused;
    private KeyboardState previousKeyboardState;

    public ISprite sprite;
    public List<IItem> itemList;
    public int currentItemIndex;
    private Link Link;
    public IItem currentItem;

    private bool isFirstRun = true;



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
        //controllerList.Add(new MouseController(this));
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

        masterCollisionHandler = new MasterCollisionHandler(); // Initialize the collision handler
        TextureManager.Instance.LoadContent(this);
        heartTexture = Content.Load<Texture2D>("heart");
        rupeeIcon = Content.Load<Texture2D>("green-rupee");

        itemFactory = new ItemFactory();
        LoadItemTextures();
        RegisterItems();

        EnemyManager.Instance.SpawnEnemy();

        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");

        BlockManager.LoadTexture(Content.Load<Texture2D>("TileSetDungeon"));
        //Content.Load<Texture2D>("TileSetDungeon");

        ResetGameState();
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
            Link.Initialize(linkSprite, new Vector2(200, 200));
            isFirstRun = false;
        }
        else
        {
            Link.Reset(linkSprite, new Vector2(200, 200));
        }

        // Reset player stats
        currentHearts = maxHearts;
        isPlayerDead = false;
        collisionCount = 0;
        respawnTimer = 0;
        InitializeHeartPositions();

        // Reset controllers
        controllerList.Clear();
        controllerList.Add(new MouseController(this));
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

    protected override void Update(GameTime gameTime)
    {

        switch (_currentGameState)
        {
        case GameState.StartMenu:
            menuManager.Update(this);
            break;
        case GameState.Playing:
            
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))Exit();

        masterCollisionHandler.HandleCollisions(
        roomManager.GetCurrentRoomItems(),
        roomManager.CurrentRoom.Enemies,
        ProjectileManager.Instance.GetActiveProjectiles(),
        BlockManager.Instance.GetActiveBlocks());
        base.Update(gameTime);
        Vector2 linkSize = Link.Instance.GetScaledDimensions();
        roomManager.Update(gameTime); // This is crucial    

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
            // maybe a placeholder screen
            break;
        case GameState.Paused:
            _pauseMenu.Update();
            break;
        }
        
        base.Update(gameTime);
        roomManager.CheckDoorTransition();
    }

    protected override void Draw(GameTime gameTime)
    {
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
            _spriteBatch.DrawString(_menuFont, "x "+ rupeeCount.ToString(), rupeePosition + new Vector2(rupeeIcon.Width + 5, 0), Color.White);
            var items = roomManager.GetCurrentRoomItems();
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
            break;


        case GameState.Options:
            _spriteBatch.DrawString(_menuFont, "Options Coming Soon", new Vector2(100, 100), Color.White);
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
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
}
