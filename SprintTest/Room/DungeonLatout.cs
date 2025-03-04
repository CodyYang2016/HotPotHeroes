using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace sprint0Test.Dungeon
{
    public class DungeonLayout
    {
        // 假设我们有 3 行 × 3 列的地牢
        private RoomData[,] rooms;

        public DungeonLayout()
        {
            rooms = new RoomData[3, 3];
            InitializeRooms();
        }

        private void InitializeRooms()
        {
            // 这里演示一个小地图:
            //   (0,0)房间: 上门无, 下门 -> (1,0), 左门无, 右门 -> (0,1)
            //   (0,1)房间: ...
            //   ...
            // 你可以在这里把你贴图的 Interiors, 碰撞信息, 门连接, 都手动写进去

            // ===== 示例: (0,0)房间 =====
            RoomData room00 = new RoomData();
            room00.RoomID = "A1"; // 自定义名称
            // 定义门
            room00.Doors["Up"] = null;     // 没有上门
            room00.Doors["Down"] = new Point(1, 0); // 下门连到(1,0)
            room00.Doors["Left"] = null;
            room00.Doors["Right"] = new Point(0, 1);
            // 定义瓦片碰撞: 这里随便举例, 16×11瓦片
            room00.CollisionMap = new bool[16, 11];
            // 假设边缘都是墙
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    bool isWall = (x == 0 || x == 15 || y == 0 || y == 10);
                    room00.CollisionMap[x, y] = !isWall;
                    // 这里 isWall=false 表示可以走, 你可以倒过来
                }
            }

            rooms[0, 0] = room00;

            // ===== 示例: (0,1)房间 =====
            RoomData room01 = new RoomData();
            room01.RoomID = "A2";
            room01.Doors["Up"] = null;
            room01.Doors["Down"] = null;
            room01.Doors["Left"] = new Point(0, 0); // 左门连回 (0,0)
            room01.Doors["Right"] = null;
            // 瓦片碰撞 (同理)
            room01.CollisionMap = new bool[16, 11];
            // ...
            rooms[0, 1] = room01;

            // 依次初始化其他房间...
            // 直到把(2,2)都填完
        }

        public RoomData GetRoom(int row, int col)
        {
            if (row < 0 || row >= 3 || col < 0 || col >= 3)
                return null;
            return rooms[row, col];
        }
    }

    // 存储一个房间的数据
    public class RoomData
    {
        public string RoomID; // 例如 "A1"
        // Doors 存储上下左右四个门对应的房间坐标 (row,col)
        // 若为 null 表示无门或墙
        public Dictionary<string, Point?> Doors;
        // 瓦片碰撞信息: [x,y] = true 表示可走, false表示是墙
        public bool[,] CollisionMap;

        public RoomData()
        {
            Doors = new Dictionary<string, Point?>()
            {
                { "Up", null }, { "Down", null },
                { "Left", null }, { "Right", null }
            };
        }
    }
}

