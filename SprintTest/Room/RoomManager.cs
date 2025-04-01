using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0Test.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace sprint0Test.Dungeon
{
    public class RoomManager
    {
        public List<Room> Rooms = new List<Room>();
        public int CurrentRoomIndex = 0;
        public static RoomManager Instance { get; private set; }
        private Texture2D dungeonTexture;
        private float scale;
        private MouseState previousMouseState;
        private Dictionary<string, List<IItem>> roomItems = new Dictionary<string, List<IItem>>();
        private List<IItem> currentRoomItems = new List<IItem>();
        private ItemFactory itemFactory;
        private Dictionary<int, string> roomIdMap = new Dictionary<int, string>();

        // 记录当前房间的门字母，供切换后确定生成位置
        private char previousDoorLetter;

        // 门连接映射：当前房间门→新房间中应出现的门（例如：A → C, B → D, C → A, D → B）
        private static readonly Dictionary<char, char> OppositeDoorMapping = new Dictionary<char, char>()
        {
            { 'A', 'C' },
            { 'B', 'D' },
            { 'C', 'A' },
            { 'D', 'B' }
        };

        public RoomManager(Texture2D dungeonTexture, float scale, ItemFactory itemFactory)
        {
            this.dungeonTexture = dungeonTexture;
            this.scale = scale;
            this.itemFactory = itemFactory;
            if (Instance == null)
                Instance = this;
            else
                throw new Exception("RoomManager instance already exists!");
            LoadItemsFromCSV("Content/room-items.csv"); // Load items from CSV
            GenerateAllRooms();
            LoadItemsForRoom(roomIdMap[CurrentRoomIndex]);
        }

        private void LoadItemsFromCSV(string filePath)
        {
            roomItems.Clear();  // Reset before loading

            string[] lines = System.IO.File.ReadAllLines(filePath);
            bool firstLine = true;  // Flag to skip the header

            foreach (string line in lines)
            {
                if (firstLine)  // Skip header row
                {
                    firstLine = false;
                    continue;
                }

                string[] parts = line.Split(',');

                if (parts.Length < 4)
                {
                    continue; // Skip invalid lines
                }

                string roomID = parts[0].Trim();
                string itemType = parts[1].Trim();

                // ✅ Ensure X and Y values are correctly parsed
                if (!float.TryParse(parts[2], out float posX) || !float.TryParse(parts[3], out float posY))
                {
                    continue;
                }

                if (!roomItems.ContainsKey(roomID))
                    roomItems[roomID] = new List<IItem>();

                IItem newItem = itemFactory.CreateItem(itemType, new Vector2(posX, posY));
                roomItems[roomID].Add(newItem);
            }
        }




        // Generates rooms in a fixed order
        private void GenerateAllRooms()
        {
            List<Rectangle> interiors = GenerateInteriors(); // Fixed ordering

            char[] doorSequence = new char[]
            {
                'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D',
                'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D',
                'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D'
            };

            for (int i = 0; i < interiors.Count; i++)
            {
                string roomID = $"Room_{i / 3}_{i % 3}";  // Unique Room ID for CSV linking
                Room r = new Room(interiors[i], doorSequence[i % doorSequence.Length], roomID);
                Rooms.Add(r);
                roomIdMap[i] = roomID;
                LoadItemsForRoom(roomID);
                Debug.WriteLine($"🆔 Created Room {roomID}");
            }
        }

        // Generates 42 interiors in a fixed order
        private List<Rectangle> GenerateInteriors()
        {
            var list = new List<Rectangle>();
            int totalRows = 7;
            int totalCols = 6;

            int startX = 1, stepX = 196, width = 193;
            int startY = 193, stepY = 115, height = 112;

            for (int row = 0; row < totalRows; row++)
            {
                for (int col = 0; col < totalCols; col++)
                {
                    int x = startX + col * stepX;
                    int y = startY + row * stepY;
                    list.Add(new Rectangle(x, y, width, height));
                }
            }
            return list;
        }
        public void LoadItemsForRoom(string roomID)
        {
            if (roomItems.ContainsKey(roomID))
            {
                currentRoomItems = new List<IItem>(roomItems[roomID]);
            }
            else
            {
                currentRoomItems.Clear();
            }
        }

        private Vector2 GetSpawnPositionForNextRoom()
        {
            char targetDoor;
            if (OppositeDoorMapping.TryGetValue(previousDoorLetter, out targetDoor))
            {
                // 返回目标门的目标区域经过缩放后的位置
                Rectangle dest = ScaleRect(Room.DoorDestinations[targetDoor]);
                return new Vector2(dest.X, dest.Y);
            }
            else
            {
                // 若找不到对应关系，返回当前房间门位置
                Rectangle dest = ScaleRect(Room.DoorDestinations[previousDoorLetter]);
                return new Vector2(dest.X, dest.Y);
            }
        }

        public List<IItem> GetCurrentRoomItems()
        {
            return currentRoomItems;
        }

        public Room GetCurrentRoom()
        {
            return Rooms[CurrentRoomIndex];
        }
        // Move to the next room
        public void NextRoom()
        {
            if (CurrentRoomIndex < Rooms.Count - 1)
                CurrentRoomIndex++;
            LoadItemsForRoom(roomIdMap[CurrentRoomIndex]);
        }

        // Move to the previous room
        public void PreviousRoom()
        {
            if (CurrentRoomIndex > 0)
                CurrentRoomIndex--;
            LoadItemsForRoom(roomIdMap[CurrentRoomIndex]);
        }
        public void HandleMouseClick(Vector2 linkPos, Vector2 linkSize)
        {
            MouseState mouseState = Mouse.GetState();

            // Check if the left mouse button was just pressed (not held down)
            bool leftClickPressed = mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;

            // Ensure the player is touching the door AND has clicked the left mouse button
            if (leftClickPressed && IsLinkAtDoor(linkPos, linkSize))
            {
                NextRoom(); // Move to the next room only on left-click
            }

            // Update previousMouseState at the end
            previousMouseState = mouseState;
        }

        // Checks if Link is at the door
        public bool IsLinkAtDoor(Vector2 linkPos, Vector2 linkSize)
        {
            Room room = GetCurrentRoom();
            Rectangle doorDestLocal = Room.DoorDestinations[room.DoorLetter];
            Rectangle doorDestScaled = ScaleRect(doorDestLocal);

            Rectangle linkRect = new Rectangle(
                (int)linkPos.X, (int)linkPos.Y,
                (int)linkSize.X, (int)linkSize.Y
            );
            return linkRect.Intersects(doorDestScaled);
        }

        public void DrawRoom(SpriteBatch spriteBatch)
        {
            Room room = GetCurrentRoom();
            spriteBatch.Draw(dungeonTexture, ScaleRect(Room.ExteriorDest), Room.ExteriorSource, Color.White);
            spriteBatch.Draw(dungeonTexture, ScaleRect(Room.InteriorDest), room.InteriorSource, Color.White);

            foreach (var kvp in Room.DoorDestinations)
            {
                char edge = kvp.Key;
                Rectangle dest = ScaleRect(kvp.Value);
                Rectangle source = (edge == room.DoorLetter) ? Room.DoorSource[edge] : Room.WallSource[edge];

                spriteBatch.Draw(dungeonTexture, dest, source, Color.White);
            }

            foreach (var item in GetCurrentRoomItems())
            {
                item.Draw(spriteBatch);
            }
        }

        private Rectangle ScaleRect(Rectangle r)
        {
            return new Rectangle((int)(r.X * scale), (int)(r.Y * scale), (int)(r.Width * scale), (int)(r.Height * scale));
        }
    }
}

