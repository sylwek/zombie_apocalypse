using System;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class PlayerShootHandler : ITickable
    {
        [Serializable]
        public class Settings
        {
            public float BulletLifetime;
            public float BulletSpeed;
            public int BulletDamage;
            public float MinShootInterval;
            public float BulletSpawnOffsetDistance;
        }

        [Inject]
        private readonly Settings _settings;
        [Inject]
        private readonly Player _player;
        [Inject]
        private readonly IPlayerInputState _playerInputState;
        [Inject]
        private readonly Bullet.Factory _bulletFactory;

        private float _lastFiringTime;

        public void Tick()
        {
            if (_playerInputState.IsFiring && Time.time - _lastFiringTime > _settings.MinShootInterval)
                Fire();
        }

        private void Fire()
        {
            SpawnBullet();
            _lastFiringTime = Time.time;
        }

        private void SpawnBullet()
        {
            var bullet = _bulletFactory.Create(
                _settings.BulletSpeed, _settings.BulletLifetime, _settings.BulletDamage, _player.gameObject);

            var transform = bullet.transform;
            // TODO: move to factory parameters
            transform.position = _player.Position + _player.transform.forward * _settings.BulletSpawnOffsetDistance;
            transform.rotation = _player.Rotation;
        }
    }
}
