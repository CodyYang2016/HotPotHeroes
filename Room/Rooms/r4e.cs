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
    public class r4e : AbstractRoom
    {
        public r4e(RoomData data)
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
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(300, 300)));
                Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(200, 200)));
            }

            BlockManager.Instance.CreateBlock(new Vector2(550, 70), BlockType.Dragon);

            BlockManager.Instance.CreateBlock(new Vector2(225, 120), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(225, 170), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(225, 220), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(225, 270), BlockType.Block);

            BlockManager.Instance.CreateBlock(new Vector2(275, 270), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(325, 270), BlockType.Block);
            BlockManager.Instance.CreateBlock(new Vector2(375, 270), BlockType.Block);
        }
    }

}