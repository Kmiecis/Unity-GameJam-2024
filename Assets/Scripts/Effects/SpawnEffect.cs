using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class SpawnEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
            Instantiate(_prefab);
        }

        public void Spawn(Component component)
        {
            var instance = Instantiate(_prefab);

            if (instance.TryGetComponent<Transform>(out var targetTransform))
            {
                targetTransform.position = component.transform.position;
            }
            if (instance.TryGetComponent<Rigidbody>(out var targetRigidbody))
            {
                if (component is NavMeshAgent sourceAgent)
                {
                    targetRigidbody.velocity = sourceAgent.velocity;
                }
                else if (component is Rigidbody sourceRigidbody)
                {
                    targetRigidbody.velocity = sourceRigidbody.velocity;
                }
            }
        }
    }
}