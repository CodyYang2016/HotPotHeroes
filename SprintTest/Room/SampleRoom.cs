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
    public class SampleRoom : AbstractRoom
    {
        public SampleRoom(string id)
        {
            RoomID = id;


            // For demo purposes. In practice, RoomManager can also assign these.
            AdjacentRooms["Right"] = "1b";
            AdjacentRooms["Down"] = "2a";
        }

        public override void Initialize()
        {
            base.Initialize();
            DoorHitboxes["Right"] = new Rectangle(750, 300, 32, 64);

            var octorokTextures = new Dictionary<string, Texture2D>
            {
                { "Octopus_Idle1", TextureManager.Instance.GetTexture("Octopus_Idle1") },
                { "Octopus_Idle2", TextureManager.Instance.GetTexture("Octopus_Idle2") }
            };
            
            var Keese_textures = new Dictionary<string, Texture2D>
            {
                { "Bat_1", TextureManager.Instance.GetTexture("Bat_1") },
                { "Bat_2", TextureManager.Instance.GetTexture("Bat_2") }
            };


            // === Spawn Enemies ===
            Enemies.Add(new Octorok(new Vector2(300, 300), octorokTextures));
            Enemies.Add(new Keese(new Vector2(200, 200), Keese_textures));


            // === Spawn Items ===
            Texture2D appleTexture = TextureManager.Instance.GetTexture("Apple");
            Texture2D heartTexture = TextureManager.Instance.GetTexture("Heart");

            // Items.Add(new Apple("Apple", appleTexture, new Vector2(300, 120)));
           //  Items.Add(new Heart("Heart", heartTexture, new Vector2(320, 160)));

            // === Add Blocks ===
            
            BlockManager.Instance.CreateBlock(new Vector2(200, 200), BlockType.Dragon);
            Console.WriteLine($"Blocks count: {BlockManager.Instance.GetBlocksCount()}"); // Or something similar

            // === Create the Perimeter ===
            Perimeter perimeter = new Perimeter(); // This will automatically add blocks to the room's perimeter
        }
    }
}
