using UnityEngine;

namespace ZombieApocalypse
{
    public class Player : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
    }
}
