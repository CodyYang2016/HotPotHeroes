
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Items;
using sprint0Test.Sprites;
using sprint0Test.Managers;
using System.Diagnostics;
using static sprint0Test.Items.Bomb;

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
        public int currentItemIndex = 0;
        //private int currentHealth = 6;
        private int currentCrystal = 0;
        //private int currentBomb = 0;
        //private int currentApple = 0;
        //public int AppleCount => currentApple;
        public int CrystalCount => currentCrystal;
        //public int BombCount => currentBomb;
        public string CurrentSelectedItemName => inventoryKeys.Count > 0 ? inventoryKeys[currentItemIndex] : "";

        private Dictionary<string, List<IItem>> inventory = new();
        private List<string> inventoryKeys = new();

        public List<string> InventoryKeys => inventoryKeys;
        private RoomManager roomManager;
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
        private Link(LinkSprite linkSprite, Vector2 startPos, RoomManager roomManager)
        {
            sprite = linkSprite;
            position = startPos;
            this.roomManager = roomManager;
            sprite.Scale = 2f;
            sprite.SetState(LinkAction.Idle, LinkDirection.Down);
        }

        // ? Public method to initialize the singleton
        public static void Initialize(LinkSprite sprite, Vector2 startPos, RoomManager roomManager)
        {
            if (instance == null)
            {
                instance = new Link(sprite, startPos, roomManager);
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
            // Early exit if inventory is empty or index is out of range
            if (inventoryKeys.Count == 0 || currentItemIndex >= inventoryKeys.Count)
                return;

            string CurrentSelectedItemName = inventoryKeys[currentItemIndex];

            // Double-check that this key exists and has items
            if (!inventory.ContainsKey(CurrentSelectedItemName) || inventory[CurrentSelectedItemName].Count == 0)
                return;

            // Use the item
            IItem item = inventory[CurrentSelectedItemName][0];
            item.Use();

            // Special case: planted bomb should be added to room
            if (item is Bomb bomb && bomb.State == Bomb.BombState.Planted)
            {
                roomManager.CurrentRoom.Items.Add(bomb);
            }

            // Remove used item
            inventory[CurrentSelectedItemName].RemoveAt(0);

            // If no more items of that type, clean up
            if (inventory[CurrentSelectedItemName].Count == 0)
            {
                inventory.Remove(CurrentSelectedItemName);
                inventoryKeys.RemoveAt(currentItemIndex);

                // Prevent out-of-bounds index after removal
                if (inventoryKeys.Count > 0)
                {
                    currentItemIndex = Math.Clamp(currentItemIndex, 0, inventoryKeys.Count - 1);
                }
                else
                {
                    currentItemIndex = 0;
                }
            }

            // Trigger animation lock
            isUsingItem = true;
            itemFrameCounter = 0;
            sprite.SetState(LinkAction.UsingItem, sprite.CurrentDirection);
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

        public void Consume(IItem item) {
            if (item.name == "Heart")
            {
                Game1.Instance.HandlePlayerHealed(1f);
            }
            else if (item.name == "Crystal") { 
                currentCrystal += 1;
            }
        }

        public void SwitchItem(int direction)
        {
            if (inventoryKeys.Count > 0)
            {
                currentItemIndex = (currentItemIndex + direction + inventoryKeys.Count) % inventoryKeys.Count;
            }
        }

        public void AddItem(IItem item)
        {
            string itemName = item.name;

            if (!inventory.ContainsKey(itemName))
            {
                inventory[itemName] = new List<IItem>();
                inventoryKeys.Add(itemName); // Track order of discovery
            }

            inventory[itemName].Add(item);
        }

        public int GetItemCount(string itemName)
        {
            return inventory.TryGetValue(itemName, out var items) ? items.Count : 0;
        }

        public IItem GetCurrentItem()
        {
            string key = CurrentSelectedItemName;
            return inventory.TryGetValue(key, out var list) && list.Count > 0 ? list[0] : null;
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
