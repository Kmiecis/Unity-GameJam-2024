using UnityEngine;

namespace Game
{
    public class UnparentEffect : BaseEffect
    {
        [SerializeField] private GameObject _target;

        protected override GameObject GetTarget()
        {
            _target.SetActive(true);
            _target.transform.SetParent(null, true);
            return _target;
        }
    }
}