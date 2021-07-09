using UnityEngine;

namespace ZombieApocalypse
{
    public interface IDamagable
    {
        int HP { get; }
        bool AcceptDamage(GameObject gameObject);
        void TakeDamage(int damage);
    }
}
