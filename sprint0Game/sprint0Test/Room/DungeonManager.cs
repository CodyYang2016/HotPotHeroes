using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0Test.Dungeon
{
    public class DungeonManager
    {
        private DungeonLayout layout;
        private int currentRow, currentCol;

        public DungeonManager()
        {
            layout = new DungeonLayout();
            currentRow = 0;
            currentCol = 0; // 初始在(0,0)
        }

        // 切换房间: 例如从(0,0)的 "Right" 门去(0,1)
        public bool GoThroughDoor(string door)
        {
            RoomData room = layout.GetRoom(currentRow, currentCol);
            if (room == null) return false;

            if (!room.Doors.ContainsKey(door)) return false;
            var target = room.Doors[door];
            if (target == null) return false; // 表示没有门或是墙

            Point next = target.Value;
            if (layout.GetRoom(next.X, next.Y) == null)
                return false; // 那个房间不存在

            // 切换到下一个房间
            currentRow = next.X;
            currentCol = next.Y;
            return true;
        }

        // 获取当前房间数据
        public RoomData GetCurrentRoom()
        {
            return layout.GetRoom(currentRow, currentCol);
        }

        // 判断瓦片是否可走
        public bool CanWalk(int tileX, int tileY)
        {
            RoomData room = GetCurrentRoom();
            if (room == null) return false;
            if (tileX < 0 || tileX >= room.CollisionMap.GetLength(0)) return false;
            if (tileY < 0 || tileY >= room.CollisionMap.GetLength(1)) return false;
            return room.CollisionMap[tileX, tileY];
        }
    }
}
