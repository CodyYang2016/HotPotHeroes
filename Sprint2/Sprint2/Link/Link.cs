using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0Test
{
    public enum LinkDirection
    {
        Up, Down, Left, Right
    }

    public enum LinkAction
    {
        Idle, Walking, Attacking, Damaged, UsingItem
    }

    public class Link
    {
        private LinkSprite sprite;
        private Vector2 position;
        // �����ٶ�
        private float speed = 2f;

        // �Ƿ����ڹ���
        private bool isAttacking = false;
        // �Ƿ�����ʹ����Ʒ
        private bool isUsingItem = false;

        // ����֡������
        private int attackFrameCounter = 0;
        // ʹ����Ʒ֡������
        private int itemFrameCounter = 0;

        // ��ǰѡ�е���Ʒ����
        private int currentItemIndex = 0;
        // ��Ʒ���ϣ���Ϊ�ջ��һЩ������Ʒ��
        private List<Item> inventory = new List<Item>();

        // �򵥵���Ļ�߽磬�ɰ����޸�
        private int screenMinX = 0;
        private int screenMinY = 0;
        private int screenMaxX = 800; // ��Ļ��
        private int screenMaxY = 480; // ��Ļ��

        public Link(LinkSprite linkSprite, Vector2 startPos)
        {
            sprite = linkSprite;
            position = startPos;

            // ��������� Link �Ŵ󣬱��� 2 ��
            sprite.Scale = 2f;

            sprite.SetState(LinkAction.Idle, LinkDirection.Down);
        }

        public void MoveUp()
        {
            if (!isAttacking && !isUsingItem)
            {
                if (sprite.CurrentAction != LinkAction.Walking || sprite.CurrentDirection != LinkDirection.Up)
                {
                    sprite.SetState(LinkAction.Walking, LinkDirection.Up);
                }
                position.Y -= speed;
            }
        }

        public void MoveDown()
        {
            if (!isAttacking && !isUsingItem)
            {
                if (sprite.CurrentAction != LinkAction.Walking || sprite.CurrentDirection != LinkDirection.Down)
                {
                    sprite.SetState(LinkAction.Walking, LinkDirection.Down);
                }
                position.Y += speed;
            }
        }

        public void MoveLeft()
        {
            if (!isAttacking && !isUsingItem)
            {
                if (sprite.CurrentAction != LinkAction.Walking || sprite.CurrentDirection != LinkDirection.Left)
                {
                    sprite.SetState(LinkAction.Walking, LinkDirection.Left);
                }
                position.X -= speed;
            }
        }

        public void MoveRight()
        {
            if (!isAttacking && !isUsingItem)
            {
                if (sprite.CurrentAction != LinkAction.Walking || sprite.CurrentDirection != LinkDirection.Right)
                {
                    sprite.SetState(LinkAction.Walking, LinkDirection.Right);
                }
                position.X += speed;
            }
        }

        // �����û�а������ʱ���͵��� Stop()
        public void Stop()
        {
            if (!isAttacking && !isUsingItem)
            {
                if (sprite.CurrentAction != LinkAction.Idle)
                {
                    sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                }
            }
        }

        // ����
        public void Attack()
        {
            if (!isAttacking && !isUsingItem)
            {
                isAttacking = true;
                attackFrameCounter = 0;
                sprite.SetState(LinkAction.Attacking, sprite.CurrentDirection);
            }
        }

        // ʹ����Ʒ
        public void UseItem()
        {
            if (!isAttacking && !isUsingItem && inventory.Count > 0)
            {
                isUsingItem = true;
                itemFrameCounter = 0;
                sprite.SetState(LinkAction.UsingItem, sprite.CurrentDirection);
                inventory[currentItemIndex].Use();
            }
        }

        // ����
        public void TakeDamage()
        {
            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Damaged, sprite.CurrentDirection);
            }
        }

        // �л���Ʒ
        public void SwitchItem(int direction)
        {
            if (inventory.Count > 0)
            {
                currentItemIndex = (currentItemIndex + direction + inventory.Count) % inventory.Count;
            }
        }

        public void Update()
        {
            // �ȸ��¶���
            sprite.Update();

            // ������������ 32 ֡ (4 ֡ �� ÿ֡ 8 �� = 32)
            if (isAttacking)
            {
                attackFrameCounter++;
                if (attackFrameCounter > 32)
                {
                    isAttacking = false;
                    sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                    attackFrameCounter = 0;
                }
            }

            // ʹ����Ʒ����ʾ��: ���� 20 ֡
            if (isUsingItem)
            {
                itemFrameCounter++;
                if (itemFrameCounter > 20)
                {
                    isUsingItem = false;
                    sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
                    itemFrameCounter = 0;
                }
            }

            // === �߽���� ===
            // ��ȡ��ǰ֡��ʵ�ʿ�� (���ǵ� Scale)
            Vector2 scaledSize = sprite.GetScaledDimensions();
            float linkWidth = scaledSize.X;
            float linkHeight = scaledSize.Y;

            // ��� & �ϱ�
            if (position.X < screenMinX) position.X = screenMinX;
            if (position.Y < screenMinY) position.Y = screenMinY;

            // �ұ� & �±� (��֤���½ǲ�������Ļ)
            if (position.X > screenMaxX - linkWidth)
                position.X = screenMaxX - linkWidth;
            if (position.Y > screenMaxY - linkHeight)
                position.Y = screenMaxY - linkHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // ֱ�ӵ��� sprite �� Draw�������Զ��Ŵ�
            sprite.Draw(spriteBatch, position);
        }
    }
}


