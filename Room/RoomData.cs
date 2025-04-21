using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework; // ✅

public class RoomData
{
    public string RoomID { get; set; }

    public bool HasBeenCleared { get; set; } = false;


    // "dungeon", "ice", "fire", etc. — texture key or category
    public string BackgroundTextureKey { get; set; } = "dungeon";

    // Optional: You could also add these if you want separate wall/door styles
    public string WallTextureKey { get; set; } = "default";
    public string DoorTextureKey { get; set; } = "default";

    public Dictionary<string, string> Doors { get; private set; }

    public bool[,] CollisionMap { get; set; }


    public RoomData(string roomID)
    {
        RoomID = roomID;
        Doors = new Dictionary<string, string>
        {
            { "Up", null },
            { "Down", null },
            { "Left", null },
            { "Right", null }
        };
    }
    public static readonly Rectangle ExteriorSource = new Rectangle(521, 11, 256, 176);
    public static readonly Rectangle ExteriorDest = new Rectangle(0, 0, 256, 176);

    // Sample interiors (you can expand these)
    //196,303, 192, 112
    public static readonly Rectangle InteriorDest = new Rectangle(32, 32, 192, 115);
    public static readonly Rectangle InteriorSource = new Rectangle(0, 195, 192, 112);
    public static readonly Rectangle Door_Top = new Rectangle(849, 11, 32, 32);
    public static readonly Rectangle Door_Left = new Rectangle(849, 44, 32, 32);
    public static readonly Rectangle Door_Bottom = new Rectangle(849, 110, 32, 32);
    public static readonly Rectangle Door_Right = new Rectangle(849, 77, 32, 32);
    public static readonly Rectangle Not_Door_Top = new Rectangle(817,11, 32, 32);
    public static readonly Rectangle Not_Door_Left = new Rectangle(817, 44, 32, 32);
    public static readonly Rectangle Not_Door_Bottom = new Rectangle(817, 110, 32, 32);
    public static readonly Rectangle Not_Door_Right = new Rectangle(817, 77, 32, 32);
    public static readonly Rectangle Top_Dest = new Rectangle(112, 0, 32, 32);  // centered horizontally
    public static readonly Rectangle Bottom_Dest = new Rectangle(112, 144, 32, 32);  // same X as Top

    public static readonly Rectangle Left_Dest = new Rectangle(0, 72, 32, 32);   // centered vertically
    public static readonly Rectangle Right_Dest = new Rectangle(224, 72, 32, 32);   // same Y as Left

}
