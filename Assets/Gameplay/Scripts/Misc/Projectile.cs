using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public abstract class Projectile : MonoBehaviour
    {
        protected float _speed;
        protected float _lifeTime;
        protected int _damage;
        protected float _startTime;
        protected IMemoryPool _pool;

        private Transform _transform;

        protected virtual Vector3 MoveDirection => -transform.forward;

        public void Awake()
        {
            _transform = transform;
        }

        public void Update()
        {
            _transform.position -= MoveDirection * _speed * Time.deltaTime;

            if (_pool != null && Time.realtimeSinceStartup - _startTime > _lifeTime)
                _pool.Despawn(this);
        }

        public void OnTriggerEnter(Collider other)
        {
            var damagable = other.GetComponent<IDamagable>();
            if (damagable != null && damagable.AcceptDamage(gameObject))
            {
                damagable.TakeDamage(_damage);
                OnDamageDone(other.gameObject);
            }
        }

        protected virtual void OnDamageDone(GameObject obj)
        {
        }
    }
}
