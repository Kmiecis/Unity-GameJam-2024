using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UITask : MonoBehaviour
    {
        [SerializeField] private GameObject _doneObject;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        public void Setup(TaskController task)
        {
            _doneObject.SetActive(false);
            _descriptionText.text = task.Description;
        }
    }
}