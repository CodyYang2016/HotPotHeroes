using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;

namespace sprint0Test.Managers
{

    public enum ShaderList
    {
        Darkness,
        other
    }
    public class ShaderManager
    {
        private static ShaderManager _instance;
        public static ShaderManager Instance => _instance ??= new ShaderManager();

        //private static Effect shader;

        private static Effect Darkness;
        private static bool applyDarkness = true;

        // private Dictionary<ShaderList, String> shaderToString = new Dictionary<ShaderList, String>
        // {
        //     { ShaderList.Darkness, "Darkness" },
        //     //{ ShaderList.other, "other" },
        // };

        public void LoadContent(ContentManager content) //, ShaderList newShader)
        {
            Darkness = content.Load<Effect>("Darkness");
        }

        public static void EnableDarkness() => applyDarkness = true;
        public static void DisableDarkness() => applyDarkness = false;
        public static void ToggleDarkness() => applyDarkness = !applyDarkness;

        public static void ApplyShading(SpriteBatch spriteBatch, Texture2D sceneTexture, GraphicsDevice graphics)
        {
            graphics.Clear(Color.CornflowerBlue);

            // Choose null or shader based on flag
            Effect effectToUse = applyDarkness ? Darkness : null;

            if (applyDarkness)
            {
                Vector2 linkPos = Link.Instance.Position + (Link.Instance.GetScaledDimensions() / 2f);

                Darkness.Parameters["screenSize"].SetValue(new Vector2(graphics.Viewport.Width, graphics.Viewport.Height));
                Darkness.Parameters["linkPosition"].SetValue(linkPos);
                Darkness.Parameters["visibilityRadius"].SetValue(50f);
            }

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, effectToUse);
            spriteBatch.Draw(sceneTexture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

    }
}
