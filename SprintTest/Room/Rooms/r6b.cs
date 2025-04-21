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
    public class r6b : AbstractRoom
    {
        public r6b(RoomData data)
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
                Enemies.Add(EnemyManager.Instance.CreateStalfos(new Vector2(550, 300)));
                Enemies.Add(EnemyManager.Instance.CreateStalfos(new Vector2(550, 100)));

                Enemies.Add(EnemyManager.Instance.CreateStalfos(new Vector2(100, 250)));
                Enemies.Add(EnemyManager.Instance.CreateStalfos(new Vector2(100, 100)));
                Enemies.Add(EnemyManager.Instance.CreateStalfos(new Vector2(100, 150)));

                Enemies.Add(EnemyManager.Instance.CreateStalfos(new Vector2(300, 100)));
            }

            
            BlockManager.Instance.CreateBlock(new Vector2(225,170), BlockType.Block);
            
            BlockManager.Instance.CreateBlock(new Vector2(225,300), BlockType.Block);

            BlockManager.Instance.CreateBlock(new Vector2(400,170), BlockType.Block);
            
            BlockManager.Instance.CreateBlock(new Vector2(400,300), BlockType.Block);
            
        }
    }

}
