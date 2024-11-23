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
        [SerializeField] private UnityEvent<TaskAction> _onIncomplete = new UnityEvent<TaskAction>();
        [SerializeField] private UnityEvent<TaskAction> _onProgress = new UnityEvent<TaskAction>();
        [SerializeField] private UnityEvent<TaskAction> _onDeprogress = new UnityEvent<TaskAction>();
        [Space]
        [SerializeField] private TaskAction[] _possibleActions;
        [SerializeField] private int _requiredCount;
        [SerializeField] private string _description;

        [DI_Inject(nameof(OnTasksControllerInject))] private TasksController _controller;

        private List<TaskAction> _completed = new List<TaskAction>();

        public UnityEvent<TaskAction> OnComplete
        {
            get => _onComplete;
        }

        public UnityEvent<TaskAction> OnIncomplete
        {
            get => _onIncomplete;
        }

        public UnityEvent<TaskAction> OnProgress
        {
            get => _onProgress;
        }

        public UnityEvent<TaskAction> OnDeprogres
        {
            get => _onDeprogress;
        }

        public bool IsCompleted
        {
            get => _completed.Count >= _requiredCount;
        }

        public string Description
        {
            get => _description;
        }

        public void CompleteAction(TaskAction action)
        {
            if (_possibleActions.Contains(action) && !_completed.Contains(action))
            {
                _completed.Add(action);

                _onProgress.Invoke(action);

                if (_completed.Count == _requiredCount)
                {
                    _onComplete.Invoke(this);
                }
            }
        }

        public void RevokeAction(TaskAction action)
        {
            // Turned off
            /*
            if (_completed.Contains(action))
            {
                _completed.Remove(action);

                _onDeprogress.Invoke(action);

                if (_completed.Count < _requiredCount)
                {
                    _onIncomplete.Invoke(this);
                }
            }
            */
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