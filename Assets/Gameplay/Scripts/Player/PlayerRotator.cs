using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class PlayerRotator : ITickable
    {
        [Inject]
        private readonly Player _player;

        [Inject]
        private readonly IPlayerRotateState _playerRotateState;

        public void Tick()
        {
            var newRotation = Quaternion.LookRotation(_playerRotateState.LookDir, Vector3.up);
            _player.Rotation = Quaternion.Euler(
                _player.Rotation.eulerAngles.x,
                newRotation.eulerAngles.y,
                _player.Rotation.eulerAngles.z);
        }
    }
}
