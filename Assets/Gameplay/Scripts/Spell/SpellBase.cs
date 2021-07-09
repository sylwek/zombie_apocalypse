using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public abstract class SpellBase : Projectile, IPoolable<Vector3, Quaternion, float, float, int, float, IMemoryPool>
    {
        public enum SpellType
        {
            FireStrike,
            IceBlast,
        }

        protected float _cooldownDuration;

        public void OnSpawned(Vector3 position, Quaternion rotation, float speed, float lifeTime, int damage, float cooldown, IMemoryPool pool)
        {
            var trans = transform;
            trans.position = position;
            trans.rotation = rotation;

            _pool = pool;
            _speed = speed;
            _lifeTime = lifeTime;
            _damage = damage;
            _cooldownDuration = cooldown;
            _startTime = Time.realtimeSinceStartup;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        // public class Factory : PlaceholderFactory<UnityEngine.Object, Vector3, Quaternion, float, float, int, SpellBase>
        // {
        // }

        // public class SpellFactory : IFactory<UnityEngine.Object, Vector3, Quaternion, float, float, int, SpellBase>
        // {
        //     readonly DiContainer _container;
        //     readonly IList<UnityEngine.Object> _prefabs;
        //
        //     //public SpellBase Create<T>(Vector3 param2, Quaternion param3, float param4, float param5, int param6)
        //     public SpellBase Create<T>()
        //         where T : SpellBase
        //     {
        //         var prefab = _prefabs.OfType<T>().Single();
        //         return Create(prefab, param2, param3, param4, param5, param6);
        //     }
        //
        //     public SpellBase Create(Object param1, Vector3 param2, Quaternion param3, float param4, float param5, int param6)
        //     {
        //         return _container.InstantiatePrefabForComponent<SpellBase>(param1);
        //     }
        // }
    }
}
