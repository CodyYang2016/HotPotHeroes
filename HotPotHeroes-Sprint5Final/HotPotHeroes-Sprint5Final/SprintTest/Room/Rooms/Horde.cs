using Microsoft.Xna.Framework;
using sprint0Test.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace sprint0Test.Dungeon
{
    public class r8c : AbstractRoom
    {
        private double spawnTimer = 0;
        private double spawnInterval = 3.0;
        private double spawnIntervalScale = 0.9;
        private double totalElapsedTime = 0;
        private double nextBossTime = 120.0;

        private int waveCount = 0;
        private bool isBossActive = false;

        public r8c(RoomData data)
        {
            RoomID = data.RoomID;
            RoomData = data;

        }

        public override void Initialize()
        {
            base.Initialize();

            RoomData.HasBeenCleared = false; // ✅ allow waves to spawn again
            Enemies.Clear();
            waveCount = 0;
            spawnTimer = 0;
            totalElapsedTime = 0;
            spawnInterval = 3.0;
            isBossActive = false;

            Debug.WriteLine("🟢 Horde room initialized.");
            Perimeter perimeter = new Perimeter(this);
            GenerateStandardDoorHitboxes();
        }

        public override void Update(GameTime gameTime)
        {
            // ✅ Update enemies + clear dead ones
            base.Update(gameTime);

            totalElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (isBossActive)
            {
                if (Enemies.Count == 0)
                {
                    isBossActive = false;
                    Debug.WriteLine("✅ Boss defeated.");
                }
                return;
            }

            if (totalElapsedTime >= nextBossTime)
            {
                SpawnBoss();
                isBossActive = true;
                nextBossTime += 120.0;
                return;
            }

            spawnTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnTimer >= spawnInterval)
            {
                SpawnWave();
                waveCount++;
                spawnTimer = 0;

                spawnInterval *= spawnIntervalScale;
                if (spawnInterval < 10.0)
                    spawnInterval = 10.0;  // ⏳ Minimum of 2 seconds between waves

            }
        }

        private void SpawnWave()
        {
            Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(600, 200)));
            Enemies.Add(EnemyManager.Instance.CreateKeese(new Vector2(300, 400)));

            Debug.WriteLine($"🌀 Wave {waveCount + 1} spawned. Enemies in room: {Enemies.Count}");
        }

        private void SpawnBoss()
        {
            var boss = EnemyManager.Instance.CreateAquamentus(new Vector2(128, 80));
            Enemies.Add(boss);

            Debug.WriteLine($"👹 Boss spawned at {totalElapsedTime / 60:0.0} min.");
        }
    }
}
