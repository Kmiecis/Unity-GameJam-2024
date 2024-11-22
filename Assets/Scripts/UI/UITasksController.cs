using Common.Coroutines;
using Common.Injection;
using Common.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UITasksController : MonoBehaviour
    {
        [SerializeField] private ComponentPool<UITask> _tasksPool = new ComponentPool<UITask>();
        [SerializeField] private Common.Coroutines.Segments.TransformMoveXSegment _showSegment;
        [SerializeField] private Common.Coroutines.Segments.TransformMoveXSegment _hideSegment;

        [DI_Inject] private GameController _gameController;

        private List<UITask> _tasks = new List<UITask>();

        private void ShowTaskList()
        {
            var tasks = _gameController.CurrentTasks;
            foreach (var task in tasks)
            {
                var uiTask = _tasksPool.Borrow();
                uiTask.Setup(task);
                _tasks.Add(uiTask);
            }

            _showSegment.Build().Start(this);
        }

        private void HideTaskList()
        {
            foreach (var task in _tasks)
            {
                _tasksPool.Return(task);
            }
            _tasks.Clear();

            _hideSegment.Build().Start(this);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_tasks.Count > 0)
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