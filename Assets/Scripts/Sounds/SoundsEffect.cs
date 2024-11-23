using UnityEngine;

namespace Game
{
    public class SoundsEffect : MonoBehaviour
    {
        [SerializeField] private SoundEffect[] _effects;

        public void Play()
        {
            foreach (var effect in _effects)
            {
                effect.Play();
            }
        }

        public void Stop()
        {
            foreach (var effect in _effects)
            {
                effect.Stop();
            }
        }
    }
}