using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test;

namespace HotpotHeroes.sprint0Game.sprint0Test.Managers
{
    public class BlockManager
    {
        private static BlockManager _instance;
        public static BlockManager Instance => _instance ??= new BlockManager();

        private List<IBlock> blockPool; // Pool of all possible blocks
        private IBlock activeBlock; // The current active block
        private int activeBlockIndex = 0;

        public BlockManager()
        {
            blockPool = new List<IBlock>
            {
                // new Octorok(new Vector2(100, 100)),
                // new Octorok(new Vector2(200, 100)),
                // new Aquamentus(new Vector2(300, 100))
                // new Moblin(new Vector2(200, 200)) // Add more enemies as needed
            };

            activeBlock = blockPool[0]; // Default first enemy
        }

        public void SpawnBlock()
        {
            if (blockPool.Count == 0)
                return;

            activeBlock = blockPool[activeBlockIndex]; // Set the current enemy
        }

        public void NextBlock()
        {
            if (blockPool.Count > 0)
            {
                activeBlockIndex = (activeBlockIndex + 1) % blockPool.Count;
                activeBlock = blockPool[activeBlockIndex]; // Set new active enemy
            }
        }

        public void PreviousBlock()
        {
            if (blockPool.Count > 0)
            {
                activeBlockIndex = (activeBlockIndex - 1 + blockPool.Count) % blockPool.Count;
                activeBlock = blockPool[activeBlockIndex]; // Set new active enemy
            }
        }

        public IBlock GetActiveBlock()
        {
            return activeBlock;
        }

        public void Update(GameTime gameTime)
        {
            activeBlock?.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            activeBlock?.Draw(spriteBatch);


        }
    }
}
