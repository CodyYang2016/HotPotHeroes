using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Sprites;

namespace sprint0Test.Items
{
    public class Rupee : IItem
    {
        private StaticSprite sprite;
        public Vector2 Position { get; private set; }

        public string name { get; private set; }

        private bool isCollected;
        public bool IsCollected => isCollected;
        public ItemBehaviorType BehaviorType => ItemBehaviorType.Collectible;
        public Rupee(string name, Texture2D texture, Vector2 position)
        {
            this.name = name;
            this.sprite = new StaticSprite(texture, 0.5f);
            this.Position = position;
            this.isCollected = false; // Initially not collected
        }

        public void Update(GameTime gameTime)
        {
            // If collected, don't update
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isCollected)
            {
                sprite.Draw(spriteBatch, Position);
            }
        }

        public void Collect()
        {
            isCollected = true;
        }

        public void Use() { }
    }
}
