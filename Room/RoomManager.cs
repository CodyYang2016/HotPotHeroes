using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Dungeon;
using sprint0Test.Items;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Managers;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test.Dungeon
{
    public class RoomManager
    {
        public Dictionary<string, IRoom> Rooms { get; private set; } = new();
        public IRoom CurrentRoom { get; private set; }
        private ItemFactory itemFactory;
        private DungeonLayout layout = new DungeonLayout(); // At the top of RoomManager
        private double doorCooldown = 0; // in milliseconds

        public RoomManager(ItemFactory itemFactory)
        {
            this.itemFactory = itemFactory;
            LoadRoom("r1c");
        }

        private float scale = 1.0f;

        private Rectangle ScaleRect(Rectangle r)
        {
            return new Rectangle(
                (int)(r.X * scale),
                (int)(r.Y * scale),
                (int)(r.Width * scale),
                (int)(r.Height * scale)
            );
        }

        public void LoadRoom(string roomID)
        {
            RoomData roomData = layout.GetRoom(roomID);
            BlockManager.Instance.ClearActiveBlocks();
            if (roomData == null)
            {
                Debug.WriteLine($"❌ Room data not found for ID: {roomID}");
                return;
            }

            AbstractRoom room = roomID switch
            {
                "r1b" => new r1b(roomData),
                "r1c" => new r1c(roomData),
                "r1d" => new r1d(roomData),
                "r2c" => new r2c(roomData),
                "r3b" => new r3b(roomData),
                "r3c" => new r3c(roomData),
                "r3d" => new r3d(roomData),
                "r4a" => new r4a(roomData),
                "r4b" => new r4b(roomData),
                "r4c" => new r4c(roomData),
                "r4d" => new r4d(roomData),
                "r4e" => new r4e(roomData),
                "r5c" => new r5c(roomData),
                "r5e" => new r5e(roomData),
                "r5f" => new r5f(roomData),
                "r6b" => new r6b(roomData),
                "r6c" => new r6c(roomData),
                "r8c" => new r8c(roomData),
                _ => null
            };

            if (room == null)
            {
                Debug.WriteLine($"❌ No room class mapped for room ID: {roomID}");
                return;
            }

            room.TilesetTexture = TextureManager.Instance.GetTexture("tileSheet");
            room.ExteriorSource = RoomData.ExteriorSource;
            room.InteriorSource = RoomData.InteriorSource;
            room.Items = ItemManager.Instance.GetItemsForRoom(roomID);
            room.Initialize();
            CurrentRoom = room;

            Debug.WriteLine($"✅ Loaded room: {roomID}");
        }

        public void Update(GameTime gameTime)
        {
            if (doorCooldown > 0)
                doorCooldown -= gameTime.ElapsedGameTime.TotalMilliseconds;

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

        public void CheckDoorTransition()
        {
            if (doorCooldown > 0 || CurrentRoom == null || CurrentRoom.RoomData == null)
                return;

            Rectangle linkRect = new Rectangle(
                (int)Link.Instance.Position.X,
                (int)Link.Instance.Position.Y,
                (int)Link.Instance.GetScaledDimensions().X,
                (int)Link.Instance.GetScaledDimensions().Y
            );

            foreach (var doorEntry in CurrentRoom.DoorHitboxes)
            {
                string direction = doorEntry.Key;
                Rectangle doorHitbox = doorEntry.Value;

                if (linkRect.Intersects(doorHitbox))
                {
                    if (CurrentRoom.RoomData.Doors.TryGetValue(direction, out string nextRoomID) && nextRoomID != null)
                    {
                        Debug.WriteLine($"➡️ Transitioning {direction} from {CurrentRoom.RoomID} to {nextRoomID}");

                        LoadRoom(nextRoomID);
                        PositionPlayerAtEntry(direction);

                        doorCooldown = 2000;
                        break;
                    }
                    else
                    {
                        Debug.WriteLine($"❌ No next room found from direction: {direction}");
                    }
                }
            }
        }

        public void PositionPlayerAtEntry(string fromDirection)
        {
            string toDirection = fromDirection switch
            {
                "Up" => "Down",
                "Down" => "Up",
                "Left" => "Right",
                "Right" => "Left",
                _ => null
            };

            if (toDirection != null && CurrentRoom.DoorHitboxes.TryGetValue(toDirection, out var entryRect))
            {
                Vector2 newPos = toDirection switch
                {
                    "Up" => new Vector2(entryRect.X + entryRect.Width / 2 - 8, entryRect.Y + 16),
                    "Down" => new Vector2(entryRect.X + entryRect.Width / 2 - 8, entryRect.Y - 32),
                    "Left" => new Vector2(entryRect.X + 16, entryRect.Y + entryRect.Height / 2 - 8),
                    "Right" => new Vector2(entryRect.X - 32, entryRect.Y + entryRect.Height / 2 - 8),
                    _ => new Vector2(entryRect.X, entryRect.Y)
                };

                Debug.WriteLine($"🚪 Spawning Link from {fromDirection} into {toDirection} at {newPos}");
                Link.Instance.SetPosition(newPos);
            }
        }
    }
}