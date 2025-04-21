using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Enemy;
using sprint0Test.Items;
using sprint0Test.Managers;
using sprint0Test.Sprites;
using System;
using sprint0Test.Enemy;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using sprint0Test.Audio;

namespace sprint0Test.Dungeon
{
    public class r5e : AbstractRoom
    {
        public r5e(RoomData data)
        {
            RoomID = data.RoomID;
            RoomData = data; 
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
                AudioManager.Instance.SetSong(SongList.Boss);
                Enemies.Add(EnemyManager.Instance.CreateAquamentus(new Vector2(500, 235)));
            }

            BlockManager.Instance.CreateBlock(new Vector2(400,80), BlockType.Black);
            BlockManager.Instance.CreateBlock(new Vector2(400,340), BlockType.Black);
            BlockManager.Instance.CreateBlock(new Vector2(450,80), BlockType.Black);
            BlockManager.Instance.CreateBlock(new Vector2(450,340), BlockType.Black);
            // BlockManager.Instance.CreateBlock(new Vector2(500,80), BlockType.Black);
            // BlockManager.Instance.CreateBlock(new Vector2(500,340), BlockType.Black);
            // BlockManager.Instance.CreateBlock(new Vector2(550,80), BlockType.Black);
            // BlockManager.Instance.CreateBlock(new Vector2(550,340), BlockType.Black);

            
            BlockManager.Instance.CreateBlock(new Vector2(100,170), BlockType.Fish);
            BlockManager.Instance.CreateBlock(new Vector2(100,220), BlockType.Fish);
            BlockManager.Instance.CreateBlock(new Vector2(100,270), BlockType.Fish);


        }
    }

}
