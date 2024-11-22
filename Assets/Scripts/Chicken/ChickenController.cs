using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class ChickenController : MonoBehaviour
    {
        private const float AnimDampTime = 5e-2f;
        private const float AnimValueThreshold = 1e-1f;
        private const int NavMeshSamples = 30;

        private readonly int AnimMovementId = Animator.StringToHash("Movement");
        private readonly int AnimDziobId = Animator.StringToHash("Dziob");

        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _moveAgent;
        [SerializeField] private float _moveRange = 10.0f;
        [SerializeField] private float _idleDuration = 1.0f;
        [SerializeField, Range(0.0f, 1.0f)] private float _idleChance = 0.33f;

        private NavMeshPath _path;
        private float _stopstamp;

        private bool IsIdling
        {
            get => _stopstamp > Time.realtimeSinceStartup;
            set => _stopstamp = Time.realtimeSinceStartup + _idleDuration;
        }

        private bool IsMoving
        {
            get => _moveAgent.hasPath;
        }

        private bool TrySampleNavMeshPosition(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < NavMeshSamples; ++i)
            {
                var randomPosition = center + Random.insideUnitSphere * range;
                if (NavMesh.SamplePosition(randomPosition, out var hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }

            result = default;
            return false;
        }

        private void Start()
        {
            _path = new NavMeshPath();
        }

        private void Update()
        {
            if (!IsIdling && !IsMoving)
            {
                var shouldIdle = Random.Range(0.0f, 1.0f) < _idleChance;
                if (shouldIdle)
                {
                    IsIdling = true;

                    _animator.SetFloat(AnimMovementId, 0.0f);
                    _animator.SetTrigger(AnimDziobId);
                }
            }

            if (!IsIdling && !IsMoving)
            {
                if (TrySampleNavMeshPosition(transform.position, _moveRange, out var position))
                {
                    if (_moveAgent.CalculatePath(position, _path))
                    {
                        _moveAgent.SetPath(_path);
                    }
                }
            }

            if (IsMoving)
            {
                var velocity = _moveAgent.velocity.magnitude;
                if (velocity > AnimValueThreshold)
                {
                    _animator.SetFloat(AnimMovementId, velocity, AnimDampTime, Time.deltaTime);
                }
                else
                {
                    _animator.SetFloat(AnimMovementId, 0.0f);
                }
            }
        }
    }
}