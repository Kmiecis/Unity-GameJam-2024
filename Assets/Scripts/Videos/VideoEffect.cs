using Common.Injection;
using UnityEngine;

namespace Game
{
    public class VideoEffect : MonoBehaviour
    {
        [SerializeField] private Video _video;
        [SerializeField] private bool _playOnStart;

        [DI_Inject] private VideosManager _manager;

        public void Play()
        {
            var player = _manager.PlayVideo(_video);

            player.transform.position = transform.position;
        }

        public void Stop()
        {
            _manager.StopVideo(_video);
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