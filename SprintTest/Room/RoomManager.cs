/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace sprint0Test.Dungeon
{
    public class RoomManager
    {
        public List<Room> Rooms = new List<Room>();
        public int CurrentRoomIndex = 0;

        private Texture2D dungeonTexture; // TileSetDungeon.png
        private float scale;             // 房间在屏幕上的缩放系数 (若窗口是800×480, room是256×176, etc.)

        public RoomManager(Texture2D dungeonTexture, float scale)
        {
            this.dungeonTexture = dungeonTexture;
            this.scale = scale;

            GenerateAllRooms();
        }

        // 生成若干房间
        private void GenerateAllRooms()
        {
            // 1) 生成 R1–R42 的源坐标
            List<Rectangle> interiors = GenerateInteriors(); // R1..R42

            // 2) 随机生成 5 个房间演示 (你也可以一次把42都用上)
            Random rand = new Random();
            char[] doorLetters = new char[] { 'A', 'B', 'C', 'D' };

            for (int i = 0; i < 5; i++)
            {
                Rectangle interiorSrc = interiors[rand.Next(interiors.Count)];
                char door = doorLetters[rand.Next(doorLetters.Length)];
                Room r = new Room(interiorSrc, door);
                Rooms.Add(r);
            }
        }

        // 你给的 R1-R6, R7-R12, R13-R18... R37-R42
        // 每行6个, 总共7行 => 42个
        // 假设每个 Interior 大小 193×112 (从(1,193)到(194,305)宽193,高112)
        // 这里写一个示例自动生成
        private List<Rectangle> GenerateInteriors()
        {
            var list = new List<Rectangle>();
            // 每行 6 个, x 起点=1, x步长=196 (1->197->393->589->785->981?), 宽=193
            // y 起点=193, 高=112
            // 下一行 y+115(比如 193->308->423->538->653->768->883? )，共7行
            int totalRows = 7;  // R1–R6(第1行), R7–R12(第2行)... R37–R42(第7行)
            int totalCols = 6;  // 每行6个

            int startX = 1;
            int stepX = 196; // 1->197->393->589->785->981
            int width = 193;

            int startY = 193;
            int stepY = 115; // 193->308->423->538->653->768->883
            int height = 112;

            for (int row = 0; row < totalRows; row++)
            {
                for (int col = 0; col < totalCols; col++)
                {
                    int x = startX + col * stepX;
                    int y = startY + row * stepY;
                    Rectangle r = new Rectangle(x, y, width, height);
                    list.Add(r);
                }
            }
            return list;
        }

        public Room GetCurrentRoom()
        {
            return Rooms[CurrentRoomIndex];
        }

        // 原有的 NextRoom 方法
        public void NextRoom()
        {
            CurrentRoomIndex = (CurrentRoomIndex + 1) % Rooms.Count;
        }

        // 新增：提供 SwitchToNextRoom 方法供外部调用
        public void SwitchToNextRoom()
        {
            NextRoom();
        }

        // 绘制当前房间
        public void DrawRoom(SpriteBatch spriteBatch)
        {
            Room room = GetCurrentRoom();

            // 1) 绘制外墙
            Rectangle destExterior = ScaleRect(Room.ExteriorDest);
            spriteBatch.Draw(
                dungeonTexture,
                destExterior,
                Room.ExteriorSource,
                Color.White
            );

            // 2) 绘制内室
            Rectangle destInterior = ScaleRect(Room.InteriorDest);
            spriteBatch.Draw(
                dungeonTexture,
                destInterior,
                room.InteriorSource,
                Color.White
            );

            // 3) 绘制四个边(A/B/C/D)：只有 room.DoorLetter 那个是门，其它是墙
            foreach (var kvp in Room.DoorDestinations)
            {
                char edge = kvp.Key;
                Rectangle dest = ScaleRect(kvp.Value);

                Rectangle source;
                if (edge == room.DoorLetter)
                    source = Room.DoorSource[edge];  // 门
                else
                    source = Room.WallSource[edge];  // 墙

                spriteBatch.Draw(dungeonTexture, dest, source, Color.White);
            }
        }

        // 判断 Link 是否进入了当前房间的“门”区域
        public bool IsLinkAtDoor(Vector2 linkPos, Vector2 linkSize)
        {
            Room room = GetCurrentRoom();
            Rectangle doorDestLocal = Room.DoorDestinations[room.DoorLetter]; // 未缩放
            // 转换成缩放后
            Rectangle doorDestScaled = ScaleRect(doorDestLocal);

            Rectangle linkRect = new Rectangle(
                (int)linkPos.X, (int)linkPos.Y,
                (int)linkSize.X, (int)linkSize.Y
            );
            return linkRect.Intersects(doorDestScaled);
        }

        // 返回当前房间“可活动区域”（Interior + 这扇门的区域）
        public Rectangle GetWalkableArea()
        {
            // interiorDest
            Rectangle interior = Room.InteriorDest;
            // door
            Rectangle doorLocal = Room.DoorDestinations[GetCurrentRoom().DoorLetter];

            // 简单做个 union：把 interior 跟 door 合并成一个大的矩形
            Rectangle unionLocal = Rectangle.Union(interior, doorLocal);
            return ScaleRect(unionLocal);
        }

        private Rectangle ScaleRect(Rectangle r)
        {
            return new Rectangle(
                (int)(r.X * scale),
                (int)(r.Y * scale),
                (int)(r.Width * scale),
                (int)(r.Height * scale)
            );
        }
    }
}
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace sprint0Test.Dungeon
{
    public class RoomManager
    {
        public List<Room> Rooms = new List<Room>();
        public int CurrentRoomIndex = 0;

        private Texture2D dungeonTexture;
        private float scale;
        private MouseState previousMouseState;

        public RoomManager(Texture2D dungeonTexture, float scale)
        {
            this.dungeonTexture = dungeonTexture;
            this.scale = scale;
            GenerateAllRooms();
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
                Room r = new Room(interiors[i], doorSequence[i % doorSequence.Length]);
                Rooms.Add(r);
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

        public Room GetCurrentRoom()
        {
            return Rooms[CurrentRoomIndex];
        }

        // Move to the next room
        public void NextRoom()
        {
            if (CurrentRoomIndex < Rooms.Count - 1)
                CurrentRoomIndex++;
        }

        // Move to the previous room
        public void PreviousRoom()
        {
            if (CurrentRoomIndex > 0)
                CurrentRoomIndex--;
        }

        // Handles mouse click to transition rooms
        /*
        public void HandleMouseClick(Vector2 linkPos, Vector2 linkSize)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (IsLinkAtDoor(linkPos, linkSize))
                    PreviousRoom();
            }
            else if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                if (IsLinkAtDoor(linkPos, linkSize))
                    NextRoom();
            }
            previousMouseState = mouseState;
        }
        */
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
        }

        private Rectangle ScaleRect(Rectangle r)
        {
            return new Rectangle((int)(r.X * scale), (int)(r.Y * scale), (int)(r.Width * scale), (int)(r.Height * scale));
        }
    }
}

