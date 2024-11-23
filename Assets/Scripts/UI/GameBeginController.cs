using Common;
using Common.Coroutines;
using Common.Coroutines.Segments;
using Common.Injection;
using UnityEngine;

namespace Game
{
    public class GameBeginController : MonoBehaviour
    {
        [SerializeField, Folded] private CanvasGroupFadeSegment _fade;

        [DI_Inject] private SoundsManager _sounds;
        [DI_Inject] private VideosManager _videos;

        private void StartScene()
        {
            _fade.Build()
                .With(Yield.ValueTo(0.0f, 1.0f, v => _sounds.Volume = v, Yield.TimeNormalized(_fade.duration)))
                .With(Yield.ValueTo(0.0f, 1.0f, v => _videos.Volume = v, Yield.TimeNormalized(_fade.duration)))
                .Start(this);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Start()
        {
            StartScene();
        }
    }
}