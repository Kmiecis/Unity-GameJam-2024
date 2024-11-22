using UnityEngine;

namespace Game
{
    public class BillboardComponent : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private Camera Camera
        {
            get => _camera == null ? (_camera = Camera.main) : _camera;
        }

        private void Update()
        {
            var targetRotation = Quaternion.LookRotation(Camera.transform.position - transform.position).eulerAngles;
            var currentRotation = transform.rotation.eulerAngles;

            targetRotation.x = currentRotation.x;
            targetRotation.z = currentRotation.z;

            transform.rotation = Quaternion.Euler(targetRotation);
        }
    }
}