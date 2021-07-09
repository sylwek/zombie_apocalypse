using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class Bullet : Projectile, IPoolable<float, float, int, GameObject, IMemoryPool>
    {
        private GameObject _source; // TODO: remove

        public void OnSpawned(float speed, float lifeTime, int damage, GameObject go, IMemoryPool pool)
        {
            _pool = pool;
            _speed = speed;
            _lifeTime = lifeTime;
            _damage = damage;
            _source = go;

            _startTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        protected override void OnDamageDone(GameObject obj)
        {
            base.OnDamageDone(obj);
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<float, float, int, GameObject, Bullet>
        {
        }
    }
}
