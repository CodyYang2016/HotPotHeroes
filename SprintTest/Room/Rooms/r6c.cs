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
    public class r6c : AbstractRoom
    {
        public r6c(RoomData data)
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
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(550, 300)));
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(550, 250)));
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(550, 100)));
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(550, 150)));
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(100, 250)));
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(100, 100)));
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(100, 150)));
                Enemies.Add(EnemyManager.Instance.CreateOctorok(new Vector2(300, 100)));
            }
            BlockManager.Instance.CreateBlock(new Vector2(350, 200), BlockType.Sand, 3f, true, false);
            BlockManager.Instance.CreateBlock(new Vector2(350, 250), BlockType.Sand, 3f, true, false);
            BlockManager.Instance.CreateBlock(new Vector2(350, 300), BlockType.Sand, 3f, true, false);
            BlockManager.Instance.CreateBlock(new Vector2(350, 350), BlockType.Sand, 3f, true, false);
            BlockManager.Instance.CreateBlock(new Vector2(300, 200), BlockType.Sand, 3f, true, false);
            BlockManager.Instance.CreateBlock(new Vector2(300, 250), BlockType.Sand, 3f, true, false);
            BlockManager.Instance.CreateBlock(new Vector2(300, 300), BlockType.Sand, 3f, true, false);
            BlockManager.Instance.CreateBlock(new Vector2(300, 350), BlockType.Sand, 3f, true, false);

        }
    }

}
