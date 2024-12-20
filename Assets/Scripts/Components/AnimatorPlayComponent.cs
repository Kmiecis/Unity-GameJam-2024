using UnityEngine;

namespace Game
{
    public class AnimatorPlayComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _state;

        public void Play()
        {
            PlayAny(_state);
        }

        public void PlayAny(string state)
        {
            _animator.Play(state);
        }

        private void Reset()
        {
            _animator = GetComponent<Animator>();
        }
    }
}