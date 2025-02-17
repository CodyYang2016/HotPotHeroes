﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Interfaces;
using sprint0Test.Sprites;

namespace sprint0Test
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D spriteTexture;
        private Vector2 location;
        List<IController> controllerList;
        public ISprite sprite;
        private Link link;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            controllerList = new List<IController>();
            location = new Vector2();
            controllerList.Add(new KeyboardController(this));
            controllerList.Add(new MouseController(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sprite = new StandingInPlacePlayerSprite(spriteTexture);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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

            linkMap.Add((LinkAction.Idle, LinkDirection.Down), new List<Texture2D> { link1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Up), new List<Texture2D> { linkB1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Left), new List<Texture2D> { linkL1 });
            linkMap.Add((LinkAction.Idle, LinkDirection.Right), new List<Texture2D> { linkR1 });

            linkMap.Add((LinkAction.Walking, LinkDirection.Down), new List<Texture2D> { link1, link2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Up), new List<Texture2D> { linkB1, linkB2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Left), new List<Texture2D> { linkL1, linkL2 });
            linkMap.Add((LinkAction.Walking, LinkDirection.Right), new List<Texture2D> { linkR1, linkR2 });

            linkMap.Add((LinkAction.Attacking, LinkDirection.Down), new List<Texture2D> { linkS1, linkS2, linkS3, linkS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Up), new List<Texture2D> { linkBS1, linkBS2, linkBS3, linkBS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Left), new List<Texture2D> { linkLS1, linkLS2, linkLS3, linkLS4 });
            linkMap.Add((LinkAction.Attacking, LinkDirection.Right), new List<Texture2D> { linkRS1, linkRS2, linkRS3, linkRS4 });

            linkMap.Add((LinkAction.Damaged, LinkDirection.Down), new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Up), new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Left), new List<Texture2D> { linkH });
            linkMap.Add((LinkAction.Damaged, LinkDirection.Right), new List<Texture2D> { linkH });

            LinkSprite linkSprite = new LinkSprite(linkMap);
            link = new Link(linkSprite, new Vector2(200, 200));
        }

        protected override void Update(GameTime gameTime)
        {


           /* 
           These needed to be handled by keyboard controller and command classes
        
           var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up)) link.MoveUp();
            else if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down)) link.MoveDown();
            else if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left)) link.MoveLeft();
            else if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right)) link.MoveRight();
            else link.Stop();

            if (kstate.IsKeyDown(Keys.Z) || kstate.IsKeyDown(Keys.N)) link.Attack();

            if (kstate.IsKeyDown(Keys.E)) link.TakeDamage();

            if (kstate.IsKeyDown(Keys.D1)) link.SwitchItem(1);
            if (kstate.IsKeyDown(Keys.D2)) link.SwitchItem(-1);

            if (kstate.IsKeyDown(Keys.X) || kstate.IsKeyDown(Keys.M)) link.UseItem();

            if (kstate.IsKeyDown(Keys.Q)) Exit();
            if (kstate.IsKeyDown(Keys.R)) RestartGame();
            */
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
            link.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}