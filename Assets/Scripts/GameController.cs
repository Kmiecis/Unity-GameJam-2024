using Common.Injection;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [DI_Install]
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _tasksCount;

        [DI_Inject] private TasksController _controller;

        private List<TaskController> _tasks = new List<TaskController>();

        public List<TaskController> CurrentTasks
        {
            get => _tasks;
        }

        private void PickTasks()
        {
            _tasks = _controller.PickTasks(_tasksCount);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Start()
        {
            PickTasks();
        }

        private void OnDestroy()
        {
            DI_Binder.Unbind(this);
        }
    }
}