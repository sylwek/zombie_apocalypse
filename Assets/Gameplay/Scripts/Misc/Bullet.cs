using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class Bullet : MonoBehaviour, IPoolable<float, float, int, GameObject, IMemoryPool>
    {
        private float _startTime;
        private float _speed;
        private float _lifeTime;
        private int _damage;
        private GameObject _source;

        private IMemoryPool _pool;

        private Vector3 MoveDirection => -transform.forward;

        public void OnTriggerEnter(Collider other)
        {
            var damagable = other.GetComponent<IDamagable>();
            if (damagable != null && other.gameObject != _source)
            {
                damagable.TakeDamage(_damage);
                _pool.Despawn(this);
            }
        }

        public void Update()
        {
            // TODO: get rid of Update
            transform.position -= MoveDirection * _speed * Time.deltaTime;

            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
                _pool.Despawn(this);
        }

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

        public class Factory : PlaceholderFactory<float, float, int, GameObject, Bullet>
        {
        }
    }
}
