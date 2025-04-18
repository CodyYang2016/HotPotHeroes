using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test;

public interface IItem
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    Vector2 Position { get; }
    void Use();  // Action when picked up or used (if applicable)
    bool IsCollected { get; }

}