using UnityEngine;

namespace Game
{
    public class TranslateComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 _velocity;

        public Vector3 velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        private void Update()
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
}