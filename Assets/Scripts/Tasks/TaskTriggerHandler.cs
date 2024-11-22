using Common;
using UnityEngine;

namespace Game
{
    public class TaskTriggerHandler : TriggerHandler
    {
        [SerializeField] private TaskController _controller;

        private void OnTaskTrigger(Collider collider)
        {
            if (collider.TryGetComponent<TaskAction>(out var action))
            {
                _controller.CompleteAction(action);
            }
        }

        private void Awake()
        {
            _onTriggerEnter.AddListener(OnTaskTrigger);
        }
    }
}