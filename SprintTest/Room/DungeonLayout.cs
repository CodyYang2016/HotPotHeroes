using System.Collections.Generic;

public class DungeonLayout
{
    private Dictionary<string, RoomData> rooms = new();

    public DungeonLayout()
    {
        InitializeRooms();
    }

    private void InitializeRooms()
    {
        var room1b = new RoomData("1b");
        room1b.Doors["Right"] = "1c";
        rooms["1b"] = room1b;

        var room1c = new RoomData("1c");
        room1c.Doors["Up"] = "2c";
        room1c.Doors["Right"] = "1d";
        room1c.Doors["Left"] = "1b";
        rooms["1c"] = room1c;

        var room1d = new RoomData("1d");
        room1d.Doors["Left"] = "1c";
        rooms["1d"] = room1d;

        var room2c = new RoomData("2c");
        room2c.Doors["Up"] = "3c";
        room2c.Doors["Down"] = "1c";
        rooms["2c"] = room2c;

        var room3c = new RoomData("3c");
        room3c.Doors["Up"] = "4c";
        room3c.Doors["Down"] = "2c";
        room3c.Doors["Left"] = "3b";
        room3c.Doors["Right"] = "3d";
        rooms["3c"] = room3c;

        var room3b = new RoomData("3b");
        room3c.Doors["Up"]= "4b";
        room3b.Doors["Right"] = "3c";
        rooms["3b"] = room3b;

        var room3d = new RoomData("3d");
        room3c.Doors["Up"] = "4d";  
        room3d.Doors["Left"] = "3c";
        rooms["3d"] = room3d;

        var room4a = new RoomData("4a");
        room4a.Doors["Right"] = "4b";
        rooms["4a"] = room4a;

        var room4b = new RoomData("4b");
        rooms["4b"].Doors["Left"] = "4a";
        rooms["4b"].Doors["Right"] = "4c";
        rooms["4b"].Doors["Down"] = "3b";

        var room4c = new RoomData("4c");
        rooms["4c"].Doors["Left"] = "4b";
        rooms["4c"].Doors["Right"] = "4d";
        rooms["4c"].Doors["Down"] = "3c";
        rooms["4c"].Doors["Up"] = "5c";
        rooms["4c"] = room4c;

        var room4d = new RoomData("4d");
        room4d.Doors["Left"] = "4c";
        room4d.Doors["Right"] = "4e";
        room4d.Doors["Down"] = "3d";
        rooms["4d"] = room4d;

        var room4e = new RoomData("4e");
        room4e.Doors["Left"] = "4d";
        room4e.Doors["Up"] = "5e";
        rooms["4e"] = room4e;

        var room5c = new RoomData("5c");
        room5c.Doors["Down"] = "4c";
        room5c.Doors["Up"] = "6c";
        rooms["5c"] = room5c;

        var room5e = new RoomData("5e");
        room5e.Doors["Down"] = "4e";
        rooms["5e"] = room5e;

        var room6c = new RoomData("6c");
        room6c.Doors["Down"] = "5c";
        room6c.Doors["Left"] = "6b";
        rooms["6c"] = room6c;

        var room6b = new RoomData("6b");
        room6b.Doors["Right"] = "5a";
        rooms["6b"] = room6b;


        // Add more as needed
    }

    public RoomData GetRoom(string roomID)
    {
        return rooms.TryGetValue(roomID, out var room) ? room : null;
    }

    public IEnumerable<RoomData> GetAllRooms() => rooms.Values;
}
