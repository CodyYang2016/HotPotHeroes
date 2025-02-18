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
        // 行走速度
        private float speed = 2f;

        // 是否正在攻击
        private bool isAttacking = false;
        // 是否正在使用物品
        private bool isUsingItem = false;

        // 攻击帧计数器
        private int attackFrameCounter = 0;
        // 使用物品帧计数器
        private int itemFrameCounter = 0;

        // 当前选中的物品索引
        private int currentItemIndex = 0;
        // 物品集合（可为空或加一些测试物品）
        private List<Item> inventory = new List<Item>();

        // 简单的屏幕边界，可按需修改
        private int screenMinX = 0;
        private int screenMinY = 0;
        private int screenMaxX = 800; // 屏幕宽
        private int screenMaxY = 480; // 屏幕高

        public Link(LinkSprite linkSprite, Vector2 startPos)
        {
            sprite = linkSprite;
            position = startPos;

            // 如果你想让 Link 放大，比如 2 倍
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

        // 当玩家没有按方向键时，就调用 Stop()
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

        // 攻击
        public void Attack()
        {
            if (!isAttacking && !isUsingItem)
            {
                isAttacking = true;
                attackFrameCounter = 0;
                sprite.SetState(LinkAction.Attacking, sprite.CurrentDirection);
            }
        }

        // 使用物品
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

        // 受伤
        public void TakeDamage()
        {
            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Damaged, sprite.CurrentDirection);
            }
        }

        // 切换物品
        public void SwitchItem(int direction)
        {
            if (inventory.Count > 0)
            {
                currentItemIndex = (currentItemIndex + direction + inventory.Count) % inventory.Count;
            }
        }

        public void Update()
        {
            // 先更新动画
            sprite.Update();

            // 攻击动画持续 32 帧 (4 帧 × 每帧 8 次 = 32)
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

            // 使用物品动画示例: 持续 20 帧
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

            // === 边界控制 ===
            // 获取当前帧的实际宽高 (考虑到 Scale)
            Vector2 scaledSize = sprite.GetScaledDimensions();
            float linkWidth = scaledSize.X;
            float linkHeight = scaledSize.Y;

            // 左边 & 上边
            if (position.X < screenMinX) position.X = screenMinX;
            if (position.Y < screenMinY) position.Y = screenMinY;

            // 右边 & 下边 (保证右下角不超出屏幕)
            if (position.X > screenMaxX - linkWidth)
                position.X = screenMaxX - linkWidth;
            if (position.Y > screenMaxY - linkHeight)
                position.Y = screenMaxY - linkHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 直接调用 sprite 的 Draw，即可自动放大
            sprite.Draw(spriteBatch, position);
        }
    }
}


