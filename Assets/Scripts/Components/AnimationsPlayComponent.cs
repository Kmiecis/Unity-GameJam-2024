using UnityEngine;

namespace Game
{
    public class AnimationsPlayComponent : MonoBehaviour
    {
        [SerializeField] private Animation[] _animations;
        [SerializeField] private string _name;

        public void PlayAll()
        {
            PlayAll(_name);
        }

        public void PlayAll(string name)
        {
            foreach (var animation in _animations)
            {
                animation.Play(name);
            }
        }

        private void Reset()
        {
            _animations = GetComponents<Animation>();
        }
    }
}