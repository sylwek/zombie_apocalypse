using TMPro;
using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public sealed class GameplayEndPopup : UIPopup, IInitializable
    {
        [Inject]
        private readonly GameplayStatistics _gameplayStatistics;

        [Inject]
        private readonly SignalBus _signalBus;

        [SerializeField]
        private TextMeshProUGUI _summaryLabel;

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerKilledSignal>(OnPlayerKilled);
        }

        protected override void OnShowed()
        {
            base.OnShowed();

            if (_summaryLabel)
                _summaryLabel.text = $"You were alive for {Mathf.CeilToInt(_gameplayStatistics.GameplayDuration)} seconds";
        }

        private void OnPlayerKilled()
        {
            Show();
        }
    }
}
