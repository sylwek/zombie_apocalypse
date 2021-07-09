using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class PlayerRotator : ITickable
    {
        [Inject]
        private readonly Player _player;

        [Inject]
        private readonly IPlayerInputState _playerInputState;

        public void Tick()
        {
            if (Time.timeScale <= 0f)
                return;

            var newRotation = Quaternion.LookRotation(_playerInputState.LookDir, Vector3.up);
            _player.Rotation = Quaternion.Euler(
                _player.Rotation.eulerAngles.x,
                newRotation.eulerAngles.y,
                _player.Rotation.eulerAngles.z);
        }
    }
}
