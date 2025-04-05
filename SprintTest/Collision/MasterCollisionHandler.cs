using System.Collections.Generic;
using sprint0Test.Projectiles;
using sprint0Test.Enemy;


namespace sprint0Test
{
    public class MasterCollisionHandler
    {
        private PlayerBlockCollisionHandler _playerBlockCollisionHandler;
        private PlayerEnemyCollisionHandler _playerEnemyCollisionHandler;
        private PlayerItemCollisionHandler _playerItemCollisionHandler;
        private EnemyBlockCollisionHandler _enemyBlockCollisionHandler;
        private PlayerProjectileCollisionHandler _playerProjectileCollisionHandler;
        private ProjectileBlockCollisionHandler _projectileBlockCollisionHandler;

        public MasterCollisionHandler()
        {
            // Initialize all individual collision handlers
            _playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
            _playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
            _playerItemCollisionHandler = new PlayerItemCollisionHandler();
            _enemyBlockCollisionHandler = new EnemyBlockCollisionHandler();
            _playerProjectileCollisionHandler = new PlayerProjectileCollisionHandler();
            _projectileBlockCollisionHandler = new ProjectileBlockCollisionHandler();
        }

        public void HandleCollisions(List<IItem> roomItems, IEnemy activeEnemies, List<IProjectile> activeProjectiles, List<IBlock> blocks)
        {
            // Handle collisions for each of the handlers
            _playerBlockCollisionHandler.HandleCollisionList(blocks);
            _playerEnemyCollisionHandler.HandleCollision(activeEnemies);
            _playerItemCollisionHandler.HandleCollisionList(roomItems);
            _enemyBlockCollisionHandler.HandleCollisionList(blocks, activeEnemies);
            _playerProjectileCollisionHandler.HandleCollisionList(activeProjectiles);
            _projectileBlockCollisionHandler.HandleCollisionList(blocks, activeProjectiles);
        }
    }
}
