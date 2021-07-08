using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class PlayerMouseInputState : ITickable, IPlayerInputState
    {
        [Inject]
        private readonly Camera _camera;

        [Inject]
        private readonly Player _player;

        public Vector3 LookDir { get; private set; }

        public bool IsFiring { get; private set; }

        public void Tick()
        {
            var mousePos = Input.mousePosition;
            var worldMousePos = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (_camera.transform.position - _player.Position).y));
            LookDir = (worldMousePos - _player.Position);

            IsFiring = Input.GetMouseButton(0);
        }
    }
}
