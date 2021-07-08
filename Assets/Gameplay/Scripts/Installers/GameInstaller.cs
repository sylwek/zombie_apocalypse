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
        }

        [Inject]
        private readonly Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance(_player);
            Container.BindInterfacesTo<EnemySpawner>().AsSingle();

            Container.BindFactory<Vector3, Color, Enemy, Enemy.Factory>()
                .FromComponentInNewPrefab(_settings.EnemyPrefab)
                .UnderTransformGroup("Dynamic/Enemies");
        }
    }
}