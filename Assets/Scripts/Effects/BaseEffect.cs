using UnityEngine;

namespace Game
{
    public abstract class BaseEffect : MonoBehaviour
    {
        protected abstract GameObject GetTarget();

        public void Play(Component source)
        {
            var instance = GetTarget();

            if (instance.TryGetComponent<Transform>(out var targetTransform))
            {
                targetTransform.position = source.transform.position;
            }

            if (instance.TryGetComponent<Rigidbody>(out var targetRigidbody))
            {
                if (source is UnityEngine.AI.NavMeshAgent sourceAgent)
                {
                    targetRigidbody.velocity = sourceAgent.velocity;
                }
                else if (source is Rigidbody sourceRigidbody)
                {
                    targetRigidbody.velocity = sourceRigidbody.velocity;
                }
            }

            if (instance.TryGetComponent<TranslateComponent>(out var targetTranslate))
            {
                if (source is UnityEngine.AI.NavMeshAgent sourceAgent)
                {
                    targetTranslate.velocity = sourceAgent.velocity;
                }
                else if (source is Rigidbody sourceRigidbody)
                {
                    targetTranslate.velocity = sourceRigidbody.velocity;
                }
            }
        }
    }
}