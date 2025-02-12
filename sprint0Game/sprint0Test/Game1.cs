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
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        spriteTexture = Content.Load<Texture2D>("mario2");
        sprite = new StandingInPlacePlayerSprite(spriteTexture);
        //sprite = new FixedAnimatedPlayerSprite(spriteTexture);
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        sprite.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}