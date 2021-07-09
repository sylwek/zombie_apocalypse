using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class GameplayStatistics : IInitializable
    {
        [Inject]
        private readonly SignalBus _signalBus;

        private float _gameplayStartTime;
        public float GameplayDuration => Time.time - _gameplayStartTime;

        public int KilledEnemies { get; private set; }

        public void Initialize()
        {
            _gameplayStartTime = Time.time;
            _signalBus.Subscribe<EnemyKilledSignal>(OnEnemyKilled);
        }

        private void OnEnemyKilled()
        {
            KilledEnemies++;
        }
    }
}
