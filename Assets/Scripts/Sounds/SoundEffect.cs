using Common.Injection;
using UnityEngine;

namespace Game
{
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] private Sound _sound;
        [SerializeField] private bool _playOnStart;

        [DI_Inject] private SoundsManager _manager;

        public void Play()
        {
            _manager.PlaySound(_sound);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Start()
        {
            if (_playOnStart)
            {
                Play();
            }
        }
    }
}