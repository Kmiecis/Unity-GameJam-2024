using UnityEngine;
using Common.Inputs;

namespace Game
{
    public class HoldComponent : MonoBehaviour
    {
        [SerializeField] private MouseDragHandler _handler;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _force = 20.0f;
        [SerializeField] private AnimationCurve _velocityCurve;

        private Vector3 _offset;
        private Vector3 _target;

        private void OnDragBegin(MouseEventData data)
        {
            var rotator = data.camera.transform;
            _offset = Quaternion.Inverse(rotator.rotation) * (transform.position - rotator.position);
            _target = rotator.position + rotator.rotation * _offset;
        }

        private void OnDrag(MouseEventData data)
        {
            var rotator = data.camera.transform;
            _target = rotator.position + rotator.rotation * _offset;

            ApplyForce();
        }

        private void ApplyForce()
        {
            var delta = _target - transform.position;
            _rigidbody.AddForce(delta * _force * _rigidbody.mass, ForceMode.Force);

            var velocityTime = Mathf.Clamp01(delta.magnitude / _offset.magnitude);
            var velocity = _velocityCurve.Evaluate(velocityTime);
            _rigidbody.velocity = ClampVelocity(_rigidbody.velocity, velocity);
        }

        private Vector3 ClampVelocity(Vector3 velocity, float maxVelocity)
        {
            return velocity.normalized * Mathf.Min(velocity.magnitude, maxVelocity);
        }

        private void Awake()
        {
            _handler.OnDragBegan.AddListener(OnDragBegin);
            _handler.OnDragging.AddListener(OnDrag);
        }

        private void OnDestroy()
        {
            _handler.OnDragBegan.RemoveListener(OnDragBegin);
            _handler.OnDragging.RemoveListener(OnDrag);
        }
    }
}