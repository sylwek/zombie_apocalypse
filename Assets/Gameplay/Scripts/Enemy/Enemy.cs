using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class Enemy : MonoBehaviour, IDamagable, IPoolable<Vector3, Color, IMemoryPool>
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private int _HP;

        private IMemoryPool _pool;

        // [Inject]
        // public void Construct(Vector3 position, Color color)
        // {
        //     Position = position;
        //     GetComponent<Renderer>().material.color = color;
        // }

        public void TakeDamage(int damage)
        {
            _HP -= damage;
            if (_HP <= 0)
            {
                _pool.Despawn(this);
            }
        }

        public void OnSpawned(Vector3 position, Color color, IMemoryPool pool)
        {
            _pool = pool;

            Position = position;
            GetComponent<Renderer>().material.color = color;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public class Factory : PlaceholderFactory<Vector3, Color, Enemy>
        {
        }
    }
}
