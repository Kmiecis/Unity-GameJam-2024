using UnityEngine;

namespace Game
{
    public class AirborneDragHandler : MonoBehaviour
    {
        [Header("Variables")]
        public float drag = 0.0f;

        [Header("Components")]
        [SerializeField]
        protected Rigidbody _rigidbody;
        [SerializeField]
        protected GroundedHandler _grounded;

        private float _drag;

        private void OnGroundedChange(bool grounded)
        {
            _rigidbody.drag = grounded ? _drag : drag;
        }

        private void Awake()
        {
            _drag = _rigidbody.drag;
        }

        private void Start()
        {
            _grounded.OnGroundedChange += OnGroundedChange;
        }

        private void OnDestroy()
        {
            _grounded.OnGroundedChange -= OnGroundedChange;
        }
    }
}
