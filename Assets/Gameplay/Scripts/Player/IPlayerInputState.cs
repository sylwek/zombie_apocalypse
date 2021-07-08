using UnityEngine;

namespace ZombieApocalypse
{
    public interface IPlayerInputState
    {
        Vector3 LookDir { get; }
        bool IsFiring { get; }
    }
}
