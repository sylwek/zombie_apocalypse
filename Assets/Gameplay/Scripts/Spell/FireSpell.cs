using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class FireSpell : SpellBase
    {
        public class Factory : PlaceholderFactory<Vector3, Quaternion, float, float, int, float, FireSpell>
        {
        }
    }
}
