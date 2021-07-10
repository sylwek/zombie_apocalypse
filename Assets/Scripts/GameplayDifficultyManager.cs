using Zenject;

namespace ZombieApocalypse
{
    public class GameplayDifficultyManager : IInitializable
    {
        [Inject]
        private readonly SignalBus _signalBus;

        public GameSettingsInstaller.DifficultySettings SelectedDifficulty { get; set; }

        public GameSettingsInstaller.DifficultySettings[] AllDifficulties { get; set; }

        public void Initialize()
        {
        }
    }
}
