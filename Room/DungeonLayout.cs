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
        var r1b = new RoomData("r1b");
        r1b.Doors["Right"] = "r1c";
        rooms["r1b"] = r1b;

        var r1c = new RoomData("r1c");
        r1c.Doors["Up"] = "r2c";
        r1c.Doors["Right"] = "r1d";
        r1c.Doors["Left"] = "r1b";
        rooms["r1c"] = r1c;

        var r1d = new RoomData("r1d");
        r1d.Doors["Left"] = "r1c";
        rooms["r1d"] = r1d;

        var r2c = new RoomData("r2c");
        r2c.Doors["Up"] = "r3c";
        r2c.Doors["Down"] = "r1c";
        rooms["r2c"] = r2c;

        var r3c = new RoomData("r3c");
        r3c.Doors["Up"] = "r4c";
        r3c.Doors["Down"] = "r2c";
        r3c.Doors["Left"] = "r3b";
        r3c.Doors["Right"] = "r3d";
        rooms["r3c"] = r3c;

        var r3b = new RoomData("r3b");
        r3b.Doors["Right"] = "r3c";
        rooms["r3b"] = r3b;

        var r3d = new RoomData("r3d");
        r3d.Doors["Left"] = "r3c";
        r3d.Doors["Up"] = "r4d";
        rooms["r3d"] = r3d;

        var r4a = new RoomData("r4a");
        r4a.Doors["Right"] = "r4b";
        rooms["r4a"] = r4a;

        var r4b = new RoomData("r4b");
        r4b.Doors["Left"] = "r4a";
        r4b.Doors["Right"] = "r4c";
        r4b.Doors["Down"] = "r3b";
        rooms["r4b"] = r4b;

        var r4c = new RoomData("r4c");
        r4c.Doors["Left"] = "r4b";
        r4c.Doors["Right"] = "r4d";
        r4c.Doors["Down"] = "r3c";
        r4c.Doors["Up"] = "r5c";
        rooms["r4c"] = r4c;

        var r4d = new RoomData("r4d");
        r4d.Doors["Left"] = "r4c";
        r4d.Doors["Right"] = "r4e";
        r4d.Doors["Down"] = "r3d";
        rooms["r4d"] = r4d;

        var r4e = new RoomData("r4e");
        r4e.Doors["Left"] = "r4d";
        r4e.Doors["Up"] = "r5e";
        rooms["r4e"] = r4e;

        var r5c = new RoomData("r5c");
        r5c.Doors["Down"] = "r4c";
        r5c.Doors["Up"] = "r6c";
        rooms["r5c"] = r5c;

        var r5e = new RoomData("r5e");
        r5e.Doors["Down"] = "r4e";
        r5e.Doors["Right"] = "r5f";
        rooms["r5e"] = r5e;

        var r5f = new RoomData("r5f");
        r5f.Doors["Left"] = "r5e";
        rooms["r5f"] = r5f;

        var r6c = new RoomData("r6c");
        r6c.Doors["Down"] = "r5c";
        r6c.Doors["Left"] = "r6b";
        rooms["r6c"] = r6c;

        var r6b = new RoomData("r6b");
        r6b.Doors["Right"] = "r6c";
        rooms["r6b"] = r6b;

        var r8c = new RoomData("r8c");

        // ✅ Explicitly define door visibility (draw only Right and Down)
        r8c.Doors["Up"] = null;
        r8c.Doors["Left"] = null;
        r8c.Doors["Right"] = "none";
        r8c.Doors["Down"] = "none";

        rooms["r8c"] = r8c;


        // Add more as needed
    }

    public RoomData GetRoom(string roomID)
    {
        return rooms.TryGetValue(roomID, out var room) ? room : null;
    }

    public IEnumerable<RoomData> GetAllRooms() => rooms.Values;
}