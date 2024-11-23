using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UITask : MonoBehaviour
    {
        [SerializeField] private GameObject _doneObject;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        private TaskController _task;
        private bool _completed;

        public void Setup(TaskController task)
        {
            _task = task;

            _doneObject.SetActive(false);
            _descriptionText.text = task.Description;
        }

        private void Update()
        {
            if (_task != null && _completed != _task.IsCompleted)
            {
                _completed = _task.IsCompleted;
                _doneObject.SetActive(_completed);
            }
        }
    }
}