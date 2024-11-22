using Common.Injection;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [DI_Install]
    public class TasksController : MonoBehaviour
    {
        private List<TaskController> _tasks = new List<TaskController>();

        public List<TaskController> AllTasks
        {
            get => _tasks;
        }

        public void AddTask(TaskController task)
        {
            _tasks.Add(task);
        }

        public List<TaskController> PickTasks(int count)
        {
            var result = new List<TaskController>(_tasks);
            while (result.Count > count)
            {
                var randomIndex = Random.Range(0, result.Count);
                result.RemoveAt(randomIndex);
            }
            return result;
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void OnDestroy()
        {
            DI_Binder.Unbind(this);
        }
    }
}