using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public abstract class SpellBase : Projectile, IPoolable<Vector3, Quaternion, float, float, int, float, IMemoryPool>
    {
        public enum SpellType
        {
            FireStrike,
            IceBlast,
        }

        protected float _cooldownDuration;

        public void OnSpawned(Vector3 position, Quaternion rotation, float speed, float lifeTime, int damage, float cooldown, IMemoryPool pool)
        {
            var trans = transform;
            trans.position = position;
            trans.rotation = rotation;

            _pool = pool;
            _speed = speed;
            _lifeTime = lifeTime;
            _damage = damage;
            _cooldownDuration = cooldown;
            _startTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            _pool = null;
        }
    }
}
