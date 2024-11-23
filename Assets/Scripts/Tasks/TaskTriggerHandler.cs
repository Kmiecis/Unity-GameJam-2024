using Common;
using UnityEngine;

namespace Game
{
    public class TaskTriggerHandler : TriggerHandler
    {
        [SerializeField] private TaskController _controller;

        private void OnTaskTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<TaskAction>(out var action))
            {
                _controller.CompleteAction(action);
            }
        }

        private void OnTaskTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent<TaskAction>(out var action))
            {
                _controller.RevokeAction(action);
            }
        }

        private void Awake()
        {
            _onTriggerEnter.AddListener(OnTaskTriggerEnter);
            _onTriggerExit.AddListener(OnTaskTriggerExit);
        }
    }
}