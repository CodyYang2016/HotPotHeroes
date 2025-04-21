using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;
using sprint0Test.Managers;
using sprint0Test.Sprites;

namespace sprint0Test.Items
{
    public class Bomb : IItem
    {
        public enum BombState { InWorld, Collected, Planted, Exploded }
        private StaticSprite bomb;
        private StaticSprite explodedBomb;
        private BombState state = BombState.InWorld;
        public Vector2 Position { get; private set; }
        private bool isCollected = false;
        public bool IsCollected => state == BombState.Exploded && timer >= explosionDuration;
        private bool hasJustExploded = false;
        public bool HasJustExploded => state == BombState.Exploded && hasJustExploded;
        public BombState State => state;
        public string name { get; private set; }
        private double timer = 0;
        private readonly double explodeTime = 1000;      // 1 second after planting
        private readonly double explosionDuration = 2000; // Show explosion for 1s
        public ItemBehaviorType BehaviorType => ItemBehaviorType.Collectible;

        public Bomb(string name, Texture2D bombTexture, Vector2 position)
        {
            this.name = name;
            this.bomb = new StaticSprite(bombTexture, 0.3f);
            this.Position = position;
            this.explodedBomb = new StaticSprite(TextureManager.Instance.GetTexture("Explosion"), 0.15f);
        }

        public void Update(GameTime gameTime)
        {
            Debug.WriteLine($"Bomb update: State = {state}, Timer = {timer}");
            if (state == BombState.Planted)
            {
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer >= explodeTime)
                {
                    state = BombState.Exploded;
                    hasJustExploded = true;
                    timer = 0;
                }
            }
            else if (state == BombState.Exploded)
            {
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer >= explosionDuration)
                {
                    isCollected = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (state == BombState.Planted)
            {
                Console.WriteLine($"🧨 Drawing Bomb at {Position}, State = {state}");
                bomb.Draw(spriteBatch, Position);
            }
            else if (state == BombState.Exploded && timer < explosionDuration)
            {
                explodedBomb.Draw(spriteBatch, Position);
            }
            else if (state == BombState.InWorld)
            {
                bomb.Draw(spriteBatch, Position);
            }
        }

        public void Collect()
        {
            if (state == BombState.InWorld)
            {
                state = BombState.Collected;
            }
        }

        public void Use()
        {
            Debug.WriteLine($"Bomb use: State = {state}");

            if (state == BombState.Collected)
            {
                Position = Link.Instance.Position;
                state = BombState.Planted;
                timer = 0;
            }
        }

        public void MarkExplosionHandled()
        {
            hasJustExploded = false;
        }
    }
}