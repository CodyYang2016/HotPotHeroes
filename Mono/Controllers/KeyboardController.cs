using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mono
{
    public class KeyboardController : IController
    {
        private Sprite _sprite; // Reference to the sprite
        private Game1 _game;

            public Rectangle spLinkFront1 = new Rectangle(1, 11, 16, 16);
            public Rectangle spLinkFront2 = new Rectangle(18, 11, 16, 16);
            public Rectangle spLinkRight1 = new Rectangle(35, 11, 16, 16);
            public Rectangle spLinkRight2 = new Rectangle(52, 11, 16, 16);
            public Rectangle spLinkBack1 = new Rectangle(70, 11, 16, 16);
            public Rectangle spLinkBack2 = new Rectangle(86, 11, 16, 16);

            //public Rectangle spLinkLeft1 = new Rectangle(103, 11, 16, 16);
            //public Rectangle spLinkLeft2 = new Rectangle(1, 11, 16, 16);

    public Rectangle spFire = new Rectangle(191, 185, 16, 16);

        public KeyboardController(Game1 game, Sprite sprite)
        {
            _sprite = sprite;
            _game = game;
        }

        public void Update()
        {
            var kstate = Keyboard.GetState();

            // Move sprite with arrow keys
            if (kstate.IsKeyDown(Keys.Up))
            {
                _sprite.Position.Y -= 3; // Move sprite up
                //_sprite.sourceRectangle = spLinkBack1;
                if(_sprite.animationCycle.Count == 0)
                {
                    _sprite.animationCycle.Add(spLinkBack1);
                    _sprite.animationCycle.Add(spLinkBack2);
                }
                
            }
            
            if (kstate.IsKeyDown(Keys.Down))
            {
                _sprite.Position.Y += 3; // Move sprite down
                //_sprite.sourceRectangle = spLinkFront1;
                if(_sprite.animationCycle.Count == 0)
                {
                    _sprite.animationCycle.Add(spLinkFront1);
                    _sprite.animationCycle.Add(spLinkFront2);
                }
            }
            
            if (kstate.IsKeyDown(Keys.Left))
            {
                _sprite.Position.X -= 3; // Move sprite left
                //_sprite.sourceRectangle = spLinkRight1;
                if(_sprite.animationCycle.Count == 0)
                {
                    _sprite.animationCycle.Add(spLinkRight1);
                    _sprite.animationCycle.Add(spLinkRight2);
                }
            }
            
            if (kstate.IsKeyDown(Keys.Right))
            {
                _sprite.Position.X += 3; // Move sprite right
                //_sprite.sourceRectangle = spLinkRight1;
                if(_sprite.animationCycle.Count == 0)
                {
                    _sprite.animationCycle.Add(spLinkRight1);
                    _sprite.animationCycle.Add(spLinkRight2);
                }
            }

            // Space key to trigger some other behavior (if needed)
            if (kstate.IsKeyDown(Keys.Space))
            {
                if(_sprite.animationCycle.Count == 0)
                {
                    _sprite.animationCycle.Add(spLinkBack1);
                    _sprite.animationCycle.Add(spLinkRight1);
                    _sprite.animationCycle.Add(spLinkFront1);
                    //_sprite.animationCycle.Add(spLinkLeft1);
                }
                //_sprite.animationCycle.Add(spFire);
                //_sprite.sourceRectangle = spFire;
            }

            if (kstate.GetPressedKeyCount() == 0)
            {
                _sprite.animationCycle.Clear();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D0))
            _game.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                _game.Scene1();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                _game.Scene2();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                _game.Scene3();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                _game.Scene4();
            }
            
        }
    }
}



// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Input;

// namespace Mono
// {
//     public class KeyboardController : IController
//     {
//         private Game1 _game;

//         public KeyboardController(Game1 game)
//         {
//             _game = game;
//         }

//         public void Update()
//         {
//             var kstate = Keyboard.GetState();

//             if (kstate.IsKeyDown(Keys.Up))
//             {
//                 _game.linkPosition.Y -= 5;
//             }
            
//             if (kstate.IsKeyDown(Keys.Down))
//             {
//                 _game.linkPosition.Y += 5;
//             }
            
//             if (kstate.IsKeyDown(Keys.Left))
//             {
//                 _game.linkPosition.X -= 5;
//             }
            
//             if (kstate.IsKeyDown(Keys.Right))
//             {
//                 _game.linkPosition.X += 5;
//             }

//             if (kstate.IsKeyDown(Keys.Space))
//             {
//                 _game.linkPosition.X += 5;
//             }
//         }
//     }
// }
