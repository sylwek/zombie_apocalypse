using UnityEngine;
using UnityEngine.UI;

namespace ZombieApocalypse
{
    public class MainMenuControls : MonoBehaviour
    {
        [SerializeField]
        private Button _startGameButton;

        [SerializeField]
        private Button _exitGameButton;

        [SerializeField]
        private SelectDifficultyPopup _selectDifficultyPopup;

        private void Start()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _exitGameButton.onClick.AddListener(ExitGame);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
            _exitGameButton.onClick.RemoveListener(ExitGame);
        }

        private void StartGame()
        {
            _selectDifficultyPopup.Show();
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }
}
