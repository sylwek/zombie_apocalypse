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
        public DifficultySettings Difficulty;

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
            public Player.Settings PlayerDifficultySettings;
        }

        [Serializable]
        public class DifficultySettings
        {
            public string Name;
            public int EnemyDamage;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller).IfNotBound();
            Container.BindInstance(Enemy.SpawnSettings).IfNotBound();
            Container.BindInstance(Enemy.MovementSettings).IfNotBound();
            Container.BindInstance(Player.ShootSettings).IfNotBound();
            Container.BindInstance(Player.PlayerDifficultySettings).IfNotBound();
            Container.BindInstance(Difficulty).IfNotBound();
        }
    }
}

