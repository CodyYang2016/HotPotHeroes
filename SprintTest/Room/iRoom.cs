using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using sprint0Test;
using System.Collections.Generic;

public interface IRoom
{
    string RoomID { get; }
    List<IEnemy> Enemies { get; }
    List<IBlock> Blocks { get; }
    List<IItem> Items { get; }

    Dictionary<string, string> AdjacentRooms { get; } // e.g. { "Up": "2a", "Right": "1b" }

    Dictionary<string, Rectangle> DoorHitboxes { get; }


    bool IsCleared { get; }

    void Initialize();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
