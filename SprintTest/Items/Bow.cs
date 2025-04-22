using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;
using sprint0Test.Sprites;

namespace sprint0Test.Items
{
    public class Bow : IItem
    {
        public Vector2 Position { get; private set; }
        public string name { get; private set; }
        public bool IsCollected { get; private set; }
        public ItemBehaviorType BehaviorType => ItemBehaviorType.Equipable;

        private StaticSprite sprite;

        public Bow(string name, Texture2D texture, Vector2 position)
        {
            this.name = name;
            this.Position = position;
            this.IsCollected = false;
            this.sprite = new StaticSprite(texture, 0.15f);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsCollected)
            {
                sprite.Draw(spriteBatch, Position);
            }
        }

        public void Collect()
        {
            IsCollected = true;
            Link.Instance.SetCurrentWeapon(WeaponType.Bow);
            Link.Instance.AddItem(this);
        }

        public void Use() { Link.Instance.SetCurrentWeapon(WeaponType.Bow); }
    }
}