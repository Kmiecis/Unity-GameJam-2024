using UnityEngine;

namespace Game
{
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Keys")]
        public string mouseAxisXName = "Mouse X";
        public string mouseAxisYName = "Mouse Y";
        public string moveAxisXName = "Horizontal";
        public string moveAxisYName = "Vertical";
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode crouchKey = KeyCode.LeftControl;
        public KeyCode jumpKey = KeyCode.Space;

        [Header("Components")]
        [SerializeField]
        protected FirstPersonCamera _camera;
        [SerializeField]
        protected FirstPersonMovement _movement;
        [SerializeField]
        protected FirstPersonSprinting _sprinting;
        [SerializeField]
        protected FirstPersonCrouching _crouching;
        [SerializeField]
        protected FirstPersonJumping _jumping;

        private Vector2 _mouseAxis;
        private int _enableFrame;

        private bool IsEnabled
        {
            get => _enableFrame <= Time.frameCount;
        }

        private void OnEnable()
        {
            _enableFrame = Time.frameCount + 3;
        }

        private void Update()
        {
            if (IsEnabled)
            {
                _camera.Rotate(
                    Input.GetAxisRaw(mouseAxisXName),
                    Input.GetAxisRaw(mouseAxisYName)
                );
                _movement.Move(
                    Input.GetAxisRaw(moveAxisXName),
                    Input.GetAxisRaw(moveAxisYName)
                );
                _sprinting.TrySetSprint(Input.GetKey(sprintKey));
                _crouching.TrySetCrouch(Input.GetKey(crouchKey));

                if (Input.GetKeyDown(jumpKey))
                {
                    _jumping.TryJump();
                }
            }
        }

        private void OnDisable()
        {
            _enableFrame = int.MaxValue;
        }
    }
}
