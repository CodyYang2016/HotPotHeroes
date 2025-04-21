using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Link1;
using sprint0Test;
using System.Linq;

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
        private Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap;
        private List<Texture2D> currentFrames = new List<Texture2D>();
        private int currentFrameIndex;
        private int frameCounter;

        private int framesPerImage = 8;
        // Original dimensions (unscaled) for Idle frames in each direction
        private readonly Dictionary<LinkDirection, Vector2> baselineSize;

        private bool isVisible = true; // Track visibility

        public float Scale { get; set; } = 1f;
        public LinkAction CurrentAction { get; private set; }
        public LinkDirection CurrentDirection { get; private set; }

        // IsVisible property
        public bool IsVisible
        {
            get => isVisible;
            set => isVisible = value;
        }

        public LinkSprite(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> map)
        {
            spriteMap = map;
            // Calculate Idle dimensions for each direction
            baselineSize = new Dictionary<LinkDirection, Vector2>();
            foreach (LinkDirection dir in Enum.GetValues(typeof(LinkDirection)))
            {
                if (spriteMap.TryGetValue((LinkAction.Idle, dir), out var frames) && frames.Count > 0)
                {
                    var tex = frames[0];
                    baselineSize[dir] = new Vector2(tex.Width, tex.Height);
                }
                else
                {
                    baselineSize[dir] = Vector2.Zero;
                }
            }

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
            // Idle state doesn't animate
            if (CurrentAction == LinkAction.Idle)
                return;

            if (currentFrames.Count == 0)
                return;

            frameCounter++;
            if (frameCounter >= framesPerImage)
            {
                frameCounter = 0;
                currentFrameIndex = (currentFrameIndex + 1) % currentFrames.Count;
            }
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!isVisible) return;

            // Current frame texture
            Texture2D currentTex = currentFrames[currentFrameIndex];
            // Scaled dimensions
            Vector2 currSize = new Vector2(currentTex.Width, currentTex.Height) * Scale;
            // Baseline Idle dimensions for current direction (scaled)
            Vector2 baseSize = baselineSize[CurrentDirection] * Scale;
            // Adjustment: Down attack sprite has extra height, manually set baselineSize to align with feet
            if (CurrentAction == LinkAction.Attacking && CurrentDirection == LinkDirection.Down)
            {
                baseSize.Y = currentTex.Height * Scale; // Make sprite touch the ground
            }

            // Calculate offset: center horizontally, align bottom vertically
            float offsetX = (baseSize.X - currSize.X) / 2f;
            float offsetY = baseSize.Y - currSize.Y;

            Vector2 drawPos = position + new Vector2(offsetX, offsetY);

            spriteBatch.Draw(
                currentTex,
                drawPos,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
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

