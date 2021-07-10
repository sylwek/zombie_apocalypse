using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

        [SerializeField]
        private Button _mainMenuButton;

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerKilledSignal>(OnPlayerKilled);
            _mainMenuButton.onClick.AddListener(MainMenu);
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

        private void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
