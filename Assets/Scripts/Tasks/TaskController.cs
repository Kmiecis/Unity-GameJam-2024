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
        [SerializeField] private TaskAction[] _completeActions;
        [SerializeField] private int _completeTarget;
        [SerializeField] private string _description;

        [DI_Inject] private TasksController _controller;

        private List<TaskAction> _completed = new List<TaskAction>();

        public string Description
        {
            get => _description;
        }

        public void CompleteAction(TaskAction action)
        {
            if (_completeActions.Contains(action))
            {
                _completed.Add(action);

                if (_completeActions.Length == _completeTarget)
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