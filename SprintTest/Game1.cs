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
using sprint0Test.Audio;

namespace sprint0Test;

public class Game1 : Game
{
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
    RoomManager roomManager;
    float roomScale;


    // New Collision Handler
    private MasterCollisionHandler masterCollisionHandler;


    // Pause-related fields
    private bool isPaused;
    private SpriteFont pauseFont;

    private KeyboardState previousKeyboardState;

    Effect Darkness;
    private RenderTarget2D sceneRenderTarget;

    /*
    private int playerCollisionCount = 0;
    private float respawnTimer = 8f;
    private bool isRespawning = false;
    private bool isFlashing = false;
    private float flashTimer = 0.6f;
    private float flashDuration = 0.6f;
    private int flashCount = 0;
    private Vector2 respawnPosition;
    */

    public Game1()
    {
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
        EnemyManager.Instance.SpawnEnemy();
        itemFactory = new ItemFactory();

        // Load BlockManager
        BlockManager.LoadTexture(dungeonTexture);

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
        Link.Initialize(linkSprite, new Vector2(200, 200));


        masterCollisionHandler.HandleCollisions(

            roomManager.GetCurrentRoomItems(),
            roomManager.CurrentRoom.Enemies,
            ProjectileManager.Instance.GetActiveProjectiles(),
            BlockManager.Instance.GetActiveBlocks());


        controllerList.Add(new KeyboardController(this, Link));


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

        foreach (IController controller in controllerList)
        {
            controller.Update();
        }



        EnemyManager.Instance.Update(gameTime);
        ProjectileManager.Instance.Update(gameTime);
        Link.Instance.Update();


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

        // Set the parameters for the shader
        // Darkness.Parameters["screenSize"].SetValue(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
        // Vector2 scaledLinkPos = Link.Instance.Position + (Link.Instance.GetScaledDimensions() / 2f);
        // Darkness.Parameters["linkPosition"].SetValue(scaledLinkPos);
        // Darkness.Parameters["visibilityRadius"].SetValue(50f);

        base.Update(gameTime);
        
        roomManager.CheckDoorTransition();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(sceneRenderTarget);
        GraphicsDevice.Clear(Color.Black);
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


        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        ShaderManager.ApplyShading(
            _spriteBatch,
            sceneRenderTarget,
            GraphicsDevice
        );

        
        base.Draw(gameTime);
    }
    
}
