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
        // 每个方向的 Idle 帧原始尺寸（未缩放）
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
            // 计算每个方向的 Idle 尺寸
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
            // 静止（Idle）状态不走动画
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
            // 当前帧贴图
            Texture2D currentTex = currentFrames[currentFrameIndex];
            // 缩放后尺寸
            Vector2 currSize = new Vector2(currentTex.Width, currentTex.Height) * Scale;
            // 对应方向的基准 Idle 尺寸（缩放后）
            Vector2 baseSize = baselineSize[CurrentDirection] * Scale;
            // 修正：向下攻击的贴图高度特别大，手动设置 baselineSize 以脚底为准
            if (CurrentAction == LinkAction.Attacking && CurrentDirection == LinkDirection.Down)
            {
                baseSize.Y = currentTex.Height * Scale; // 让贴图贴地
            }

            // 计算偏移：水平居中、垂直底部对齐
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
