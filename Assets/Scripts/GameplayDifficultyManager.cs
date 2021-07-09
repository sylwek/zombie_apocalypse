using Zenject;

namespace ZombieApocalypse
{
    public class GameplayDifficultyManager : IInitializable
    {
        [Inject]
        private readonly SignalBus _signalBus;

        public GameSettingsInstaller.DifficultySettings SelectedDifficulty { get; private set; }

        public void Initialize()
        {
            _signalBus.Subscribe<DifficultySelected>(OnDifficultySelected);
        }

        private void OnDifficultySelected(DifficultySelected diff)
        {
            SelectedDifficulty = diff.Difficulty;
        }
    }
}
