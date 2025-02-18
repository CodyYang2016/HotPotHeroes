using System.Collections.Generic;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
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
    List<IController> controllerList;
    public ISprite sprite;

    private BlockSprites blockSprites;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        
        controllerList = new List<IController>();
        //controllerList.Add(new KeyboardController(this, blockSprites));
        controllerList.Add(new MouseController(this));
        
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteTexture = Content.Load<Texture2D>("mario2");
        sprite = new StandingInPlacePlayerSprite(spriteTexture);

        var dungeonTexture = Content.Load<Texture2D>("TileSetDungeon");
        blockSprites = new BlockSprites(dungeonTexture);
        controllerList.Add(new KeyboardController(this, blockSprites));
        TextureManager.Instance.LoadContent(this);
        EnemyManager.Instance.SpawnEnemy();
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (IController controller in controllerList)
        {
            controller.Update();
        }
        sprite.Update();

        blockSprites.UpdateActiveBlocks(); // Call to update active blocks

        EnemyManager.Instance.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        sprite.Draw(_spriteBatch);

        blockSprites.DrawActiveBlocks(_spriteBatch); // Call to draw active blocks
        EnemyManager.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

