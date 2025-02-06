using CSE3902;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CSE3902
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ISprite currentSprite;
        private IController mouseController;
        private IController keyboardController;
        private float scaleFactor;
        private SpriteFont gameFont;
        private string creditsText = "Cody Yang - Art Source: Carmen Canvas Mario";


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            scaleFactor = 3f;
        }

        protected override void Initialize()
        {
            keyboardController = new KeyboardController(this);
            mouseController = new MouseController(this); 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var marioTexture = Content.Load<Texture2D>("Sprite");
            currentSprite = new StaticSprite(marioTexture, new Rectangle(0, 10, 16,18), scaleFactor);
            gameFont = Content.Load<SpriteFont>("Arial"); 

        }

        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update(gameTime); // ✅ Ensure Update uses GameTime
            mouseController.Update(gameTime);
            currentSprite.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Draw current sprite
            currentSprite.Draw(spriteBatch);

            if (gameFont != null)
            {
                spriteBatch.DrawString(gameFont, creditsText, new Vector2(50, 400), Color.White);
            }

            Texture2D rect = new Texture2D(GraphicsDevice, 100, 20);
            Color[] data = new Color[100 * 20];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            rect.SetData(data);

            spriteBatch.Draw(rect, new Vector2(250, 450), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void SetSprite(ISprite newSprite)
        {
            currentSprite = newSprite;
        }
    }
}
