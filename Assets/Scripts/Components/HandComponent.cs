using Common.Injection;
using Common.Inputs;
using UnityEngine;

namespace Game
{
    public class HandComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _openObject;
        [SerializeField] private GameObject _closedObject;

        [DI_Inject] private MouseSystem _mouse;

        private bool IsOpen => _openObject.activeSelf;

        private bool IsClosed => _closedObject.activeSelf;

        public void ShowOpen()
        {
            _openObject.SetActive(true);
            _closedObject.SetActive(false);
        }

        public void ShowClosed()
        {
            _closedObject.SetActive(true);
            _openObject.SetActive(false);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Update()
        {
            if (_mouse.Dragger != null)
            {
                if (!IsClosed)
                {
                    ShowClosed();
                }
            }
            else if (_mouse.Hoverer != null)
            {
                if (!IsOpen)
                {
                    ShowOpen();
                }
            }
            else
            {
                if (IsClosed)
                {
                    ShowOpen();
                }
            }
        }
    }
}