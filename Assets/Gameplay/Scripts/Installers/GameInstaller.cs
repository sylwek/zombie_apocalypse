using System;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Player _player;

        [Serializable]
        public class Settings
        {
            public Enemy EnemyPrefab;
            public Bullet BulletPrefab;
            public SpellBase FireSpellPrefab;
            public SpellBase IceSpellPrefab;
        }

        [Inject]
        private readonly Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle(); // TODO: move to ProjectContext installer

            Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemySpawner>().AsSingle();
            Container.Bind<SpellSpawner>().AsSingle();
            Container.BindInterfacesTo<SpellInputCaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayStatistics>().AsSingle();

            Container.BindFactory<Vector3, Color, int, Enemy, Enemy.Factory>()
                .FromPoolableMemoryPool<Vector3, Color, int, Enemy, EnemyPool>(poolBinder => poolBinder
                    .WithInitialSize(30)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<EnemyInstaller>(_settings.EnemyPrefab)
                    .UnderTransformGroup("Enemies"));

            Container.BindFactory<float, float, int, GameObject, Bullet, Bullet.Factory>()
                .FromPoolableMemoryPool<float, float, int, GameObject, Bullet, BulletPool>(poolBinder => poolBinder
                    .WithInitialSize(30)
                    .FromComponentInNewPrefab(_settings.BulletPrefab)
                    .UnderTransformGroup("Bullets"));

            // TODO: one factory for all spells
            Container.BindFactory<Vector3, Quaternion, float, float, int, float, FireSpell, FireSpell.Factory>()
                .FromPoolableMemoryPool<Vector3, Quaternion, float, float, int, float, FireSpell, FireSpellPool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromComponentInNewPrefab(_settings.FireSpellPrefab)
                    .UnderTransformGroup("Spells"));

            Container.BindFactory<Vector3, Quaternion, float, float, int, float, IceSpell, IceSpell.Factory>()
                .FromPoolableMemoryPool<Vector3, Quaternion, float, float, int, float, IceSpell, IceSpellPool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromComponentInNewPrefab(_settings.IceSpellPrefab)
                    .UnderTransformGroup("Spells"));

            GameSignalsInstaller.Install(Container);

        }

        private class EnemyPool : MonoPoolableMemoryPool<Vector3, Color, int, IMemoryPool, Enemy>
        {}
        private class BulletPool : MonoPoolableMemoryPool<float, float, int, GameObject, IMemoryPool, Bullet>
        {}
        private class FireSpellPool : MonoPoolableMemoryPool<Vector3, Quaternion, float, float, int, float, IMemoryPool, FireSpell>
        {}
        private class IceSpellPool : MonoPoolableMemoryPool<Vector3, Quaternion, float, float, int, float, IMemoryPool, IceSpell>
        {}
    }
}