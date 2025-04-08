
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Items;
using sprint0Test.Sprites;
using sprint0Test.Managers;

namespace sprint0Test.Link1
{
    public class Link
    {
        private static Link instance; // Singleton instance

        private LinkSprite sprite;
        private Vector2 position;
        private float speed = 2f;
        private bool isAttacking = false;
        private bool isUsingItem = false;
        private int attackFrameCounter = 0;
        private int itemFrameCounter = 0;
        private int currentItemIndex = 0;
        private int currentHealth = 6;
        private List<Item> inventory = new List<Item>();

        private readonly int screenMinX = 0;
        private readonly int screenMinY = 0;
        private readonly int screenMaxX = 800;
        private readonly int screenMaxY = 480;

        private bool isVisible = true; // This will track visibility

        // ? Singleton access property
        public static Link Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("Link instance has not been initialized!");
                }
                return instance;
            }
        }

        public Vector2 Position => position;
        public IReadOnlyList<Item> Inventory => inventory.AsReadOnly();

        public bool IsVisible // Add IsVisible property
        {
            get => isVisible;
            set
            {
                isVisible = value;
                sprite.IsVisible = value; // If the sprite also needs to reflect visibility
            }
        }

        // Method to set position (since Position is read-only)
        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        // ? Private constructor to prevent direct instantiation
        private Link(LinkSprite linkSprite, Vector2 startPos)
        {
            sprite = linkSprite;
            position = startPos;
            sprite.Scale = 2f;
            sprite.SetState(LinkAction.Idle, LinkDirection.Down);
        }

        // ? Public method to initialize the singleton
        public static void Initialize(LinkSprite sprite, Vector2 startPos)
        {
            if (instance == null)
            {
                instance = new Link(sprite, startPos);
            }
            else
            {
                throw new InvalidOperationException("Link has already been initialized!");
            }
        }

        public void MoveUp() => Move(LinkDirection.Up);
        public void MoveDown() => Move(LinkDirection.Down);
        public void MoveLeft() => Move(LinkDirection.Left);
        public void MoveRight() => Move(LinkDirection.Right);

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

                Vector2 direction = position;
                int offset = 45;
                if (sprite.CurrentDirection == LinkDirection.Up)
                {
                    direction.Y -= offset;
                    //Console.WriteLine("Attack UP");
                }
                if (sprite.CurrentDirection == LinkDirection.Down)
                {
                    direction.Y += offset;
                    //Console.WriteLine("Attack Down");
                }
                if (sprite.CurrentDirection == LinkDirection.Left)
                {
                    direction.X -= offset;
                    //Console.WriteLine("Attack Left");
                }
                if (sprite.CurrentDirection == LinkDirection.Right)
                {
                    direction.X += offset;
                    //Console.WriteLine("Attack Right");
                }
                ProjectileManager.Instance.SpawnProjectile(direction, direction, "Sword");
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

        // int damage
        public void TakeDamage()
        {
            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Damaged, sprite.CurrentDirection);
            }
            // currentHealth -= damage;
            // Larry's code
            Game1.Instance.HandlePlayerDamage();

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

        public Vector2 GetScaledDimensions()
        {
            return sprite.GetScaledDimensions(); // Forward call to LinkSprite
        }

    }
}
