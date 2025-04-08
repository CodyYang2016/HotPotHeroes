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
    public class r4b : AbstractRoom
    {
        
        public r4b(RoomData data)
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
                Enemies.Add(EnemyManager.Instance.CreateMoblin(new Vector2(200, 300)));
                Enemies.Add(EnemyManager.Instance.CreateDarknut(new Vector2(200, 200)));
                Enemies.Add(EnemyManager.Instance.CreateMoblin(new Vector2(400, 200)));
                Enemies.Add(EnemyManager.Instance.CreateDarknut(new Vector2(400, 250)));
                Enemies.Add(EnemyManager.Instance.CreateMoblin(new Vector2(100, 200)));
                Enemies.Add(EnemyManager.Instance.CreateDarknut(new Vector2(100, 350)));
            }

            // (Item and block spawning can stay or follow similar logic)
            
            BlockManager.Instance.CreateBlock(new Vector2(225,220), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(225,270), BlockType.Block);

            BlockManager.Instance.CreateBlock(new Vector2(275,220), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(275,270), BlockType.Block);

            BlockManager.Instance.CreateBlock(new Vector2(325,220), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(325,270), BlockType.Block);
            
        }
    }

}
