using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using sprint0Test.Items;
using sprint0Test.Sprites;
using sprint0Test.Managers;
using System.Diagnostics;
using static sprint0Test.Items.Bomb;
using sprint0Test.Dungeon;

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
        private int currentCrystal = 0;
        private List<IItem> inventory = new List<IItem>();
        private RoomManager roomManager;
        private readonly int screenMinX = 0;
        private readonly int screenMinY = 0;
        private readonly int screenMaxX = 800;
        private readonly int screenMaxY = 480;
        private bool isVisible = true; // This will track visibility
        //sprint5 Link update
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
        public IReadOnlyList<IItem> Inventory => inventory.AsReadOnly();

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

            isMovingThisFrame = true;               

            // 只有在“动作或朝向变化”时才重设动画
            if (sprite.CurrentAction != LinkAction.Walking ||
                sprite.CurrentDirection != direction)
            {
                sprite.SetState(LinkAction.Walking, direction);
            }

            // 真正移动
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
                if (inventory[currentItemIndex] is Bomb bomb && bomb.State == BombState.Planted)
                {
                    roomManager.CurrentRoom.Items.Add(bomb);
                }
            }
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


        public void Consume(IItem item)
        {
            if (item.name == "Heart")
            {
                //Game1.Instance.HandlePlayerHealed();
            }
            else if (item.name == "Crystal")
            {
                currentCrystal += 1;
            }
        }

        public void SwitchItem(int direction)
        {
            if (inventory.Count > 0)
            {
                currentItemIndex = (currentItemIndex + direction + inventory.Count) % inventory.Count;
            }
        }

        public void AddItem(IItem item)
        {
            inventory.Add(item);
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
            position.X = MathHelper.Clamp(position.X, screenMinX, screenMaxX - scaledSize.X);
            position.Y = MathHelper.Clamp(position.Y, screenMinY, screenMaxY - scaledSize.Y);

            if (!isMovingThisFrame && !isAttacking && !isUsingItem)
            {
                sprite.SetState(LinkAction.Idle, sprite.CurrentDirection);
            }

            // ???????
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
            if (!isVisible) return;
            sprite.Draw(spriteBatch, position);
        }

        public Vector2 GetScaledDimensions()
        {
            return sprite.GetScaledDimensions(); // Forward call to LinkSprite
        }

    }
}