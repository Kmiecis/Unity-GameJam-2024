using Common.Injection;
using UnityEngine;

namespace Game
{
    public class SoundShot : MonoBehaviour
    {
        [SerializeField] private Sound _sound;

        [DI_Inject] private SoundsManager _manager;

        public void ShotSound()
        {
            _manager.PlaySound(_sound);

            Destroy(this);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }
    }
}