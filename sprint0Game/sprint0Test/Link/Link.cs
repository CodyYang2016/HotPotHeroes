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
        private float speed = 2f;
        private LinkDirection currentDir;
        private bool isAttacking = false;
        private bool isUsingItem = false;
        private int attackFrameCounter = 0;
        private int itemFrameCounter = 0;

        private int currentItemIndex = 0;
        private List<Item> inventory = new List<Item>();

        public Link(LinkSprite linkSprite, Vector2 startPos)
        {
            sprite = linkSprite;
            position = startPos;
            currentDir = LinkDirection.Down;
            sprite.SetState(LinkAction.Idle, currentDir);
        }

        public void MoveUp()
        {
            if (!isAttacking && !isUsingItem)
            {
                currentDir = LinkDirection.Up;
                sprite.SetState(LinkAction.Walking, currentDir);
                position.Y -= speed;
            }
        }

        public void MoveDown()
        {
            if (!isAttacking && !isUsingItem)
            {
                currentDir = LinkDirection.Down;
                sprite.SetState(LinkAction.Walking, currentDir);
                position.Y += speed;
            }
        }

        public void MoveLeft()
        {
            if (!isAttacking && !isUsingItem)
            {
                currentDir = LinkDirection.Left;
                sprite.SetState(LinkAction.Walking, currentDir);
                position.X -= speed;
            }
        }

        public void MoveRight()
        {
            if (!isAttacking && !isUsingItem)
            {
                currentDir = LinkDirection.Right;
                sprite.SetState(LinkAction.Walking, currentDir);
                position.X += speed;
            }
        }

        public void Stop()
        {
            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Idle, currentDir);
            }
        }

        public void Attack()
        {
            if (!isAttacking && !isUsingItem)
            {
                isAttacking = true;
                attackFrameCounter = 0;
                sprite.SetState(LinkAction.Attacking, currentDir);
            }
        }

        public void UseItem()
        {
            if (!isAttacking && !isUsingItem && inventory.Count > 0)
            {
                isUsingItem = true;
                itemFrameCounter = 0;
                sprite.SetState(LinkAction.UsingItem, currentDir);
                inventory[currentItemIndex].Use();
            }
        }

        public void TakeDamage()
        {
            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Damaged, currentDir);
            }
        }

        public void SwitchItem(int direction)
        {
            if (inventory.Count > 0)
            {
                currentItemIndex = (currentItemIndex + direction + inventory.Count) % inventory.Count;
            }
        }

        public void Update()
        {
            if (isAttacking)
            {
                attackFrameCounter++;
                if (attackFrameCounter > 20)
                {
                    isAttacking = false;
                    sprite.SetState(LinkAction.Idle, currentDir);
                }
            }

            if (isUsingItem)
            {
                itemFrameCounter++;
                if (itemFrameCounter > 20)
                {
                    isUsingItem = false;
                    sprite.SetState(LinkAction.Idle, currentDir);
                }
            }

            sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position);
        }
    }
}

