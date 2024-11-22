using UnityEngine;

namespace Game
{
    public class SpawnEffect : BaseEffect
    {
        [SerializeField] private GameObject _prefab;

        protected override GameObject GetTarget()
        {
            return Instantiate(_prefab);
        }
    }
}