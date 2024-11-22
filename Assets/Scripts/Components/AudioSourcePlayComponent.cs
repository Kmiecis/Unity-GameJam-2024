using UnityEngine;

namespace Game
{
    public class AudioSourcePlayComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        public void Play()
        {
            _source.Play();
        }

        public void Reset()
        {
            _source = GetComponent<AudioSource>();
        }
    }
}