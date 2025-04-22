
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
    public enum WeaponType
    {
        Sword,
        Bow
    }
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
        private int currentCrystal = 0;
        public int CrystalCount => currentCrystal;
        private WeaponType currentWeapon = WeaponType.Sword;
        public string CurrentSelectedItemName => inventoryKeys.Count > 0 ? inventoryKeys[currentItemIndex] : "";

        private Dictionary<string, List<IItem>> inventory = new();
        private List<string> inventoryKeys = new();

        public List<string> InventoryKeys => inventoryKeys;
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

                Vector2 linkCenter = position + GetScaledDimensions() / 2f;
                Vector2 direction = Vector2.Zero;
                Vector2 spawnOffset = Vector2.Zero;

                switch (sprite.CurrentDirection)
                {
                    case LinkDirection.Up:
                        direction = new Vector2(0, -1);
                        spawnOffset = new Vector2(0, -20);
                        break;
                    case LinkDirection.Down:
                        direction = new Vector2(0, 1);
                        spawnOffset = new Vector2(0, 20);
                        break;
                    case LinkDirection.Left:
                        direction = new Vector2(-1, 0);
                        spawnOffset = new Vector2(-20, 0);
                        break;
                    case LinkDirection.Right:
                        direction = new Vector2(1, 0);
                        spawnOffset = new Vector2(20, 0);
                        break;
                }

                Vector2 spawnPosition = linkCenter + spawnOffset;

                IItem equipped = GetCurrentItem();
                if (equipped != null && equipped.name == "Bow" && equipped.BehaviorType == ItemBehaviorType.Equipable)
                {
                    currentWeapon = WeaponType.Bow;
                }
                else
                {
                    currentWeapon = WeaponType.Sword;
                }

                string weapon = currentWeapon == WeaponType.Bow ? "Arrow" : "Sword";
                ProjectileManager.Instance.SpawnProjectile(spawnPosition, direction, weapon);
            }
        }

        private Vector2 GetDirectionVector(LinkDirection direction)
        {
            return direction switch
            {
                LinkDirection.Up => new Vector2(0, -1),
                LinkDirection.Down => new Vector2(0, 1),
                LinkDirection.Left => new Vector2(-1, 0),
                LinkDirection.Right => new Vector2(1, 0),
                _ => Vector2.Zero
            };
        }

        public void UseItem()
        {
            if (inventoryKeys.Count == 0 || currentItemIndex >= inventoryKeys.Count)
                return;

            string selectedItem = inventoryKeys[currentItemIndex];

            if (!inventory.ContainsKey(selectedItem) || inventory[selectedItem].Count == 0)
                return;

            IItem item = inventory[selectedItem][0];
            item.Use();

            if (item.BehaviorType != ItemBehaviorType.Equipable)
            {
                inventory[selectedItem].RemoveAt(0);

                if (inventory[selectedItem].Count == 0)
                {
                    inventory.Remove(selectedItem);
                    inventoryKeys.RemoveAt(currentItemIndex);

                    if (inventoryKeys.Count > 0)
                    {
                        currentItemIndex = Math.Clamp(currentItemIndex, 0, inventoryKeys.Count - 1);
                    }
                    else
                    {
                        currentItemIndex = 0;
                    }
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

        public void CycleItem(int direction)
        {
            if (inventoryKeys.Count > 0)
            {
                currentItemIndex = (currentItemIndex + direction + inventoryKeys.Count) % inventoryKeys.Count;
            }
        }

        public void SetCurrentWeapon(WeaponType weapon)
        {
            currentWeapon = weapon;
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
