using System;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class Player : MonoBehaviour, IDamagable, IInitializable
    {
        [Serializable]
        public class Settings
        {
            public int InitialHP;
        }

        public Vector3 Position => Transform.position;

        public Vector3 Forward => Transform.forward;

        public Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }

        [Inject]
        private readonly Settings _settings;

        public int HP { get; private set; }

        private Transform _transform;
        private Transform Transform => _transform != null
            ? _transform
            : (_transform = transform);

        public void Initialize()
        {
            HP = _settings.InitialHP;
        }

        public bool AcceptDamage(GameObject gameObject) => gameObject.GetComponent<Enemy>() != null;

        public void TakeDamage(int damage)
        {
            HP -= Mathf.Max(HP - damage, 0);
            if (HP <= 0)
                 Debug.Log("Player died.");
        }
    }
}
