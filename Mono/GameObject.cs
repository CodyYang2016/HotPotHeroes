using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    public abstract class GameObject
    {
        public abstract void Update(GameTime gameTime); 
        public abstract void Draw(SpriteBatch spriteBatch); 
    }
}
