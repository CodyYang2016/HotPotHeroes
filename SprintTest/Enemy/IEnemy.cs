using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using sprint0Test.Enemy;

namespace sprint0Test.Enemy
{
    public interface IEnemy
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void TakeDamage(int damage);
        void ChangeState(IEnemyState newState);
        void SetPosition(Vector2 newPosition);
        Vector2 GetPosition();
        Vector2 GetDimensions();
        void PerformAttack(); 
    }
}
