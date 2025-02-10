using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // controller
        private IController _keyboardController;
        private IController _mouseController;

        // The currently displayed sprite
        private ISprite _currentSprite;

        // Textures
        private Texture2D _spriteSheet;      // mario.png (键1用)
        private Texture2D _spriteSheetJump;  // mariojump.png (键3用)

        // 8 individual Mario frames (16×32 each)
        private Texture2D _mario1;
        private Texture2D _mario2;
        private Texture2D _mario3;
        private Texture2D _mario4;
        private Texture2D _mario5;
        private Texture2D _mario6;
        private Texture2D _mario7;
        private Texture2D _mario8;

        // These two arrays are used to distinguish the 4 frames of "right" and "left".
        private Texture2D[] _marioRightFrames;
        private Texture2D[] _marioLeftFrames;

        private SpriteFont _font;
        private Vector2 _screenCenter;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set the window size
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            _screenCenter = new Vector2(400, 300);

            // Initializing the Controller
            _keyboardController = new KeyboardController();
            _mouseController = new MouseController(new Action[]
            {
                ShowStaticSprite,           // Quad1
                ShowAnimatedSprite,         // Quad2
                ShowMovingSprite,           // Quad3
                ShowMovingAnimatedSprite,   // Quad4
                Exit                        // right click
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Textures
            _spriteSheet = Content.Load<Texture2D>("mario");
            _spriteSheetJump = Content.Load<Texture2D>("mariojump");
            _mario1 = Content.Load<Texture2D>("mario(1)");
            _mario2 = Content.Load<Texture2D>("mario(2)");
            _mario3 = Content.Load<Texture2D>("mario(3)");
            _mario4 = Content.Load<Texture2D>("mario(4)");
            _mario5 = Content.Load<Texture2D>("mario(5)");
            _mario6 = Content.Load<Texture2D>("mario(6)");
            _mario7 = Content.Load<Texture2D>("mario(7)");
            _mario8 = Content.Load<Texture2D>("mario(8)");

            // Consider the first 4 frames as the "walk right" animation frames
            _marioRightFrames = new Texture2D[] { _mario1, _mario2, _mario3, _mario4 };
            // The last 4 frames are considered as the "walk left" animation frames
            _marioLeftFrames = new Texture2D[] { _mario5, _mario6, _mario7, _mario8 };

            _font = Content.Load<SpriteFont>("DefaultFont");

            // Registering Keyboard Commands
            ((KeyboardController)_keyboardController).RegisterCommand(Keys.D1, ShowStaticSprite);
            ((KeyboardController)_keyboardController).RegisterCommand(Keys.D2, ShowAnimatedSprite);
            ((KeyboardController)_keyboardController).RegisterCommand(Keys.D3, ShowMovingSprite);
            ((KeyboardController)_keyboardController).RegisterCommand(Keys.D4, ShowMovingAnimatedSprite);
            ((KeyboardController)_keyboardController).RegisterCommand(Keys.D0, Exit);

            // Displays static Mario by default
            ShowStaticSprite();
        }

        protected override void Update(GameTime gameTime)
        {
            _keyboardController.Update(gameTime);
            _mouseController.Update(gameTime);

            _currentSprite?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentSprite?.Draw(_spriteBatch);

            // Draw text
            _spriteBatch.DrawString(
                _font,
                "Credit",
                new Vector2(10, 530),
                Color.Black
            );

            _spriteBatch.DrawString(
                _font,
                "Program Made By: Yuchen Dang",
                new Vector2(10, 550),
                Color.Black
            );
            _spriteBatch.DrawString(
                _font,
                "Sprites from: https://www.mariomayhem.com/downloads/sprites/super_mario_bros_sprites.php",
                new Vector2(10, 570),
                Color.Black
            );

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // (1) Static
        private void ShowStaticSprite()
        {
            _currentSprite = new StaticSprite(
                _spriteSheet,
                new Rectangle(0, 0, 16, 33),
                _screenCenter,
                scale: 2.0f
            );
        }

        // (2) In-place animation: loop playback [mario1, mario2, mario3, mario4]
        private void ShowAnimatedSprite()
        {
            //Use AnimatedSprite and pass in the 4 images of "walk right"
            _currentSprite = new AnimatedSprite(
                frames: _marioRightFrames,  
                frameTime: 0.15,            
                position: _screenCenter,
                scale: 2.0f
            );
        }

        // (3) Move up and down (non-animated)
        private void ShowMovingSprite()
        {
            _currentSprite = new MovingSprite(
                _spriteSheetJump,
                new Rectangle(0, 0, 16, 33),
                _screenCenter,
                scale: 2.0f
            );
        }

        // (4) Move left or right: Play right [1..4], Play left [5..8]
        private void ShowMovingAnimatedSprite()
        {
            _currentSprite = new MovingAnimatedSprite(
                framesRight: _marioRightFrames,
                framesLeft: _marioLeftFrames,
                frameTime: 0.15,  
                position: _screenCenter,
                scale: 2.0f
            );
        }

        private new void Exit()
        {
            Exit();
        }
    }
}






