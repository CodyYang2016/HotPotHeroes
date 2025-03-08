using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.Generic;



namespace sprint0Test.Dungeon
{
    public class Room
    {
        // ==========【外墙】==========
        // 整个外墙在 TileSetDungeon 中的源矩形 (521,11)-(777,187), 即 256×176
        public static readonly Rectangle ExteriorSource = new Rectangle(521, 11, 256, 176);
        public static readonly Rectangle ExteriorDest = new Rectangle(0, 0, 256, 176);

        // ==========【Interior】==========
        // interior 目标区域，在本地(0,0)-(256,176)坐标下是 (33,33,192,112)
        // 对应原先 (554,44)-(746,156) 减去外墙基准 (521,11)
        public static readonly Rectangle InteriorDest = new Rectangle(33, 33, 192, 112);

        // 当前房间选到的 interior 源矩形 (R1–R42)
        public Rectangle InteriorSource;

        // ==========【门/墙】==========
        // 你提供的：A(634,11)-(666,43)、B(521,84)-(553,116)、C(634,156)-(666,188)、D(746,84)-(778,116)
        // 这是外墙坐标系，需要减去(521,11)以映射到 (0,0)-(256,176)
        // 计算后得到：
        //   A => (634-521=113, 11-11=0) -> (666-521=145, 43-11=32) => (113,0,32,32)
        //   B => (521-521=0,  84-11=73)->(553-521=32, 116-11=105) => (0,73,32,32)
        //   C => (634-521=113,156-11=145)->(666-521=145,188-11=177) => (113,145,32,32)
        //   D => (746-521=225,84-11=73)->(778-521=257,116-11=105) => (225,73,32,32)
        // （确认每个宽高都是 32×32）

        public static readonly Dictionary<char, Rectangle> DoorDestinations = new Dictionary<char, Rectangle>()
        {
            { 'A', new Rectangle(113,  0, 32, 32) },
            { 'B', new Rectangle(  0, 73, 32, 32) },
            { 'C', new Rectangle(113,145, 32,32) },
            { 'D', new Rectangle(225, 73, 32, 32) },
        };

        // 你给的门/墙源坐标：
        //   A如果是墙 => (816,11)-(848,43)；门 => (849,11)-(881,43)
        //   B如果是墙 => (816,44)-(848,76)；门 => (849,44)-(881,76)
        //   C如果是墙 => (816,110)-(848,142)；门 => (849,110)-(881,142)
        //   D如果是墙 => (816,77)-(848,109)；门 => (849,77)-(881,109)

        public static readonly Dictionary<char, Rectangle> WallSource = new Dictionary<char, Rectangle>()
        {
            { 'A', new Rectangle(816, 11, 32, 32) },
            { 'B', new Rectangle(816, 44, 32, 32) },
            { 'C', new Rectangle(816,110, 32, 32) },
            { 'D', new Rectangle(816, 77, 32, 32) },
        };

        public static readonly Dictionary<char, Rectangle> DoorSource = new Dictionary<char, Rectangle>()
        {
            { 'A', new Rectangle(849, 11, 32, 32) },
            { 'B', new Rectangle(849, 44, 32, 32) },
            { 'C', new Rectangle(849,110, 32, 32) },
            { 'D', new Rectangle(849, 77, 32, 32) },
        };

        // 哪个位置(A/B/C/D)是门，其余三个是墙
        public char DoorLetter;
        public string RoomID { get; private set; }
        public Room(Rectangle interiorSource, char doorLetter, string roomID)
        {
            this.InteriorSource = interiorSource;
            this.DoorLetter = doorLetter;
            this.RoomID = roomID;
        }
    }
}
