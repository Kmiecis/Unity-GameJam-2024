using UnityEngine;
using Common.Inputs;

namespace Game
{
    public class HoldComponent : MonoBehaviour
    {
        [SerializeField] private MouseDragHandler _handler;

        private Vector3 _offset;

        private void OnDragBegin(MouseEventData data)
        {
            _offset = Quaternion.Inverse(data.camera.transform.rotation) * (data.source.position - data.camera.transform.position);
        }

        private void OnDrag(MouseEventData data)
        {
            data.source.position = data.camera.transform.position + data.camera.transform.rotation * _offset;
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