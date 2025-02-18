using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0Test.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HotpotHeroes.sprint0Game.sprint0Test.Managers;
// LINK TO PLAYER CLASS BEING BUILT OUTSIDE OF MY OWN CODE

namespace sprint0Test.Enemy
{
    public abstract class AbstractEnemy : IEnemy
    {
        protected Vector2 position;
        protected int health;
        protected Texture2D texture;
        protected IEnemyState currentState;
        protected float detectionRadius = 100f; // Default detection range
        protected float attackRange = 30f; // Default attack range
        protected float scale = 3f;

        public AbstractEnemy(Vector2 startposition, Texture2D enemyTexture)
        {
            position = startposition;
            texture = enemyTexture;
            health = 3;  // Default health
            currentState = new AttackState(this);  // Default state
        }

        public virtual void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
        }
        public void SetScale(float newScale)
        {
            scale = newScale;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                ChangeState(new DeadState(this));
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        //public bool DetectPlayer()
        //{
        //    // Placeholder: Detect if player is within a certain range
        //    return Vector2.Distance(position, Player.Instance.position) < detectionRadius;
        //}

        //public bool IsInAttackRange()
        //{
        //    return Vector2.Distance(position, Player.Instance.position) < attackRange;
        //}

        //public Vector2 GetDirectionToPlayer()
        //{
        //    return Vector2.Normalize(Player.Instance.position - position);
        //}

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Vector2 GetPosition()
        {
            return position;
        }
        public virtual void PerformAttack()
        {
            // Overriden
        }

        public void Destroy()
        {
            // EnemyManager.Instance.RemoveEnemy(this);
        }

    }
}
