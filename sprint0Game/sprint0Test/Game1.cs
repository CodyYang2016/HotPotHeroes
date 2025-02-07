using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Interfaces;
using sprint0Test.Sprites;

namespace sprint0Test;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public Texture2D spriteTexture;
    private Vector2 location;
    List<IController> controllerList;
    public ISprite sprite;

    private List<ISprite> _gameObjects; // List to game objects
    private List<ISprite> _active; // List to hold all active game objects
    private int currentIndex = 0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        controllerList = new List<IController>();
        location = new Vector2();
        controllerList.Add(new KeyboardController(this));
        controllerList.Add(new MouseController(this));

        _gameObjects = new List<ISprite>(); // Initialize the list
        _active = new List<ISprite>();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        spriteTexture = Content.Load<Texture2D>("mario2");
        sprite = new StandingInPlacePlayerSprite(spriteTexture);
        //sprite = new FixedAnimatedPlayerSprite(spriteTexture);

        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon"); // sprites of objects
        // Add Blocks as rocks to the _gameObjects list
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(984, 11, 16, 16),  ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(984, 27, 16, 16),  ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(984, 45, 16, 16),  ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(1001, 11, 16, 16), ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(1001, 27, 16, 16), ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(1001, 45, 16, 16), ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(1018, 11, 16, 16), ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(1018, 27, 16, 16), ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(1035, 11, 16, 16), ObjectType.Rock, new Vector2(100, 80), 3f));
        _gameObjects.Add(new Block(dungeonTexture, new Rectangle(1035, 27, 16, 16), ObjectType.Rock, new Vector2(100, 80), 3f));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // TODO: Add your update logic here
        foreach(IController controller in controllerList)
        {
            controller.Update();
        }
        sprite.Update();

        foreach (var sprite in _active)
        {
            sprite.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        sprite.Draw(_spriteBatch);

        foreach (var sprite in _active)
        {
            sprite.Draw(_spriteBatch);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void SetActiveList(ISprite newSprite)
    {
        // Optionally, clear the _active list or keep adding/removing sprites as needed
        _active.Clear();  // For now, let's clear and add just one sprite

        // Add the new active sprite
        _active.Add(newSprite);
    }
    // Method to get the list of all game objects
    public List<ISprite> GetGameObjects()
    {
        return _gameObjects;
    }
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
    public void SetCurrentIndex(int index)
    {
        currentIndex = index;
    }
}