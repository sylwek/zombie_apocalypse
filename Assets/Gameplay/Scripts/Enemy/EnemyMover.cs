using System;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class EnemyMover : ITickable
    {
        [Serializable]
        public class Settings
        {
            public float MoveSpeed;
        }

        [Inject]
        private readonly Settings _settings;

        [Inject]
        private readonly Enemy _enemy;

        [Inject]
        private readonly Player _player;

        public void Tick()
        {
            _enemy.Position = Vector3.MoveTowards(_enemy.Position, _player.Position, _settings.MoveSpeed * Time.deltaTime);
        }
    }
}
