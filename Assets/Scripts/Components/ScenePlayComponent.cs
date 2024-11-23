using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ScenePlayComponent : MonoBehaviour
    {
        [SerializeField] private AssetReference _scene;

        public void PlayScene()
        {
            PlayAnyScene(_scene.Name);
        }

        public void PlayAnyScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}