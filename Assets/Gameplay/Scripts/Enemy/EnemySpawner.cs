using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ZombieApocalypse
{
    public class EnemySpawner : ITickable, IInitializable
    {
        [Serializable]
        public class Settings
        {
            public int InitialHP;
            public float MinSpawnRadius;
            public float MaxSpawnRadius;
            public Color BaseColor;
        }

        [Inject]
        private readonly Settings _settings;
        [Inject]
        private readonly GameSettingsInstaller.DifficultySettings _difficultySettings;
        [Inject]
        private readonly Enemy.Factory _enemyFactory;
        [Inject]
        private readonly Player _player;

        private float _nextSpawn;

        public void Initialize()
        {
            SetupNextSpawnTime();
        }

        public void Tick()
        {
            if (ShouldSpawnEnemy())
            {
                SpawnEnemy();
                SetupNextSpawnTime();
            }
        }

        private void SetupNextSpawnTime()
        {
            _nextSpawn = Time.time + GetNextSpawnInverval();
        }

        private bool ShouldSpawnEnemy() => Time.time >= _nextSpawn;

        private void SpawnEnemy()
        {
            var directionVec = (Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward).normalized;
            var positionOffset = directionVec * Mathf.Lerp(_settings.MinSpawnRadius, _settings.MaxSpawnRadius, Random.value);
            Color.RGBToHSV(_settings.BaseColor,  out var h, out var s, out var v);
            var color = Random.ColorHSV(0f, 1f, s, s, v, v);
            _enemyFactory.Create(_player.Position + positionOffset, color, _settings.InitialHP);
        }

        private float GetNextSpawnInverval() => Random.Range(_difficultySettings.MaxEnemySpawnInterval, _difficultySettings.MaxEnemySpawnInterval);

    }
}
