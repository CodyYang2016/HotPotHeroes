using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Headboard
{
    private Texture2D heartTexture;
    private SpriteFont font;
    private Vector2 position;
    private int health;

    public Headboard(Texture2D heartTexture, SpriteFont font, Vector2 position)
    {
        this.heartTexture = heartTexture;
        this.font = font;
        this.position = position;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(heartTexture, position, Color.White);

        Vector2 textPosition = new Vector2(position.X + heartTexture.Width + 5, position.Y);
        spriteBatch.DrawString(font, $"x {health}", textPosition, Color.White);
    }
}