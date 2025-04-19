using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test
{
    public class BlockPush : AbstractBlock
    {

        public BlockPush(Texture2D texture, Rectangle sourceRectangle, Vector2 position, float scale = 1.0f, bool isVisible = true)
            : base(texture, position, sourceRectangle, scale, isVisible)
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
