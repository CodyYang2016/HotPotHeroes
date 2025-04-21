using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Enemy;
using sprint0Test.Items;
using sprint0Test.Managers;
using sprint0Test.Sprites;
using System;
using sprint0Test.Enemy;
using System.Collections.Generic;

namespace sprint0Test.Dungeon
{
    public class r5f : AbstractRoom
    {
        public r5f(RoomData data)
        {
            RoomID = data.RoomID;
            RoomData = data; // ✅ GOOD: keeps all door info
        }

        public override void Initialize()
        {


            // Clear the enemies list 
            // if isCleared == FALSE:
            // Enemies.ADD
            // else:
            // Add nothing 
            base.Initialize();


            if (!RoomData.HasBeenCleared)
            {

            }

            // (Item and block spawning can stay or follow similar logic)
        

            Texture2D appleTexture = TextureManager.Instance.GetTexture("Apple");
            Texture2D heartTexture = TextureManager.Instance.GetTexture("Heart");

            // Items.Add(new Apple("Apple", appleTexture, new Vector2(300, 120)));
            // Items.Add(new Heart("Heart", heartTexture, new Vector2(320, 160)));
        }
    }

}
