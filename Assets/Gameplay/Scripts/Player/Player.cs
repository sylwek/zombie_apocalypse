using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class Player : MonoBehaviour, IDamagable, IInitializable
    {
        public Vector3 Position => Transform.position;

        public Vector3 Forward => Transform.forward;

        public Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }

        [Inject]
        private readonly GameSettingsInstaller.DifficultySettings _settings;

        [Inject]
        private readonly SignalBus _signalBus;

        public int HP { get; private set; }

        private Transform _transform;
        private Transform Transform => _transform != null
            ? _transform
            : (_transform = transform);

        public void Initialize()
        {
            HP = _settings.PlayerInitialHP;
            Debug.Log($"Playing with {_settings.Name} difficulty");
        }

        public bool AcceptDamage(GameObject gameObject) => gameObject.GetComponent<Enemy>() != null;

        public void TakeDamage(int damage)
        {
            HP = Mathf.Max(HP - damage, 0);
            if (HP <= 0)
            {
                _signalBus.Fire<PlayerKilledSignal>();
            }
        }
    }
}
