using UnityEngine;

namespace Game
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [Header("Variables")]
        public float speed = 360.0f;
        public Vector2 sensitivity = Vector2.one;

        [Header("Components")]
        [SerializeField, Tooltip("Transform to rotate horizontally")]
        protected Transform _modelTransform;
        [SerializeField, Tooltip("Transform to rotate vertically")]
        protected Transform _headTransform;

        private Vector2 _rotation;
        private Quaternion _modelRotation;
        private Quaternion _headRotation;

        public void RotateHorizontally(float dx)
        {
            _rotation.y += dx * sensitivity.x;
            _modelRotation = Quaternion.Euler(0.0f, _rotation.y, 0.0f);
        }

        public void RotateVertically(float dy)
        {
            _rotation.x -= dy * sensitivity.y;
            _rotation.x = Mathf.Clamp(_rotation.x, -90.0f, 90.0f);
            _headRotation = Quaternion.Euler(_rotation.x, 0.0f, 0.0f);
        }

        public void Rotate(float dx, float dy)
        {
            RotateHorizontally(dx);
            RotateVertically(dy);
        }

        private void UpdateRotation(float deltaTime)
        {
            _modelTransform.localRotation = Quaternion.RotateTowards(
                _modelTransform.localRotation, _modelRotation, speed * deltaTime
            );
            _headTransform.localRotation = Quaternion.RotateTowards(
                _headTransform.localRotation, _headRotation, speed * deltaTime
            );
        }

        private void Awake()
        {
            _rotation.y = _modelTransform.localEulerAngles.y;
            _rotation.x = _headTransform.localEulerAngles.x;
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            UpdateRotation(deltaTime);
        }
    }
}
