using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class Enemy : MonoBehaviour, IDamagable, IMovementSlowdown, IPoolable<Vector3, Color, int, IMemoryPool>
    {
        [Inject]
        private readonly GameSettingsInstaller.DifficultySettings _difficultySettings;

        [Inject]
        private readonly SignalBus _signalBus;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public int HP { get; private set; }
        public float SlowdownEndTime { get; private set; }
        public float SlowdownMovementMul { get; private set; }


        private IMemoryPool _pool;

        private void OnTriggerEnter(Collider other)
        {
            var damagable = other.GetComponent<IDamagable>();
            if (damagable != null && damagable.AcceptDamage(gameObject))
            {
                damagable.TakeDamage(_difficultySettings.EnemyDamage);
                _pool.Despawn(this);
            }
        }

        // TODO: optimize
        public bool AcceptDamage(GameObject gameObject) =>
            gameObject.GetComponent<Bullet>() != null
            || gameObject.GetComponent<SpellBase>() != null;

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                _signalBus.Fire<EnemyKilledSignal>();
                _pool.Despawn(this);
            }
        }

        public void OnSpawned(Vector3 position, Color color, int hp, IMemoryPool pool)
        {
            _pool = pool;

            HP = hp;
            Position = position;
            GetComponent<Renderer>().material.color = color;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public class Factory : PlaceholderFactory<Vector3, Color, int, Enemy>
        {
        }

        public void Slowdown(float duration, float multiplier)
        {
            SlowdownEndTime = Time.time + duration;
            SlowdownMovementMul = multiplier;
        }
    }
}
