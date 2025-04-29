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
        private ProjectileEnemyCollisionHandler _projectileEnemyCollisionHandler;

        public MasterCollisionHandler()
        {
            // Initialize all individual collision handlers
            _playerBlockCollisionHandler = new PlayerBlockCollisionHandler();
            _playerEnemyCollisionHandler = new PlayerEnemyCollisionHandler();
            _playerItemCollisionHandler = new PlayerItemCollisionHandler();
            _enemyBlockCollisionHandler = new EnemyBlockCollisionHandler();
            _playerProjectileCollisionHandler = new PlayerProjectileCollisionHandler();
            _projectileBlockCollisionHandler = new ProjectileBlockCollisionHandler();
            _projectileEnemyCollisionHandler = new ProjectileEnemyCollisionHandler();
        }

        public void HandleCollisions(List<IItem> items, List<IEnemy> enemies, List<IProjectile> projectiles, List<IBlock> blocks)
        {
            // Handle collisions for each of the handlers
            _playerBlockCollisionHandler.HandleCollisionList(blocks);
            _playerEnemyCollisionHandler.HandleCollisionList(enemies);
            _playerItemCollisionHandler.HandleCollisionList(items);
            _enemyBlockCollisionHandler.HandleCollisionList(blocks, enemies);
            _playerProjectileCollisionHandler.HandleCollisionList(projectiles);
            _projectileBlockCollisionHandler.HandleCollisionList(blocks, projectiles);
            _projectileEnemyCollisionHandler.HandleCollisionList(enemies, projectiles);
        }
    }
}