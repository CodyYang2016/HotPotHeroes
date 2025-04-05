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
using sprint0Test.Projectiles;
namespace sprint0Test;

public class Game1 : Game
{
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
    private BlockSprites blockSprites;
    private ItemFactory itemFactory;
    public List<IItem> itemList;
    public int currentItemIndex;
    private Link Link;

    public IItem currentItem;

    RoomManager roomManager;
    float roomScale;

    private PlayerBlockCollisionHandler playerBlockCollisionHandler;
    private PlayerEnemyCollisionHandler playerEnemyCollisionHandler;
    private PlayerItemCollisionHandler playerItemCollisionHandler;
    private EnemyBlockCollisionHandler enemyBlockCollisionHandler;
    private PlayerProjectileCollisionHandler playerProjectileCollisionHandler;
    private ProjectileBlockCollisionHandler projectileBlockCollisionHandler;
    private ProjectileEnemyCollisionHandler projectileEnemyCollisionHandler;

    // Pause-related fields
    private bool isPaused;
    private SpriteFont pauseFont;

    private KeyboardState previousKeyboardState;

    private List<IProjectile> enemyProjectiles = new List<IProjectile>();

    private Texture2D heartTexture;
    private List<Vector2> heartPositions;
    private int collisionCount;
    private int maxHearts = 3;
    private int currentHearts;
    private bool isPlayerDead;
    private float respawnTimer;
    private Vector2 playerRespawnPosition;

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
        heartTexture = Content.Load<Texture2D>("heart");
        heartPositions = new List<Vector2>
    {
        new Vector2(10, 10),
        new Vector2(50, 10),
        new Vector2(90, 10)
    };
        currentHearts = maxHearts;
        collisionCount = 0;
        isPlayerDead = false;



        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");
        blockSprites = new BlockSprites(dungeonTexture);
        TextureManager.Instance.LoadContent(this);
        EnemyManager.Instance.SpawnEnemy();
        itemFactory = new ItemFactory();

        //Register Textures
        itemFactory.RegisterTexture("Heart", Content.Load<Texture2D>("heart"));
        itemFactory.RegisterTexture("RedPotion", Content.Load<Texture2D>("red-potion"));
        itemFactory.RegisterTexture("BluePotion", Content.Load<Texture2D>("blue-potion"));
        itemFactory.RegisterTexture("GreenPotion", Content.Load<Texture2D>("green-potion"));
        itemFactory.RegisterTexture("RedRupee", Content.Load<Texture2D>("red-rupee"));
        itemFactory.RegisterTexture("BlueRupee", Content.Load<Texture2D>("blue-rupee"));
        itemFactory.RegisterTexture("GreenRupee", Content.Load<Texture2D>("green-rupee"));
        itemFactory.RegisterTexture("Apple", Content.Load<Texture2D>("apple"));
        itemFactory.RegisterTexture("Crystal", Content.Load<Texture2D>("crystal"));
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

        //itemFactory.RegisterItem("Boomerang", position => new Boomerang(itemFactory.GetTexture("Boomerang"), position, 1, 8));


        // 6) 初始化房间管理器
        //   原房间尺寸256×176，窗口800×480，计算缩放
        roomScale = Math.Min(800f / 256f, 480f / 176f);
        roomManager = new RoomManager(dungeonTexture, roomScale, itemFactory);

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

        LinkSprite linkSprite = new LinkSprite(linkMap);

        playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
        playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
        playerItemCollisionHandler = new PlayerItemCollisionHandler();
        enemyBlockCollisionHandler = new EnemyBlockCollisionHandler();
        playerProjectileCollisionHandler = new PlayerProjectileCollisionHandler();
        projectileBlockCollisionHandler = new ProjectileBlockCollisionHandler();
        projectileEnemyCollisionHandler = new ProjectileEnemyCollisionHandler();

        // Link = new Link(linkSprite, new Vector2(200, 200));
        Link.Initialize(linkSprite, new Vector2(200, 200));

        controllerList.Add(new KeyboardController(this, Link, blockSprites));


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

        //foreach (IController controller in controllerList)
        //{
            //controller.Update();
        //}
        // sprite.Update();
        //foreach (var item in roomManager.GetCurrentRoomItems())
        //{
            //item.Update(gameTime);
        //}

        //blockSprites.UpdateActiveBlocks(); // Call to update active blocks

        //EnemyManager.Instance.Update(gameTime);
        //ProjectileManager.Instance.Update(gameTime);
        //Link.Instance.Update();

        playerBlockCollisionHandler.HandleCollisionList(blockSprites._active);
        playerEnemyCollisionHandler.HandleCollision(EnemyManager.Instance.GetActiveEnemy());
        playerItemCollisionHandler.HandleCollisionList(roomManager.GetCurrentRoomItems());
        enemyBlockCollisionHandler.HandleCollisionList(blockSprites._active, EnemyManager.Instance.GetActiveEnemy());
        playerProjectileCollisionHandler.HandleCollisionList(ProjectileManager.Instance.GetActiveProjectiles());
        projectileBlockCollisionHandler.HandleCollisionList(blockSprites._active, ProjectileManager.Instance.GetActiveProjectiles());
        projectileEnemyCollisionHandler.HandleCollisionList(EnemyManager.Instance.GetActiveEnemy(), ProjectileManager.Instance.GetActiveProjectiles());

        base.Update(gameTime);
        Vector2 linkSize = Link.Instance.GetScaledDimensions();


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

        blockSprites.UpdateActiveBlocks(); // Call to update active blocks
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

        if (roomManager.IsLinkAtDoor(Link.Instance.Position, linkSize))
        {
            // Get mouse state
            MouseState mouseState = Mouse.GetState();

            // Move to the next room only if the player left-clicks
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                roomManager.NextRoom();
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        if (_currentGameState == GameState.Playing)
        {
            roomManager.DrawRoom(_spriteBatch);
            ProjectileManager.Instance.Draw(_spriteBatch); // Ensure this is present

            // sprite.Draw(_spriteBatch);
            var items = roomManager.GetCurrentRoomItems();
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.Draw(_spriteBatch);
                }
            }
            if (Link.Instance != null)  // Prevents crash if Link wasn't initialized
            {
                Link.Instance.Draw(_spriteBatch);
            }
            else
            {
                Console.WriteLine("Error: Link.Instance is null in Draw()!");
            }
            blockSprites.DrawActiveBlocks(_spriteBatch); // Call to draw active blocks
            EnemyManager.Instance.Draw(_spriteBatch);
        }
        else if (_currentGameState == GameState.Paused)
        {
            _pauseMenu.Draw(_spriteBatch, GraphicsDevice);
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

        // Draw pause message if paused
        if (isPaused)
        {
            string pauseText = "Game Paused\nPress 'Tab' to Resume";
            Vector2 textSize = pauseFont.MeasureString(pauseText);
            Vector2 position = new Vector2((_graphics.PreferredBackBufferWidth - textSize.X) / 2,
                                           (_graphics.PreferredBackBufferHeight - textSize.Y) / 2);
            _spriteBatch.DrawString(pauseFont, pauseText, position, Color.White);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }

}
