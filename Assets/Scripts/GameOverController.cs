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

        [DI_Inject] private GameController _gameController;

        private void CheckTimeleft(float timeleft, bool completed)
        {
            if (timeleft < 0.0f)
            {
                _fade.Build()
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
            CheckTimeleft(_gameController.Timeleft, _gameController.IsCompleted);
        }
    }
}