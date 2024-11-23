using Common;
using Common.Coroutines;
using Common.Coroutines.Segments;
using Common.Injection;
using Common.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UITasksController : MonoBehaviour
    {
        [SerializeField] private ComponentPool<UITask> _tasksPool = new ComponentPool<UITask>();
        [SerializeField, Folded] private TransformMoveXSegment _showSegment;
        [SerializeField, Folded] private TransformMoveXSegment _hideSegment;

        [DI_Inject] private GameController _gameController;

        private List<UITask> _tasks;
        private bool _shown;

        private void ShowTaskList()
        {
            if (_tasks == null)
            {
                _tasks = new List<UITask>();

                var tasks = _gameController.CurrentTasks;
                foreach (var task in tasks)
                {
                    var uiTask = _tasksPool.Borrow();
                    uiTask.Setup(task);
                    _tasks.Add(uiTask);
                }
            }

            this.StopAllCoroutines();
            _showSegment.Build().Start(this);
            _shown = true;
        }

        private void HideTaskList()
        {
            this.StopAllCoroutines();
            _hideSegment.Build().Start(this);
            _shown = false;
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_shown)
                {
                    HideTaskList();
                }
                else
                {
                    ShowTaskList();
                }
            }
        }
    }
}