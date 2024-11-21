using UnityEngine;

namespace Game
{
    public class CursorController : MonoBehaviour
    {
        public KeyCode lockKey = KeyCode.Mouse0;
        public KeyCode releaseKey = KeyCode.Escape;

        private void SetCursorState(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
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

        private void Update()
        {
            ReadCursorState();
        }
    }
}
