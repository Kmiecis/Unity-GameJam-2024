using UnityEngine;

namespace Game
{
    public class RotateAroundComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _rotateOffset;
        [SerializeField] private Vector3 _rotateAxis = Vector3.up;
        [SerializeField] private float _rotateSpeed = 1.0f;

        private void Update()
        {
            transform.RotateAround(_target.position, _rotateAxis, _rotateSpeed * Time.deltaTime);
        }

        private void OnValidate()
        {
            transform.position = _target.position + _rotateOffset;
        }
    }
}