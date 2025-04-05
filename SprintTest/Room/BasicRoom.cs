using sprint0Test.Managers;
using sprint0Test;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintTest.Room
{
    public class BasicRoom : AbstractRoom
    {
        public BasicRoom(RoomData data)
        {
            RoomID = data.RoomID;
            RoomData = data;

            foreach (var kvp in data.Doors)
            {
                if (!string.IsNullOrEmpty(kvp.Value))
                    AdjacentRooms[kvp.Key] = kvp.Value;
            }

            // You can also set default DoorHitboxes here
        }

        public override void Initialize()
        {
            base.Initialize();

            // Setup generic hitboxes
            DoorHitboxes["Right"] = new Rectangle(750, 300, 32, 64);
            DoorHitboxes["Left"] = new Rectangle(0, 300, 32, 64);
            DoorHitboxes["Up"] = new Rectangle(384, 0, 32, 64);
            DoorHitboxes["Down"] = new Rectangle(384, 544, 32, 64);


            // Optional: Add enemies/items if not cleared
            if (!RoomData.HasBeenCleared)
            {
                Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(300, 300)));
                BlockManager.Instance.CreateBlock(new Vector2(100, 200), BlockType.Brick);
            }
        }
    }

}
