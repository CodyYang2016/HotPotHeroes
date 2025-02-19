using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Link1;

namespace sprint0Test.Link1

{
    public class LinkSprite
    {
        // ӳ��: (LinkAction, LinkDirection) -> �ö���/�����µ�һ��֡
        private Dictionary<(LinkAction, LinkDirection), List<Texture2D>> spriteMap;
        private List<Texture2D> currentFrames;
        private int currentFrameIndex;
        private int frameCounter;

        // ÿ��֡ͼƬͣ���ĸ��´��� (ԽС����Խ��)
        private int framesPerImage = 8;

        // ����ͨ����� Scale ���� Link �ķŴ�����Ĭ���� 1f
        public float Scale { get; set; } = 1f;

        public LinkAction CurrentAction { get; private set; }
        public LinkDirection CurrentDirection { get; private set; }

        public LinkSprite(Dictionary<(LinkAction, LinkDirection), List<Texture2D>> map)
        {
            spriteMap = map;

            // Ĭ�ϳ�ʼΪ Idle, Face Down
            CurrentAction = LinkAction.Idle;
            CurrentDirection = LinkDirection.Down;
            if (!spriteMap.TryGetValue((CurrentAction, CurrentDirection), out currentFrames))
            {
                Console.WriteLine($"ERROR: Key ({CurrentAction}, {CurrentDirection}) not found in spriteMap!");
                currentFrames = new List<Texture2D>(); // Assign an empty list to prevent crashes
            }

            // currentFrames = spriteMap[(CurrentAction, CurrentDirection)];
        }

        // �����µĶ���/����ʱ���ö���֡����
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
            // ���������� framesPerImage�����е���һ֡
            frameCounter++;
            if (frameCounter > framesPerImage)
            {
                frameCounter = 0;
                currentFrameIndex++;
                // �������֡����
                if (currentFrameIndex >= currentFrames.Count)
                {
                    // ����������������ͣ�����һ֡��Ҳ����ѭ������������
                    if (CurrentAction == LinkAction.Attacking)
                    {
                        currentFrameIndex = currentFrames.Count - 1;
                    }
                    else
                    {
                        // ���ද��(������)ѭ��
                        currentFrameIndex = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            // ʹ�� spriteBatch.Draw �����Ų���
            spriteBatch.Draw(
                currentTex,
                position,
                null,                   // ���ü�
                Color.White,
                0f,                     // ����ת
                Vector2.Zero,           // ��ת����
                Scale,                  // ���ű���
                SpriteEffects.None,
                0f                      // ͼ�����
            );
        }

        // ��ȡ��ǰ֡��ͼ * ���ź�ĳߴ磬������ײ����Ļ�߽��ж�
        public Vector2 GetScaledDimensions()
        {
            Texture2D currentTex = currentFrames[currentFrameIndex];
            float width = currentTex.Width * Scale;
            float height = currentTex.Height * Scale;
            return new Vector2(width, height);
        }
    }
}
