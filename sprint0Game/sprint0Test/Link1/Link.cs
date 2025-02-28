using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace sprint0Test.Link1
{
    public class Link
    {
        private LinkSprite sprite;
        private Vector2 position;
        private float speed = 2f;
        private bool isAttacking = false;
        private bool isUsingItem = false;
        private int attackFrameCounter = 0;
        private int itemFrameCounter = 0;
        private int currentItemIndex = 0;
        private List<Item> inventory = new List<Item>();

        private readonly int screenMinX = 0;
        private readonly int screenMinY = 0;
        private readonly int screenMaxX = 800;
        private readonly int screenMaxY = 480;

        public Vector2 Position => position;
        public IReadOnlyList<Item> Inventory => inventory.AsReadOnly();

        public float Speed = 2f;
        
        
        public Link(LinkSprite linkSprite, Vector2 startPos)
        {
            sprite = linkSprite;
            position = startPos;
            sprite.Scale = 2f;
            sprite.SetState(LinkAction.Idle, LinkDirection.Down);
        }

        public void MoveUp()
        {
            Move(LinkDirection.Up);
        }

        public void MoveDown()
        {
            Move(LinkDirection.Down);
        }

        public void MoveLeft()
        {
            Move(LinkDirection.Left);
        }

        public void MoveRight()
        {
            Move(LinkDirection.Right);
        }

        private void Move(LinkDirection direction)
        {
            if (isAttacking || isUsingItem) return;
            
            sprite.SetState(LinkAction.Walking, direction);
            switch (direction)
            {
                case LinkDirection.Up: position.Y -= speed; break;
                case LinkDirection.Down: position.Y += speed; break;
                case LinkDirection.Left: position.X -= speed; break;
                case LinkDirection.Right: position.X += speed; break;
            }
        }

        public void Stop()
        {
            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
            }
        }

        public void Attack()
        {
            if (!isAttacking && !isUsingItem)
            {
                isAttacking = true;
                attackFrameCounter = 0;
                sprite.SetState(LinkAction.Attacking, sprite.CurrentDirection);
            }
        }

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

        public void TakeDamage()
        {
            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Damaged, sprite.CurrentDirection);
            }
        }

        public void SwitchItem(int direction)
        {
            if (inventory.Count > 0)
            {
                currentItemIndex = (currentItemIndex + direction + inventory.Count) % inventory.Count;
            }
        }

        public void AddItem(Item item)
        {
            inventory.Add(item);
        }

        public void Update()
        {
            sprite.Update();

            if (isAttacking && ++attackFrameCounter > 32)
            {
                isAttacking = false;
                sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
            }

            if (isUsingItem && ++itemFrameCounter > 20)
            {
                isUsingItem = false;
                sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
            }

            Vector2 scaledSize = sprite.GetScaledDimensions();
            position.X = MathHelper.Clamp(position.X, screenMinX, screenMaxX - scaledSize.X);
            position.Y = MathHelper.Clamp(position.Y, screenMinY, screenMaxY - scaledSize.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position);
        }
    }
}
