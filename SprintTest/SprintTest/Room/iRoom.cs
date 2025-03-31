using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using sprint0Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0Test.Room
{
    public interface IRoom
    {
        string RoomID { get; }
        List<IEnemy> Enemies { get; }
        List<IBlock> Blocks { get; }
        List<IItem> Items { get; }

        bool IsCleared { get; }

        void Initialize(); // spawn everything
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }

}
