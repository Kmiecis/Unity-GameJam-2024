using UnityEngine;

namespace Game
{
    public class AnimationPlayComponent : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private string _name;

        public void Play()
        {
            Play(_name);
        }

        public void Play(string name)
        {
            _animation.Play(name);
        }

        private void Reset()
        {
            _animation = GetComponent<Animation>();
        }
    }
}