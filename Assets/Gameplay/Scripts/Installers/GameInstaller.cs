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
        }

        [Inject]
        private readonly Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle(); // TODO: move to ProjectContext installer

            Container.Bind<Player>().FromInstance(_player);
            Container.BindInterfacesTo<EnemySpawner>().AsSingle();

            // Container.BindFactory<Vector3, Color, Enemy, Enemy.Factory>()
            //     .FromComponentInNewPrefab(_settings.EnemyPrefab)
            //     .UnderTransformGroup("Enemies");

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
        }

        class EnemyPool : MonoPoolableMemoryPool<Vector3, Color, int, IMemoryPool, Enemy>
        {
        }

        class BulletPool : MonoPoolableMemoryPool<float, float, int, GameObject, IMemoryPool, Bullet>
        {
        }
    }
}