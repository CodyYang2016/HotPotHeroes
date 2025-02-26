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
    private Link Link;

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
        //controllerList.Add(new KeyboardController(this, Link, blockSprites));
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

        // Damageda
        linkMap.Add((LinkAction.Damaged, LinkDirection.Down),
            new List<Texture2D> { linkH });
        linkMap.Add((LinkAction.Damaged, LinkDirection.Up),
            new List<Texture2D> { linkH });
        linkMap.Add((LinkAction.Damaged, LinkDirection.Left),
            new List<Texture2D> { linkH });
        linkMap.Add((LinkAction.Damaged, LinkDirection.Right),
            new List<Texture2D> { linkH });

        LinkSprite linkSprite = new LinkSprite(linkMap);

        Link = new Link(linkSprite, new Vector2(200, 200));
        controllerList.Add(new KeyboardController(this, Link, blockSprites));
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
        Link.Draw(_spriteBatch);

        blockSprites.DrawActiveBlocks(_spriteBatch); // Call to draw active blocks
        EnemyManager.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

