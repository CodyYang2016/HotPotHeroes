using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using sprint0Test;
using System.Collections.Generic;
using System;
using sprint0Test.Managers;

public abstract class AbstractRoom : IRoom
{
    public string RoomID { get; protected set; }

    public List<IEnemy> Enemies { get; protected set; } = new List<IEnemy>();
    public List<IBlock> Blocks { get; protected set; } = new List<IBlock>();
    public List<IItem> Items { get; protected set; } = new List<IItem>();

    public Texture2D TilesetTexture { get; set; }
    public Rectangle ExteriorSource { get; set; }
    public Rectangle InteriorSource { get; set; }

    public Dictionary<string, string> AdjacentRooms { get; protected set; } = new Dictionary<string, string>();
    public Dictionary<string, Rectangle> DoorHitboxes { get; protected set; } = new();

    public bool IsCleared => Enemies.TrueForAll(e => e.IsDead);

    public virtual void Initialize() { }


    public virtual void Update(GameTime gameTime)
    {
        foreach (var enemy in Enemies) enemy.Update(gameTime);
        foreach (var block in Blocks) block.Update();
        foreach (var item in Items) item.Update(gameTime);

        Enemies.RemoveAll(e => e.IsDead);
        Items.RemoveAll(i => i.IsCollected);
    }

    // Compute the scale based on window size
    protected float GetRoomScale(GraphicsDevice graphics)
    {
        return Math.Min(
            (float)graphics.Viewport.Width / 256f,
            (float)graphics.Viewport.Height / 176f
        );
    }

    // Apply the scale to a rectangle
    protected Rectangle ScaleRectangle(Rectangle original, float scale)
    {
        return new Rectangle(
            (int)(original.X * scale),
            (int)(original.Y * scale),
            (int)(original.Width * scale),
            (int)(original.Height * scale)
        );
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        float scale = GetRoomScale(spriteBatch.GraphicsDevice);

        Rectangle scaledExteriorDest = ScaleRectangle(RoomData.ExteriorDest, scale);
        Rectangle scaledInteriorDest = ScaleRectangle(RoomData.InteriorDest, scale);

        // Draw exterior and interior
        spriteBatch.Draw(TilesetTexture, scaledExteriorDest, RoomData.ExteriorSource, Color.White);
        spriteBatch.Draw(TilesetTexture, scaledInteriorDest, RoomData.InteriorSource, Color.White);

        // Draw enemies, blocks, and items
        foreach (var block in Blocks) block.Draw(spriteBatch);
        foreach (var enemy in Enemies) enemy.Draw(spriteBatch);
        foreach (var item in Items) item.Draw(spriteBatch);
    }
}
