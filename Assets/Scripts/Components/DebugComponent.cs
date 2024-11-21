using UnityEngine;

namespace Game
{
    public class DebugComponent : MonoBehaviour
    {
        [SerializeField] private string _message;

        public void Log()
        {
            Debug.Log(_message);
        }

        public void Warning()
        {
            Debug.LogWarning(_message);
        }

        public void Error()
        {
            Debug.LogError(_message);
        }
    }
}