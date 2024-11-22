using UnityEngine;

namespace Game
{
    public class AnimatorTriggerComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _trigger;

        public void SetTrigger()
        {
            SetTrigger(_trigger);
        }

        public void SetTrigger(string trigger)
        {
            _animator.SetTrigger(trigger);
        }
    }
}