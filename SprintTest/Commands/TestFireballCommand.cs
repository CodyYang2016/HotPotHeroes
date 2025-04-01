using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using sprint0Test.Managers;

namespace sprint0Test
{
    public class TestFireballCommand : ICommand
    {
        public void Execute()
        {
            Vector2 spawnPosition = new Vector2(500, 300); // Arbitrary test position
            Vector2 fireballDirection = new Vector2(1, 0); // Moves right

            Console.WriteLine("Spawning test fireball!");

            ProjectileManager.Instance.SpawnProjectile(
                spawnPosition,
                fireballDirection,
                "Fireball"
            );
        }
    }
}

