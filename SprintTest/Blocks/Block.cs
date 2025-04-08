using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test
{
    public class Block : AbstractBlock
    {

        public Block(Texture2D texture, Rectangle sourceRectangle, Vector2 position, float scale = 1.0f, bool isVisible = true, bool isSolid = true)
            : base(texture, position, sourceRectangle, scale, isVisible, isSolid)
        {
        }

        public override void Update()
        {
            // Update logic for the block, for example, if it's a moving object or interactable
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
