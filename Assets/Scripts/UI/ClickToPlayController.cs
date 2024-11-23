using UnityEngine;

namespace Game
{
    public class ClickToPlayController : MonoBehaviour
    {
        [SerializeField] private GameObject _hintObject;

        private void Update()
        {
            _hintObject.SetActive(Cursor.visible);
        }
    }
}