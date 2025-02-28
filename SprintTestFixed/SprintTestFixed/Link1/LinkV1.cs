using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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

    public class LinkV1
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

      
        private int screenMinX = 0;
        private int screenMinY = 0;
        private int screenMaxX = 800; 
        private int screenMaxY = 480; 

        public LinkV1(LinkSprite linkSprite, Vector2 startPos)
        {
            sprite = linkSprite;
            position = startPos;

            sprite.Scale = 2f;

            sprite.SetState(LinkAction.Idle, LinkDirection.Down);
        }

        public LinkV1()
        {
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

        public void Update()
        {
          
            sprite.Update();

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

           
            Vector2 scaledSize = sprite.GetScaledDimensions();
            float linkWidth = scaledSize.X;
            float linkHeight = scaledSize.Y;

            if (position.X < screenMinX) position.X = screenMinX;
            if (position.Y < screenMinY) position.Y = screenMinY;
           
            if (position.X > screenMaxX - linkWidth)
                position.X = screenMaxX - linkWidth;
            if (position.Y > screenMaxY - linkHeight)
                position.Y = screenMaxY - linkHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            sprite.Draw(spriteBatch, position);
        }
    }
}


