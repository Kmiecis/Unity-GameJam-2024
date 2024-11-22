using UnityEngine;

namespace Game
{
    public class ParticleEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;

        public void Spawn(Component component)
        {
            var instance = Instantiate(_particles);

            if (instance.TryGetComponent<Transform>(out var targetTransform))
            {
                targetTransform.position = component.transform.position;
            }
        }
    }
}