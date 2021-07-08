using UnityEngine;
using Zenject;

namespace ZombieApocalypse
{
    public class Enemy : MonoBehaviour
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        [Inject]
        public void Construct(Vector3 position, Color color)
        {
            Position = position;
            GetComponent<Renderer>().material.color = color;
        }

        public class Factory : PlaceholderFactory<Vector3, Color, Enemy>
        {
        }
    }
}
