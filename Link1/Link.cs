
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
        private int currentWeaponIndex = 0;
        private int currentCrystal = 0;
        public int CrystalCount => currentCrystal;
        private WeaponType currentWeapon = WeaponType.Sword;
        public string CurrentSelectedItemName => inventoryKeys.Count > 0 ? inventoryKeys[currentItemIndex] : "";

        private Dictionary<string, List<IItem>> inventory = new();
        private List<string> inventoryKeys = new();
        private List<IItem> weapons = new();
        public List<string> InventoryKeys => inventoryKeys;
        private readonly int screenMinX = 0;
        private readonly int screenMinY = 0;
        private readonly int screenMaxX = Game1.RoomWidth;
        private readonly int screenMaxY = Game1.RoomHeight;
        private bool isVisible = true; // This will track visibility
        private double damageCooldownTimer = 0;
        private const double DamageCooldownDuration = 0.5; // in seconds

        private bool isMovingThisFrame = false;

        //Link Dash implamentation
        private bool isDashing = false;
        private float dashSpeed = 8f;
        private int dashDuration = 10; // frames
        private int dashCooldown = 30; // frames
        private int dashCounter = 0;
        private int dashCooldownCounter = 0;
        private LinkDirection dashDirection;

        private bool isInvulnerable = false;
        public bool IsInvulnerable
        {
            get => isInvulnerable;
            set => isInvulnerable = value;
        }
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
            Debug.WriteLine($"Link is being spawned at Pos: {position}");
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

            isMovingThisFrame = true;

            if (sprite.CurrentAction != LinkAction.Walking ||
                sprite.CurrentDirection != direction)
            {
                sprite.SetState(LinkAction.Walking, direction);
            }

            switch (direction)
            {
                case LinkDirection.Up: position.Y -= speed; break;
                case LinkDirection.Down: position.Y += speed; break;
                case LinkDirection.Left: position.X -= speed; break;
                case LinkDirection.Right: position.X += speed; break;
            }
        }

        public void Dash()
        {
            if (!isDashing && dashCooldownCounter <= 0)
            {
                isDashing = true;
                dashCounter = dashDuration;
                dashDirection = sprite.CurrentDirection;
                dashCooldownCounter = dashCooldown + dashDuration;
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

                IItem equipped = GetCurrentWeapon();
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
            if (isInvulnerable) return;        // God Mode ??????
            if (damageCooldownTimer > 0) return; // ? Still on cooldown


            if (!isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Damaged, sprite.CurrentDirection);
            }

            Game1.Instance.HandlePlayerDamage();

            damageCooldownTimer = DamageCooldownDuration; // ? Start cooldown
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
            if (item.BehaviorType != ItemBehaviorType.Equipable)
            {
                if (!inventory.ContainsKey(itemName))
                {
                    inventory[itemName] = new List<IItem>();
                    inventoryKeys.Add(itemName); // Track order of discovery
                }
                inventory[itemName].Add(item);
            }
            else {
                weapons.Add(item);
            }
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

        public IItem GetCurrentWeapon()
        {
            return weapons[currentWeaponIndex];
        }

        public void SetCurrentWeapon(WeaponType weapon)
        {
            currentWeapon = weapon;
        }

        public void CycleWeapon()
        {
            if (weapons.Count > 0)
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
            }
        }


        public void Update(GameTime gameTime)
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

            if (damageCooldownTimer > 0)
            {
                damageCooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (isDashing)
            {
                // Move faster in dashDirection
                switch (dashDirection)
                {
                    case LinkDirection.Up: position.Y -= dashSpeed; break;
                    case LinkDirection.Down: position.Y += dashSpeed; break;
                    case LinkDirection.Left: position.X -= dashSpeed; break;
                    case LinkDirection.Right: position.X += dashSpeed; break;
                }

                dashCounter--;
                if (dashCounter <= 0)
                {
                    isDashing = false;
                    sprite.SetState(LinkAction.Idle, dashDirection);
                }
            }
            else
            {
                if (dashCooldownCounter > 0)
                    dashCooldownCounter--;
            }


            Vector2 scaledSize = sprite.GetScaledDimensions();
            //const int TOP_MARGIN = 88;
            ////const int BOTTOM_MARGIN = 88;

            //int ROOM_HEIGHT = 480;
            //int ROOM_WIDTH = 735;

            //position.Y = MathHelper.Clamp(
            //    position.Y,
            //    TOP_MARGIN,
            //    ROOM_HEIGHT
            //);

            //position.X = MathHelper.Clamp(
            //    position.X,
            //    0,
            //    ROOM_WIDTH
            //);
            position.X = MathHelper.Clamp(position.X, screenMinX, screenMaxX - scaledSize.X);
            position.Y = MathHelper.Clamp(position.Y, screenMinY, screenMaxY - scaledSize.Y);
            if (!isMovingThisFrame && !isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
            }

            isMovingThisFrame = false;
        }

        public static void Reset(LinkSprite newSprite, Vector2 newPosition)
        {
            if (instance == null)
                throw new InvalidOperationException("Link has not been initialized yet!");

            instance.sprite = newSprite;
            instance.position = newPosition;
            instance.ResetState();
        }

        public void ResetState()
        {
            // Reset directional state, animation, and counters
            sprite.Scale = 2f;
            sprite.SetState(LinkAction.Idle, LinkDirection.Down);
            isAttacking = false;
            isUsingItem = false;
            attackFrameCounter = 0;
            itemFrameCounter = 0;
            currentItemIndex = 0;
            inventory.Clear(); // optionally clear inventory
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
