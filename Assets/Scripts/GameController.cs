using Common.Injection;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [DI_Inject] private TasksController _controller;

        private List<TaskController> _tasks = new List<TaskController>();

        private void Awake()
        {
            DI_Binder.Bind(this);
        }
    }
}