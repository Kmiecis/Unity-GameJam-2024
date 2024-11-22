using UnityEngine;

namespace Game
{
    public class FirstPersonSprinting : MonoBehaviour
    {
        public float movementMultiplier = 2.0f;

        [Space(10)]
        [SerializeField]
        protected FirstPersonMovement _movement;

        private bool _isSprinting;

        public void TrySetSprint(bool enabled)
        {
            if (_isSprinting != enabled)
            {
                var movementMul = enabled ? movementMultiplier : 1.0f / movementMultiplier;
                RecalculateMovement(movementMul);

                _isSprinting = enabled;
            }
        }

        private void RecalculateMovement(float movementMul)
        {
            _movement.force *= movementMul;
        }
    }
}
