using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Link1;

namespace sprint0Test.Link1

{
    public class LinkSprite
    {
        // 映射: (LinkAction, LinkDirection) -> 该动作/方向下的一组帧
        private Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap;
        private List<Texture2D> currentFrames;
        private int currentFrameIndex;
        private int frameCounter;

        // 每张帧图片停留的更新次数 (越小动画越快)
        private int framesPerImage = 8;

        // 可以通过这个 Scale 控制 Link 的放大倍数，默认是 1f
        public float Scale { get; set; } = 1f;

        public LinkAction CurrentAction { get; private set; }
        public LinkDirection CurrentDirection { get; private set; }

        public LinkSprite(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> map)
        {
            spriteMap = map;

            // 默认初始为 Idle, Face Down
            CurrentAction = LinkAction.Idle;
            CurrentDirection = LinkDirection.Down;
            if (!spriteMap.TryGetValue((CurrentAction, CurrentDirection), out currentFrames))
            {
                Console.WriteLine($"ERROR: Key ({CurrentAction}, {CurrentDirection}) not found in spriteMap!");
                currentFrames = new List<Texture2D>(); // Assign an empty list to prevent crashes
            }

            // currentFrames = spriteMap[(CurrentAction, CurrentDirection)];
        }

        // 设置新的动作/方向时重置动画帧索引
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
            // 计数器超过 framesPerImage，就切到下一帧
            frameCounter++;
            if (frameCounter > framesPerImage)
            {
                frameCounter = 0;
                currentFrameIndex++;
                // 如果超出帧数量
                if (currentFrameIndex >= currentFrames.Count)
                {
                    // 攻击动作可以让它停在最后一帧，也可以循环，看你需求
                    if (CurrentAction == LinkAction.Attacking)
                    {
                        currentFrameIndex = currentFrames.Count - 1;
                    }
                    else
                    {
                        // 其余动作(如行走)循环
                        currentFrameIndex = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            // 使用 spriteBatch.Draw 的缩放参数
            spriteBatch.Draw(
                currentTex,
                position,
                null,                   // 不裁剪
                Color.White,
                0f,                     // 无旋转
                Vector2.Zero,           // 旋转中心
                Scale,                  // 缩放倍数
                SpriteEffects.None,
                0f                      // 图层深度
            );
        }

        // 获取当前帧贴图 * 缩放后的尺寸，用于碰撞或屏幕边界判断
        public Vector2 GetScaledDimensions()
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            float width = currentTex.Width * Scale;
            float height = currentTex.Height * Scale;
            return new Vector2(width, height);
        }
    }
}
