using Common.Injection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class TaskController : TaskAction
    {
        [SerializeField] private UnityEvent<TaskAction> _onComplete = new UnityEvent<TaskAction>();
        [SerializeField] private TaskAction[] _actions;
        [SerializeField] private int _target;

        [DI_Inject] private TasksController _controller;

        private List<TaskAction> _completed = new List<TaskAction>();

        public void CompleteAction(TaskAction action)
        {
            if (_actions.Contains(action))
            {
                _completed.Add(action);

                if (_actions.Length == _target)
                {
                    _onComplete.Invoke(this);
                }
            }
        }

        private void OnTasksControllerInject(TasksController controller)
        {
            controller.AddTask(this);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }
    }
}