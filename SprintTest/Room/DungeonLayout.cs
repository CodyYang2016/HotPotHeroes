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
        var room1a = new RoomData("1a");
        room1a.Doors["Right"] = "1b";
        room1a.Doors["Down"] = "2a";
        rooms["1a"] = room1a;

        var room1b = new RoomData("1b");
        room1b.Doors["Left"] = "1a";
        room1b.Doors["Down"] = "2b";
        rooms["1b"] = room1b;

        var room2a = new RoomData("2a");
        room2a.Doors["Up"] = "1a";
        room2a.Doors["Right"] = "2b";
        rooms["2a"] = room2a;

        var room2b = new RoomData("2b");
        room2b.Doors["Up"] = "1b";
        room2b.Doors["Left"] = "2a";
        rooms["2b"] = room2b;

        // Add more as needed
    }

    public RoomData GetRoom(string roomID)
    {
        return rooms.TryGetValue(roomID, out var room) ? room : null;
    }

    public IEnumerable<RoomData> GetAllRooms() => rooms.Values;
}
