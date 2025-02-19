using System.Collections.Generic;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Interfaces;
using sprint0Test.Items;
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
    private ItemFactory itemFactory;
    public List<IItem> itemList;
    public int currentItemIndex;
    public IItem currentItem;

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
        itemFactory = new ItemFactory();

        //Register Textures
        itemFactory.RegisterTexture("Heart", Content.Load<Texture2D>("heart"));
        itemFactory.RegisterTexture("Boomerang", Content.Load<Texture2D>("boomerang"));

        //Register Item Creation Logic
        itemFactory.RegisterItem("Heart", position => new Heart(itemFactory.GetTexture("Heart"), position));
        itemFactory.RegisterItem("Boomerang", position => new Boomerang(itemFactory.GetTexture("Boomerang"), position, 1, 8));

        //Create Initial Item List
        itemList = new List<IItem>
    {
        itemFactory.CreateItem("Heart", new Vector2(200, 200)),
        itemFactory.CreateItem("Boomerang", new Vector2(200, 200))

    };
                currentItemIndex = 1;
        currentItem = itemList[currentItemIndex];
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
        currentItem.Update(gameTime);


        blockSprites.UpdateActiveBlocks(); // Call to update active blocks

        EnemyManager.Instance.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        sprite.Draw(_spriteBatch);
        currentItem.Draw(_spriteBatch);


        blockSprites.DrawActiveBlocks(_spriteBatch); // Call to draw active blocks
        EnemyManager.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

