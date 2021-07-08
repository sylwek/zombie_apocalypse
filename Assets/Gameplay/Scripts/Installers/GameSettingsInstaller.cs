using System;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    [CreateAssetMenu(menuName = "Zombie Apocalypse/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings GameInstaller;
        public EnemySettings Enemy;

        [Serializable]
        public class EnemySettings
        {
            public EnemySpawner.Settings SpawnSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller).IfNotBound();
            Container.BindInstance(Enemy.SpawnSettings).IfNotBound();
        }
    }
}

