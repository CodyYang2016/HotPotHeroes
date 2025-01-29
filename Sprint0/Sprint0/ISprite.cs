using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface ISprite
{
    float Scale{get; set;} //Scale
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
