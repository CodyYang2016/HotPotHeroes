using Microsoft.Xna.Framework;
using System.Collections.Generic;



namespace sprint0Test.Dungeon
{
    public class Room
    {
      
        public static readonly Rectangle ExteriorSource = new Rectangle(521, 11, 256, 176);
        public static readonly Rectangle ExteriorDest = new Rectangle(0, 0, 256, 176);

      
        public static readonly Rectangle InteriorDest = new Rectangle(33, 33, 192, 112);

    
        public Rectangle InteriorSource;

     

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
