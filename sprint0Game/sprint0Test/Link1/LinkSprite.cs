using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;

namespace sprint0Test.Link1

{
    public enum LinkDirection
    {
        Up, Down, Left, Right
    }

    public enum LinkAction
    {
        Idle, Walking, Attacking, Damaged, UsingItem
    }
    public class LinkSprite
    {
        // ӳ��: (LinkAction, LinkDirection) -> �ö���/�����µ�һ��֡
        private Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap;
        private List<Texture2D> currentFrames = new List<Texture2D>();
        private int currentFrameIndex;
        private int frameCounter;

        private int framesPerImage = 8;

        public float Scale { get; set; } = 1f;
        public LinkAction CurrentAction { get; private set; }
        public LinkDirection CurrentDirection { get; private set; }

        public LinkSprite(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> map)
        {
            spriteMap = map;
            SetState(LinkAction.Idle, LinkDirection.Down); // Initialize to Idle state
        }

        public void SetState(LinkAction action, LinkDirection dir)
        {
            CurrentAction = action;
            CurrentDirection = dir;

            if (!spriteMap.TryGetValue((CurrentAction, CurrentDirection), out currentFrames) || currentFrames.Count == 0)
            {
                Console.WriteLine($"ERROR: Key ({CurrentAction}, {CurrentDirection}) not found in spriteMap!");
                currentFrames = new List<Texture2D>(); // Assign an empty list to prevent crashes
            }
            currentFrameIndex = 0;
            frameCounter = 0;
        }

        public void Update()
        {
        if (currentFrames.Count == 0) return;
        frameCounter ++;
            if (frameCounter >= framesPerImage)
            {
                frameCounter = 0;
                //Loop through Frames
                currentFrameIndex = (currentFrameIndex + 1) % currentFrames.Count;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            spriteBatch.Draw(currentTex,position,null,Color.White,0f, Vector2.Zero,Scale, SpriteEffects.None,0f  
            );
        }

        public Vector2 GetScaledDimensions()
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            float width = currentTex.Width * Scale;
            float height = currentTex.Height * Scale;
            return new Vector2(width, height);
        }
    }
}
