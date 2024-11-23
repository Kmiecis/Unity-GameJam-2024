using UnityEngine;

namespace Game
{
    public class AnimationsPlayComponent : MonoBehaviour
    {
        [SerializeField] private Animation[] _animations;

        public void PlayAll()
        {
            foreach (var animation in _animations)
            {
                animation.Play();
            }
        }

        private void Reset()
        {
            _animations = GetComponents<Animation>();
        }
    }
}