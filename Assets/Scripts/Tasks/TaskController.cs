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
        [SerializeField] private TaskAction[] _requiredActions;
        [SerializeField] private int _requiredTarget;
        [SerializeField] private string _description;

        [DI_Inject(nameof(OnTasksControllerInject))] private TasksController _controller;

        private List<TaskAction> _completed = new List<TaskAction>();

        public UnityEvent<TaskAction> OnComplete
        {
            get => _onComplete;
        }

        public string Description
        {
            get => _description;
        }

        public void CompleteAction(TaskAction action)
        {
            if (_requiredActions.Contains(action))
            {
                _completed.Add(action);

                if (_requiredActions.Length == _requiredTarget)
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