using Common.Injection;
using UnityEngine;

namespace Game
{
    public abstract class MonoInject<T> : MonoBehaviour
    {
        [SerializeField] private T _value;

        private void Awake()
        {
            DI_Binder.Install(_value);
        }

        private void OnDestroy()
        {
            DI_Binder.Uninstall(_value);
        }
    }
}