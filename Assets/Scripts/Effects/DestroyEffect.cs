using UnityEngine;

namespace Game
{
    public class DestroyEffect : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DestroyAny(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}