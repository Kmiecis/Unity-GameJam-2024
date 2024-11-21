using Common.Extensions;
using UnityEngine;

namespace Game
{
    public class FirstPersonMovement : MonoBehaviour
    {
        [Header("Variables")]
        public float force = 4000.0f;
        public float maxVelocity = 20.0f;

        [Header("Components")]
        [SerializeField]
        protected Rigidbody _rigidbody;
        [SerializeField, Tooltip("Transform to inherit forward and right vector from")]
        protected Transform _modelTransform;
        [SerializeField]
        protected GroundedHandler _grounded;

        private Vector2 _movement;

        public void MoveHorizontally(float dx)
        {
            _movement.x = dx;
        }

        public void MoveVertically(float dy)
        {
            _movement.y = dy;
        }

        public void Move(float dx, float dy)
        {
            MoveHorizontally(dx);
            MoveVertically(dy);
        }

        private void UpdateMovement(float deltaTime)
        {
            //if (_grounded.IsGrounded)
            {
                var normal = _grounded.GroundNormal;

                var right = Vector3.ProjectOnPlane(_modelTransform.right, normal);
                var forward = Vector3.ProjectOnPlane(_modelTransform.forward, normal);

                var movement = right * _movement.x + forward * _movement.y;
                movement = Vector3.ClampMagnitude(movement, 1.0f);

                _rigidbody.AddForce(force * deltaTime * movement, ForceMode.Force);

                _rigidbody.velocity = ClampVelocity(_rigidbody.velocity, maxVelocity);
            }
        }

        private Vector3 ClampVelocity(Vector3 velocity, float maxVelocity)
        {
            var vy = velocity.y;
            var vxz = velocity.XZ();

            vxz = vxz.normalized * Mathf.Min(vxz.magnitude, maxVelocity);

            return vxz.X_Y(vy);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            UpdateMovement(deltaTime);
        }
    }
}
