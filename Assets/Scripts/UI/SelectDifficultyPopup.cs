using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace ZombieApocalypse
{
    public sealed class SelectDifficultyPopup : UIPopup
    {
        [Inject]
        private GameplayDifficultyManager _difficultyManager;

        // TODO: dynamic difficulty settings list
        [SerializeField]
        private Button _easyDifficultyButton;

        [SerializeField]
        private Button _mediumDifficultyButton;

        [SerializeField]
        private Button _hardDifficultyButton;

        private void Start()
        {
            _easyDifficultyButton.onClick.AddListener(SelectEasy);
            _mediumDifficultyButton.onClick.AddListener(SelectMedium);
            _hardDifficultyButton.onClick.AddListener(SelectHard);
        }

        private void OnDestroy()
        {
            _easyDifficultyButton.onClick.RemoveListener(SelectEasy);
            _mediumDifficultyButton.onClick.RemoveListener(SelectMedium);
            _hardDifficultyButton.onClick.RemoveListener(SelectHard);
        }

        private void SelectEasy()
        {
            _difficultyManager.SelectedDifficulty = _difficultyManager.AllDifficulties[0];
            OnSelectedDifficulty();
        }

        private void SelectMedium()
        {
            _difficultyManager.SelectedDifficulty = _difficultyManager.AllDifficulties[1];
            OnSelectedDifficulty();
        }

        private void SelectHard()
        {
            _difficultyManager.SelectedDifficulty = _difficultyManager.AllDifficulties[2];
            OnSelectedDifficulty();
        }

        private void OnSelectedDifficulty()
        {
            Hide();
            SceneManager.LoadScene("Gameplay");
        }
    }
}
