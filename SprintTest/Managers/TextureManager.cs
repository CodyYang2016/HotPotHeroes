using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0Test.Projectiles;

namespace sprint0Test.Managers
{
    public class TextureManager
    {
        private static TextureManager _instance;
        public static TextureManager Instance => _instance ??= new TextureManager();

        private Dictionary<string, Texture2D> textures;

        private TextureManager()
        {
            textures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(Game game)
        {
            // Load the sprite sheet containing enemy textures
            Texture2D enemySpriteSheet = game.Content.Load<Texture2D>("DungeonEnemy");
            Texture2D bossSpriteSheet = game.Content.Load<Texture2D>("ZeldaBoss");
            Texture2D worldSpriteSheet = game.Content.Load<Texture2D>("ZeldaOverWorld");
            Texture2D tileSheet = game.Content.Load<Texture2D>("TileSetDungeon");
            Texture2D explosion = game.Content.Load<Texture2D>("Explosion");

            //Room Exteriors if neccesary
            textures["Room_Exterior"] = CutTexture(tileSheet, new Rectangle(521, 11, 256, 176));
            textures["ExteriorDest"] = CutTexture(tileSheet, new Rectangle(0, 0, 256, 176));
            textures["Room_Interior"] = CutTexture(tileSheet, new Rectangle(554, 44, 192, 112));
            textures["Door_A"] = CutTexture(tileSheet, new Rectangle(849, 11, 32, 32));
            textures["Door_B"] = CutTexture(tileSheet, new Rectangle(849, 44, 32, 32));
            textures["Door_C"] = CutTexture(tileSheet, new Rectangle(849, 110, 32, 32));
            textures["Door_D"] = CutTexture(tileSheet, new Rectangle(849, 77, 32, 32));
            textures["tileSheet"] = tileSheet;
            textures["Explosion"] = explosion;


            /*            //Items
                        textures["apple"] = CutTexture(apple, new Rectangle(53, 63, 54, 62));
                        textures["heart"] = CutTexture(heart, new Rectangle(31, 27, 32, 29));*/


            // Dragon
            textures["Dragon_Idle1"] = CutTexture(bossSpriteSheet, new Rectangle(1, 11, 24, 32));
            textures["Dragon_Idle2"] = CutTexture(bossSpriteSheet, new Rectangle(25, 11, 24, 32));
            //textures["Dragon_Idle3"] = CutTexture(bossSpriteSheet, new Rectangle(49, 11, 24, 32));
            //textures["Dragon_Idle4"] = CutTexture(bossSpriteSheet, new Rectangle(64, 11, 24, 32));
            textures["Fireball"] = CutTexture(bossSpriteSheet, new Rectangle(107, 11, 10, 16));


            //Octopus
            textures["Octopus_Idle1"] = CutTexture(worldSpriteSheet, new Rectangle(1, 11, 16, 16));
            textures["Octopus_Idle2"] = CutTexture(worldSpriteSheet, new Rectangle(17, 11, 16, 16));
            textures["Rock"] = CutTexture(worldSpriteSheet, new Rectangle(77, 26, 9, 17));

            //Moblin
            textures["Goblin_1"] = CutTexture(worldSpriteSheet, new Rectangle(100, 28, 15, 17));
            textures["Goblin_2"] = CutTexture(worldSpriteSheet, new Rectangle(117, 28, 15, 16));
            // textures["Goblin_3"] = CutTexture(worldSpriteSheet, new Rectangle(131, 11, 16, 16));
            // textures["Goblin_4"] = CutTexture(worldSpriteSheet, new Rectangle(148, 11, 16, 16));

            // Stalfos
            textures["Skeleton"] = CutTexture(enemySpriteSheet, new Rectangle(1, 59, 15, 16));

            // Darknut
            textures["Darknut_Idle_Down_1"] = CutTexture(enemySpriteSheet, new Rectangle(1, 106, 16, 17));
            textures["Darknut_Idle_Down_2"] = CutTexture(enemySpriteSheet, new Rectangle(18, 106, 16, 17));
            // textures["Darknut_Idle_Up_1"] = CutTexture(enemySpriteSheet, new Rectangle(35, 106, 16, 17));
            // textures["Darknut_Idle_Side_1"] = CutTexture(enemySpriteSheet, new Rectangle(52, 106, 16, 17));
            // textures["Darknut_Idle_Side_2"] = CutTexture(enemySpriteSheet, new Rectangle(69, 106, 16, 17));

            // Keese
            textures["Bat_1"] = CutTexture(enemySpriteSheet, new Rectangle(184, 28, 15, 16));
            textures["Bat_2"] = CutTexture(enemySpriteSheet, new Rectangle(200, 28, 15, 16));



        }

        private Texture2D CutTexture(Texture2D spriteSheet, Rectangle sourceRect)
        {
            GraphicsDevice graphicsDevice = spriteSheet.GraphicsDevice;
            Texture2D croppedTexture = new Texture2D(graphicsDevice, sourceRect.Width, sourceRect.Height);

            // Get pixel data from the sprite sheet
            Color[] data = new Color[sourceRect.Width * sourceRect.Height];
            spriteSheet.GetData(0, sourceRect, data, 0, data.Length);

            // Set pixel data to the new texture
            croppedTexture.SetData(data);

            return croppedTexture;
        }

        public Texture2D GetTexture(string name)
        {
            return textures.ContainsKey(name) ? textures[name] : null;
        }
    }
}
