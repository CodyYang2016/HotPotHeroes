using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test;
using System;

public interface IItem
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    Vector2 Position { get; }
    string name { get; }
    void Use();  // Action when picked up or used (if applicable)
    void Collect();
    bool IsCollected { get; }
    ItemBehaviorType BehaviorType { get; }
}
public enum ItemBehaviorType
{
    Consumable,
    Collectible,
    Equipable
}