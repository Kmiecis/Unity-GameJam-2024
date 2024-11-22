using UnityEngine;

namespace Game
{
    public class DestroyOnFinish : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;

        private void Update()
        {
            if (!_particles.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}