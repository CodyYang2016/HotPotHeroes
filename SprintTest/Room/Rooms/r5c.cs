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
    public class r5c : AbstractRoom
    {
        public r5c(RoomData data)
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
                Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(200, 200)));
                Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(200, 300)));
                Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(550, 200)));
                Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(550, 250)));

                Enemies.Add(EnemyManager.Instance.CreateDarknut(new Vector2(300, 250)));
            }

            // (Item and block spawning can stay or follow similar logic)
        
            //BlockManager.Instance.CreateBlock(new Vector2(250,250), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(240,300), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(230,350), BlockType.Block);
            
            //BlockManager.Instance.CreateBlock(new Vector2(400,250), BlockType.Block);
            //BlockManager.Instance.CreateBlock(new Vector2(400,300), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(400,350), BlockType.Block);

            //BlockManager.Instance.CreateBlock(new Vector2(250,250), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(240,90), BlockType.Block);
            //BlockManager.Instance.CreateBlock(new Vector2(230,140), BlockType.Block);
            
            //BlockManager.Instance.CreateBlock(new Vector2(400,250), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(400,90), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(400,140), BlockType.Block);
        }
    }

}
