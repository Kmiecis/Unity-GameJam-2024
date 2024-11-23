using Common;
using Common.Coroutines;
using Common.Coroutines.Segments;
using Common.Injection;
using UnityEngine;

namespace Game
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField, Folded] private CanvasGroupFadeSegment _fade;
        [SerializeField] private ScenePlayComponent _winScene;
        [SerializeField] private ScenePlayComponent _loseScene;

        [DI_Inject] private GameController _game;
        [DI_Inject] private SoundsManager _sounds;
        [DI_Inject] private VideosManager _videos;

        private void CheckTimeleft(float timeleft, bool completed)
        {
            if (timeleft < 0.0f)
            {
                _fade.Build()
                    .With(Yield.ValueTo(_sounds.Volume, 0.0f, v => _sounds.Volume = v, Yield.TimeNormalized(_fade.duration)))
                    .With(Yield.ValueTo(_videos.Volume, 0.0f, v => _videos.Volume = v, Yield.TimeNormalized(_fade.duration)))
                    .Then(() => LoadScene(completed))
                    .Start(this);
            }
        }

        private void LoadScene(bool completed)
        {
            if (completed)
            {
                _winScene.PlayScene();
            }
            else
            {
                _loseScene.PlayScene();
            }
        }
        
        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Update()
        {
            CheckTimeleft(_game.Timeleft, _game.IsCompleted);
        }
    }
}