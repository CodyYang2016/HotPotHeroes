﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Mono;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private List<Sprite> _gameObjects; // List to hold all active game objects
    private SpriteBatch _spriteBatch;

    private Sprite _playerSprite;
    

    private Texture2D link;

    public Vector2 linkPosition;
    //float linkSpeed;

    

    private SpriteFont font;
    private float time = 0;

    List<IController> controllerList;

    public Rectangle spLinkFront1 = new Rectangle(1, 11, 16, 16);
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

         _gameObjects = new List<Sprite>(); // Initialize the list of game objects that are active on screen

        _playerSprite = new Sprite(Content.Load<Texture2D>("Link"))
        {
            Position = new Vector2(200,100),
            sourceRectangle = spLinkFront1
        };

        _gameObjects.Add(_playerSprite);

        controllerList = new List<IController>();
        controllerList.Add(new KeyboardController(this, _playerSprite));
        controllerList.Add(new MouseController(this));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        link = Content.Load<Texture2D>("Link");
        font = Content.Load<SpriteFont>("PlayerFont");

    }

    protected override void Update(GameTime gameTime)
    {
        
        time += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Get controller Inputs
        foreach(IController controller in controllerList){ 
            controller.Update();
        }

        // Update all game objects
        foreach (var gameObject in _gameObjects)
        {
            gameObject.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
  
        _spriteBatch.Begin();

        _spriteBatch.DrawString(font, "Time: " + (int)Math.Floor(time), new Vector2(0, 0), Color.Black);
        _spriteBatch.DrawString(font, "Credits\nElijah Routh\nSprites from:\nhttps://www.spriters-resource.com/nes/legendofzelda/sheet/8366/", new Vector2(10, 350), Color.Black);

        //Draw all active Game Objects
        foreach (var gameObject in _gameObjects)
        {
            gameObject.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    // Method to clear all objects and spawn new ones
    public void Scene1()
    {
        _gameObjects.Clear();
        _gameObjects.Add(_playerSprite); //reload playable link
    }

    //non-moving, animated scene
    public void Scene2()
    {
        _gameObjects.Clear(); 
        
        _gameObjects.Add(new Enemy(Content.Load<Texture2D>("link"), new Vector2(300, 300)));
    }

    //moving, non-animated scene
    public void Scene3()
    {
        _gameObjects.Clear(); 

        _gameObjects.Add(new Enemy2(Content.Load<Texture2D>("link"), new Vector2(300, 300))); 
    }

    //moving, animated scene
    public void Scene4()
    {
        _gameObjects.Clear(); 

        _gameObjects.Add(new Enemy3(Content.Load<Texture2D>("link"), new Vector2(300, 300))); 
    }
}


