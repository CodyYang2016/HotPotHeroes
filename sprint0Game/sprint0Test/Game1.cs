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

    // Declare BlockSprites instance
    private BlockSprites blockSprites;

    private List<IBlock> _active; // List to hold all active game objects
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

        _active = new List<IBlock>();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load the player sprite
        spriteTexture = Content.Load<Texture2D>("mario2");
        sprite = new StandingInPlacePlayerSprite(spriteTexture);

        // Load the dungeon texture
        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");

        // Create the BlockSprites instance
        blockSprites = new BlockSprites(dungeonTexture);

        // Add blocks to the active list (or do other logic as necessary)
        _active.AddRange(blockSprites.gameObjects);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Update controllers and sprite
        foreach (IController controller in controllerList)
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

    public void SetActiveList(IBlock newSprite)
    {
        // Optionally, clear the _active list or keep adding/removing sprites as needed
        _active.Clear();  // For now, let's clear and add just one sprite

        // Add the new active sprite
        _active.Add(newSprite);
    }

    // Method to get the list of all game objects from BlockSprites
    public List<IBlock> GetGameObjects()
    {
        return blockSprites.gameObjects;
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
