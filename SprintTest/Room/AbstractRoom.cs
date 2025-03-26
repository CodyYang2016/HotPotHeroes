using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Room
{
    using global::sprint0Test.Enemy;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    namespace sprint0Test.Dungeon
    {
        public abstract class AbstractRoom : IRoom
        {
            public string RoomID { get; protected set; }

            public List<IEnemy> Enemies { get; protected set; } = new List<IEnemy>();
            public List<IBlock> Blocks { get; protected set; } = new List<IBlock>();
            public List<IItem> Items { get; protected set; } = new List<IItem>();

            public bool IsCleared => Enemies.TrueForAll(e => e.IsDead);

            public virtual void Initialize()
            {
                // Optional: preload static content here if needed
            }

            public virtual void Update(GameTime gameTime)
            {
                foreach (var enemy in Enemies)
                {
                    enemy.Update(gameTime);
                }

                foreach (var block in Blocks)
                {
                    block.Update();
                }

                foreach (var item in Items)
                {
                    item.Update(gameTime);
                }

                // Clean up dead enemies or used items if necessary
                Enemies.RemoveAll(e => e.IsDead);
                Items.RemoveAll(i => i.IsCollected);
            }

            public virtual void Draw(SpriteBatch spriteBatch)
            {
                foreach (var block in Blocks)
                {
                    block.Draw(spriteBatch);
                }

                foreach (var enemy in Enemies)
                {
                    enemy.Draw(spriteBatch);
                }

                foreach (var item in Items)
                {
                    item.Draw(spriteBatch);
                }
            }
        }
    }

}
