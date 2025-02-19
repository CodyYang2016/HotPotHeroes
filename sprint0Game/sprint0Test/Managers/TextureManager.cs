using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotHeroes.sprint0Game.sprint0Test.Managers
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
            Texture2D enemySpriteSheet = game.Content.Load<Texture2D>("zeldaenemy");
            Texture2D bossSpriteSheet = game.Content.Load<Texture2D>("ZeldaBoss");
            Texture2D worldSpriteSheet = game.Content.Load<Texture2D>("ZeldaOverWorld");


            
            // Slice the sprite sheet into individual textures (example: Octorok and Stalfos)
            textures["Dragon_Idle1"] = CutTexture(bossSpriteSheet, new Rectangle(1, 11, 24, 32));
            textures["Dragon_Idle2"] = CutTexture(bossSpriteSheet, new Rectangle(25, 11, 24, 32));
          //textures["Dragon_Idle3"] = CutTexture(bossSpriteSheet, new Rectangle(49, 11, 24, 32));
          //textures["Dragon_Idle4"] = CutTexture(bossSpriteSheet, new Rectangle(64, 11, 24, 32));


            //next one is 25,11 
            textures["Fireball"] = CutTexture(bossSpriteSheet, new Rectangle(109, 25, 10, 16));
            textures["Octopus_Idle1"] = CutTexture(worldSpriteSheet, new Rectangle(1, 11, 16, 16));
            textures["Octopus_Idle2"] = CutTexture(worldSpriteSheet, new Rectangle(17, 11, 16, 16));

            //Octopus Projectile
            textures["Octopus_Projectile"] = CutTexture(bossSpriteSheet, new Rectangle(77, 26, 9, 17));
           
            //next one 18, 11
            textures["Goblin"] = CutTexture(enemySpriteSheet, new Rectangle(82, 11, 16, 16));
            //next one 99,11
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
