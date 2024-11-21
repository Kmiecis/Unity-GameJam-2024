using UnityEngine;

namespace Game
{
    public class FirstPersonJumping : MonoBehaviour
    {
        public float impulse = 10.0f;

        [Space(10)]
        [SerializeField]
        protected Rigidbody _rigidbody;
        [SerializeField]
        protected GroundedHandler _grounded;

        public void TryJump()
        {
            if (_grounded.IsGrounded)
            {
                RecalculateJump(impulse * _grounded.GroundNormal);
            }
        }

        private void RecalculateJump(Vector3 force)
        {
            var velocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(velocity.x, 0.0f, velocity.z);

            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
