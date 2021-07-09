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

        public Vector3 Position => transform.position;

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        [Inject]
        private readonly Settings _settings;

        public int HP { get; private set; }

        public bool AcceptDamage(GameObject gameObject) => gameObject.GetComponent<Enemy>() != null;

        public void TakeDamage(int damage)
        {
            HP -= Mathf.Max(HP - damage, 0);
            if (HP <= 0)
                 Debug.Log("Player died.");
        }

        public void Initialize()
        {
            HP = _settings.InitialHP;
        }
    }
}
