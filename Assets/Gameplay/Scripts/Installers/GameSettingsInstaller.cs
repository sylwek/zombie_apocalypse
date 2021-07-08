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
        public PlayerSettings Player;

        [Serializable]
        public class EnemySettings
        {
            public EnemySpawner.Settings SpawnSettings;
            public EnemyMover.Settings MovementSettings;
        }

        [Serializable]
        public class PlayerSettings
        {
            public PlayerShootHandler.Settings ShootSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller).IfNotBound();
            Container.BindInstance(Enemy.SpawnSettings).IfNotBound();
            Container.BindInstance(Enemy.MovementSettings).IfNotBound();
            Container.BindInstance(Player.ShootSettings).IfNotBound();
        }
    }
}

