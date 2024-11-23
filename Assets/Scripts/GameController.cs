using Common.Injection;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [DI_Install]
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _tasksCount;
        [SerializeField] private float _tasksTime = 300.0f;

        [DI_Inject] private TasksController _controller;

        private List<TaskController> _tasks = new List<TaskController>();
        private float _starttime;

        public List<TaskController> CurrentTasks
        {
            get => _tasks;
        }

        public float Timeleft
        {
            get => _tasksTime - (Time.realtimeSinceStartup - _starttime);
        }

        private void PickTasks()
        {
            _tasks = _controller.PickTasks(_tasksCount);
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
            PickTasks();
        }

        private void Update()
        {
            Debug.Log("Timeleft " + Timeleft);
        }

        private void OnDestroy()
        {
            DI_Binder.Unbind(this);
        }
    }
}