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
        public enum BombState { InWorld, InInventory, Planted, Exploding, Done }

        private StaticSprite bomb;
        private StaticSprite explodedBomb;
        private BombState state = BombState.InWorld;
        public Vector2 Position { get; private set; }
        public bool IsCollected => state == BombState.InInventory || state == BombState.Done;
        public BombState State => state;
        public string name { get; private set; }
        private double timer = 0;
        private readonly double explodeTime = 1000;
        private readonly double explosionDuration = 2000;
        public ItemBehaviorType BehaviorType => ItemBehaviorType.Collectible;
        public bool HasJustExploded { get; private set; } = false;

        public Bomb(string name, Texture2D bombTexture, Vector2 position)
        {
            this.name = name;
            this.bomb = new StaticSprite(bombTexture, 0.15f);
            this.Position = position;
            this.explodedBomb = new StaticSprite(TextureManager.Instance.GetTexture("Explosion"), 0.15f);
        }

        public void Update(GameTime gameTime)
        {
            if (state == BombState.Planted)
            {
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer >= explodeTime)
                {
                    state = BombState.Exploding;
                    timer = 0;
                    HasJustExploded = true;
                }
            }
            else if (state == BombState.Exploding)
            {
                timer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer >= explosionDuration)
                {
                    state = BombState.Done;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case BombState.InWorld:
                case BombState.Planted:
                    bomb.Draw(spriteBatch, Position);
                    break;

                case BombState.Exploding:
                    explodedBomb.Draw(spriteBatch, Position);
                    break;
            }
        }

        public void Collect()
        {
            if (state == BombState.InWorld)
            {
                state = BombState.InInventory;
            }
        }

        public void Use()
        {
            if (state == BombState.InInventory)
            {
                Position = Link.Instance.Position;
                state = BombState.Planted;
                timer = 0;
            }
        }

        public void MarkExplosionHandled()
        {
            HasJustExploded = false;
        }
    }
}
