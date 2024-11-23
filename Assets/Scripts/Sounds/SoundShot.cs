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
            var sound = _manager.PlaySound(_sound);

            sound.transform.position = transform.position;

            Destroy(this);
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }
    }
}