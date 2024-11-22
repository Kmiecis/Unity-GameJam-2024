using UnityEngine;

namespace Game
{
    public class FirstPersonCrouching : MonoBehaviour
    {
        public float movementMultiplier = 0.33f;
        public float heightMultiplier = 0.66f;

        [Space(10)]
        [SerializeField]
        protected FirstPersonMovement _movement;
        [SerializeField, Tooltip("Transform to scale vertically")]
        protected Transform _modelTransform;
        
        private bool _isCrouching;

        public void TrySetCrouch(bool enabled)
        {
            if (_isCrouching != enabled)
            {
                var movementMul = enabled ? movementMultiplier : 1.0f / movementMultiplier;
                RecalculateMovement(movementMul);

                var heightMul = enabled ? heightMultiplier : 1.0f / heightMultiplier;
                RecalculateModel(heightMul);

                _isCrouching = enabled;
            }
        }

        private void RecalculateMovement(float movementMul)
        {
            _movement.force *= movementMul;
        }

        private void RecalculateModel(float heightMul)
        {
            var scale = _modelTransform.localScale;
            scale.y *= heightMul;
            _modelTransform.localScale = scale;
        }
    }
}
