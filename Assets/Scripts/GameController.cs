using Common.Injection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [DI_Install]
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _tasksCount = 10;
        [SerializeField] private float _tasksTime = 300.0f;

        [DI_Inject] private TasksController _controller;

        private List<TaskController> _tasks;
        private float _starttime;

        public List<TaskController> CurrentTasks
        {
            get => _tasks ?? (_tasks = _controller.PickTasks(_tasksCount));
        }

        public float Timeleft
        {
            get => _tasksTime - (Time.realtimeSinceStartup - _starttime);
        }

        public bool IsCompleted
        {
            get => _tasks != null && _tasks.All(t => t.IsCompleted);
        }

        private void StartTimer()
        {
            _starttime = Time.realtimeSinceStartup;
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Start()
        {
            StartTimer();
        }

        private void OnDestroy()
        {
            DI_Binder.Unbind(this);
        }
    }
}