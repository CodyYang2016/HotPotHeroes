using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Link1;
using sprint0Test.Sprites;

namespace sprint0Test.Items
{
    public class Sword : IItem
    {
        public string name => "Sword";
        public Vector2 Position { get; private set; }
        public bool IsCollected { get; private set; } = true;
        public ItemBehaviorType BehaviorType => ItemBehaviorType.Equipable;
        private StaticSprite sprite;

        public Sword(string name, Texture2D texture, Vector2 position)
        {
            this.Position = position;
            this.sprite = new StaticSprite(texture, 0.15f);
        }

        public void Collect() { }

        public void Draw(SpriteBatch spriteBatch) { }

        public void Update(GameTime gameTime) { }

        public void Use()
        {
            // Set the current weapon to Sword when this item is selected
            Link.Instance.SetCurrentWeapon(WeaponType.Sword);
        }
    }
}
