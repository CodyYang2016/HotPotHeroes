using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0Test
{
    public class LinkSprite
    {
        private Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap;
        private List<Texture2D> currentFrames;
        private int currentFrameIndex;
        private int frameCounter;
        private int framesPerImage = 8;

        public LinkAction CurrentAction { get; private set; }
        public LinkDirection CurrentDirection { get; private set; }

        public LinkSprite(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> map)
        {
            spriteMap = map;
            CurrentAction = LinkAction.Idle;
            CurrentDirection = LinkDirection.Down;
            currentFrames = spriteMap[(CurrentAction, CurrentDirection)];
        }

        public void SetState(LinkAction action, LinkDirection dir)
        {
            CurrentAction = action;
            CurrentDirection = dir;
            currentFrames = spriteMap[(action, dir)];
            currentFrameIndex = 0;
            frameCounter = 0;
        }

        public void Update()
        {
            frameCounter++;
            if (frameCounter > framesPerImage)
            {
                frameCounter = 0;
                currentFrameIndex++;
                if (currentFrameIndex >= currentFrames.Count)
                {
                    if (CurrentAction == LinkAction.Attacking)
                    {
                        currentFrameIndex = currentFrames.Count - 1;
                    }
                    else
                    {
                        currentFrameIndex = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            spriteBatch.Draw(currentTex, position, Color.White);
        }
    }
}
