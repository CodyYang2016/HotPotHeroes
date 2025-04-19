using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using sprint0Test;
using System.Collections.Generic;
using System;
using sprint0Test.Room;

public abstract class AbstractRoom : IRoom
{
    public RoomData RoomData { get; set; }

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

    public virtual void Initialize()
    {
        // Automatically generate door hitboxes from RoomData
        
        Perimeter perimeter = new Perimeter(this);
        GenerateStandardDoorHitboxes();
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (var enemy in Enemies) enemy.Update(gameTime);
        foreach (var block in Blocks) block.Update();
        foreach (var item in Items) item.Update(gameTime);

        Enemies.RemoveAll(e => e.IsDead);
        Items.RemoveAll(i => i.IsCollected);

        if (!RoomData.HasBeenCleared && Enemies.Count == 0)
        {
            RoomData.HasBeenCleared = true;
        }
    }

    protected float GetRoomScale(GraphicsDevice graphics)
    {
        return Math.Min(
            (float)graphics.Viewport.Width / 256f,
            (float)graphics.Viewport.Height / 176f
        );
    }

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

        spriteBatch.Draw(TilesetTexture, scaledExteriorDest, RoomData.ExteriorSource, Color.White);
        spriteBatch.Draw(TilesetTexture, scaledInteriorDest, RoomData.InteriorSource, Color.White);

        DrawDoor(spriteBatch, "Up", RoomData.Door_Top, RoomData.Not_Door_Top, RoomData.Top_Dest, scale);
        DrawDoor(spriteBatch, "Down", RoomData.Door_Bottom, RoomData.Not_Door_Bottom, RoomData.Bottom_Dest, scale);
        DrawDoor(spriteBatch, "Left", RoomData.Door_Left, RoomData.Not_Door_Left, RoomData.Left_Dest, scale);
        DrawDoor(spriteBatch, "Right", RoomData.Door_Right, RoomData.Not_Door_Right, RoomData.Right_Dest, scale);

        foreach (var block in Blocks) block.Draw(spriteBatch);
        foreach (var enemy in Enemies) enemy.Draw(spriteBatch);
        foreach (var item in Items) item.Draw(spriteBatch);
    }

    private void DrawDoor(SpriteBatch spriteBatch, string direction, Rectangle doorSource, Rectangle notDoorSource, Rectangle destination, float scale)
    {
        Rectangle scaledDest = ScaleRectangle(destination, scale);
        bool hasDoor = RoomData.Doors.ContainsKey(direction) && RoomData.Doors[direction] != null;
        Rectangle source = hasDoor ? doorSource : notDoorSource;
        spriteBatch.Draw(TilesetTexture, scaledDest, source, Color.White);
    }

    protected void GenerateStandardDoorHitboxes()
    {
        float scale = GetRoomScale(GraphicsDeviceHelper.Device);

        if (RoomData.Doors["Up"] != null)
        {
            DoorHitboxes["Up"] = ScaleRectangle(RoomData.Top_Dest, scale);
        }

        if (RoomData.Doors["Down"] != null)
        {
            DoorHitboxes["Down"] = ScaleRectangle(RoomData.Bottom_Dest, scale);
        }

        if (RoomData.Doors["Left"] != null)
        {
            DoorHitboxes["Left"] = ScaleRectangle(RoomData.Left_Dest, scale);
        }

        if (RoomData.Doors["Right"] != null)
        {
            DoorHitboxes["Right"] = ScaleRectangle(RoomData.Right_Dest, scale);
        }
    }

    public bool HasDoor(string direction)
    {
        bool hasDoor = RoomData.Doors.ContainsKey(direction) && RoomData.Doors[direction] != null;
        return hasDoor;
    }

}
