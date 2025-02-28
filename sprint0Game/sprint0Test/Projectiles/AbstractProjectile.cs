using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Projectiles;

public abstract class AbstractProjectile : IProjectile
{
    protected Vector2 position;
    protected Vector2 direction;
    protected Texture2D texture;
    protected bool isActive;
    protected float speed;
    protected float lifetime;

    public AbstractProjectile(Vector2 startPosition, Vector2 direction, Texture2D texture, float speed, float lifetime = 5.0f)
    {
        this.position = startPosition;
        this.direction = direction;
        this.texture = texture;
        this.speed = speed;
        this.lifetime = lifetime;
        this.isActive = true;
    }

    public virtual void Update(GameTime gameTime)
    {
        if (!isActive) return;

        position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        lifetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (lifetime <= 0)
        {
            Deactivate();
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (isActive && texture != null)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }

    // ✅ Reset function for reusing the projectile from the pool
    public void Reset(Vector2 newPosition, Vector2 newDirection)
    {
        position = newPosition;
        direction = newDirection;
        lifetime = 5.0f;  // Reset lifetime
        isActive = true;  // Reactivate projectile
    }
}
