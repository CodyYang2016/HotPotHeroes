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

            LoadItemsFromCSV("Content/room-items.csv"); // 如有CSV配置文件
            GenerateAllRooms();
            LoadItemsForRoom(roomIdMap[CurrentRoomIndex]);
        }

        private void LoadItemsFromCSV(string filePath)
        {
            roomItems.Clear();
            if (!System.IO.File.Exists(filePath))
                return;
            string[] lines = System.IO.File.ReadAllLines(filePath);
            bool firstLine = true;
            foreach (string line in lines)
            {
                if (firstLine)
                {
                    firstLine = false;
                    continue;
                }
                string[] parts = line.Split(',');
                if (parts.Length < 4)
                    continue;
                string roomID = parts[0].Trim();
                string itemType = parts[1].Trim();
                if (!float.TryParse(parts[2], out float posX) || !float.TryParse(parts[3], out float posY))
                    continue;
                if (!roomItems.ContainsKey(roomID))
                    roomItems[roomID] = new List<IItem>();
                IItem newItem = itemFactory.CreateItem(itemType, new Vector2(posX, posY));
                roomItems[roomID].Add(newItem);
            }
        }

        private void GenerateAllRooms()
        {
            List<Rectangle> interiors = GenerateInteriors();
            char[] doorSequence = new char[]
            {
                'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D',
                'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D',
                'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'A', 'B', 'C', 'D',
                'A', 'B', 'C', 'D'
            };

            for (int i = 0; i < interiors.Count; i++)
            {
                string roomID = $"Room_{i / 3}_{i % 3}";
                Room r = new Room(interiors[i], doorSequence[i % doorSequence.Length], roomID);
                Rooms.Add(r);
                roomIdMap[i] = roomID;
                LoadItemsForRoom(roomID);
                Debug.WriteLine($"Created Room {roomID}");
            }
        }

        private List<Rectangle> GenerateInteriors()
        {
            var list = new List<Rectangle>();
            int totalRows = 7, totalCols = 6;
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
                currentRoomItems = new List<IItem>(roomItems[roomID]);
            else
                currentRoomItems.Clear();
        }

        public List<IItem> GetCurrentRoomItems()
        {
            return currentRoomItems;
        }

        public Room GetCurrentRoom()
        {
            return Rooms[CurrentRoomIndex];
        }

        public void NextRoom()
        {
            previousDoorLetter = GetCurrentRoom().DoorLetter;
            if (CurrentRoomIndex < Rooms.Count - 1)
                CurrentRoomIndex++;
            else
                CurrentRoomIndex = 0;
            LoadItemsForRoom(roomIdMap[CurrentRoomIndex]);
        }

        // 新增：切换房间并返回 Link 在新房间中出现的位置
        public Vector2 SwitchToNextRoom()
        {
            NextRoom();
            return GetSpawnPositionForNextRoom();
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

        public bool IsLinkAtDoor(Vector2 linkPos, Vector2 linkSize)
        {
            Room room = GetCurrentRoom();
            Rectangle doorDestLocal = Room.DoorDestinations[room.DoorLetter];
            Rectangle doorDestScaled = ScaleRect(doorDestLocal);
            Rectangle linkRect = new Rectangle((int)linkPos.X, (int)linkPos.Y, (int)linkSize.X, (int)linkSize.Y);
            return linkRect.Intersects(doorDestScaled);
        }

        public Rectangle GetWalkableArea()
        {
            Rectangle interior = Room.InteriorDest;
            Rectangle doorLocal = Room.DoorDestinations[GetCurrentRoom().DoorLetter];
            Rectangle unionLocal = Rectangle.Union(interior, doorLocal);
            return ScaleRect(unionLocal);
        }

        private Rectangle ScaleRect(Rectangle r)
        {
            return new Rectangle((int)(r.X * scale), (int)(r.Y * scale), (int)(r.Width * scale), (int)(r.Height * scale));
        }

        // 新增：提供 DrawRoom 方法以供 Game1 调用
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
    }

