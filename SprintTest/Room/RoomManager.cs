using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Dungeon;
using sprint0Test.Items;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Managers;

public class RoomManager
{
    public Dictionary<string, IRoom> Rooms { get; private set; } = new();
    public IRoom CurrentRoom { get; private set; }
    private ItemFactory itemFactory;

    public RoomManager(ItemFactory itemFactory)
    {
        this.itemFactory = itemFactory;
        GenerateRooms();
        SwitchToRoom("1a");
    }
    private float scale = 1.0f; // Add this field to your class if not present

    private Rectangle ScaleRect(Rectangle r)
    {
        return new Rectangle(
            (int)(r.X * scale),
            (int)(r.Y * scale),
            (int)(r.Width * scale),
            (int)(r.Height * scale)
        );
    }


    private void GenerateRooms()
    {
        DungeonLayout layout = new DungeonLayout();

        // Load the shared texture (make sure it's loaded in TextureManager)
        Texture2D tileset = TextureManager.Instance.GetTexture("tileSheet");

        foreach (RoomData data in layout.GetAllRooms())
        {
            var room = new SampleRoom(data.RoomID);

            // ✅ Assign texture and visual source rectangles
            room.TilesetTexture = tileset;
            room.ExteriorSource = RoomData.ExteriorSource;
            room.InteriorSource = RoomData.InteriorSource; // You can later make this per-room

            // ✅ Assign door connections
            foreach (var door in data.Doors)
            {
                if (!string.IsNullOrEmpty(door.Value))
                {
                    room.AdjacentRooms[door.Key] = door.Value;
                }
            }

            room.Initialize();
            Rooms[data.RoomID] = room;
        }
    }



    public void SwitchToRoom(string roomID)
    {
        if (Rooms.TryGetValue(roomID, out var newRoom))
        {
            CurrentRoom = newRoom;
        }
        else
        {
            Debug.WriteLine($"Room '{roomID}' not found.");
        }
    }

    public void MoveToAdjacentRoom(string direction)
    {
        if (CurrentRoom != null && CurrentRoom.AdjacentRooms.TryGetValue(direction, out var nextRoomID))
        {
            SwitchToRoom(nextRoomID);
        }
    }

    public void Update(GameTime gameTime)
    {
        CurrentRoom?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        CurrentRoom?.Draw(spriteBatch);
    }
    public List<IItem> GetCurrentRoomItems()
    {
        return CurrentRoom?.Items ?? new List<IItem>();
    }




    private MouseState previousMouseState;



    public void CheckDoorTransition(Vector2 linkPos, Vector2 linkSize)
    {
        IRoom room = CurrentRoom;
        if (room == null) return;

        MouseState mouseState = Mouse.GetState();
        bool clicked = mouseState.LeftButton == ButtonState.Pressed &&
                       previousMouseState.LeftButton == ButtonState.Released;

        Rectangle linkRect = new Rectangle((int)linkPos.X, (int)linkPos.Y, (int)linkSize.X, (int)linkSize.Y);

        foreach (var kvp in room.DoorHitboxes)
        {
            string direction = kvp.Key;
            Rectangle doorRect = kvp.Value;

            if (clicked && linkRect.Intersects(doorRect))
            {
                MoveToAdjacentRoom(direction);
                break;
            }
        }

        previousMouseState = mouseState;

    }



}
