using System;
using System.Linq;
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
        public SpellSettings Spell;
        public DifficultySettings[] Difficulty;

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

        [Serializable]
        public class SpellSettings
        {
            public SpellGameplaySettings FireSpell;
            public IceSpellGameplaySettings IceSpell;
            public SpellInputCaster.Settings InputSettings;
            public float SpawnOffsetDistance;
        }

        [Serializable]
        public class SpellGameplaySettings
        {
            public float Lifetime;
            public float MoveSpeed;
            public float CooldownDuration;
            public int Damage;
        }

        [Serializable]
        public class IceSpellGameplaySettings : SpellGameplaySettings
        {
            public float SlowdownDuration;
            public float SlowdownMultiplier;
        }

        [Serializable]
        public class DifficultySettings
        {
            public string Name;
            public int EnemyDamage;
            public float MinEnemySpawnInterval;
            public float MaxEnemySpawnInterval;
            public int PlayerInitialHP;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller).IfNotBound();
            Container.BindInstance(Enemy.SpawnSettings).IfNotBound();
            Container.BindInstance(Enemy.MovementSettings).IfNotBound();
            Container.BindInstance(Player.ShootSettings).IfNotBound();
            Container.BindInstance(Spell).IfNotBound();
            Container.BindInstance(Spell.InputSettings).IfNotBound();
            Container.BindInstance(Difficulty.First()).IfNotBound();
        }
    }
}

