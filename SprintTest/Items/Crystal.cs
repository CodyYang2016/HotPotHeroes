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
    public class Crystal : IItem
    {
        private StaticSprite sprite;
        public Vector2 Position { get; private set; }
        public string name { get; private set; }

        private bool isCollected;
        public bool IsCollected => isCollected;
        public ItemBehaviorType BehaviorType => ItemBehaviorType.Consumable;
        public Crystal(string name, Texture2D texture, Vector2 position)
        {
            this.name = name;
            this.sprite = new StaticSprite(texture, 0.1f);
            this.Position = position;
            this.isCollected = false;
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
