using Common.Injection;
using UnityEngine;

namespace Game
{
    [DI_Install]
    public class CursorController : MonoBehaviour
    {
        public KeyCode lockKey = KeyCode.Mouse0;
        public KeyCode releaseKey = KeyCode.Escape;
        public MonoBehaviour[] enables;

        private void SetCursorState(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;

            foreach (var enable in enables)
                enable.enabled = locked;
        }

        private void ReadCursorState()
        {
            if (Input.GetKeyDown(lockKey))
            {
                SetCursorState(true);
            }
            else if (Input.GetKeyDown(releaseKey))
            {
                SetCursorState(false);
            }
        }

        private void OnEnable()
        {
            DI_Binder.Bind(this);
        }

        private void Update()
        {
            ReadCursorState();
        }

        private void OnDisable()
        {
            DI_Binder.Unbind(this);
        }
    }
}
