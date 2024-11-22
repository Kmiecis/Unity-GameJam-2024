using UnityEngine;

namespace Game
{
    public class AudioSourcesPlayComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _sources;

        public void Play(int index)
        {
            _sources[index].Play();
        }

        private void Reset()
        {
            _sources = GetComponents<AudioSource>();
        }
    }
}