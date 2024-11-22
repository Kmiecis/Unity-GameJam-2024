using Common.Coroutines;
using Common.Materials;
using UnityEngine;

namespace Game
{
    public class OutlineComponent : MonoBehaviour
    {
        [SerializeField] private MaterialApplier _applier;
        [SerializeField] private MaterialAlpha _alpha;

        private int _highlights;
        private Coroutine _faderoutine;

        public void FadeIn()
        {
            if (++_highlights == 1 && gameObject.activeInHierarchy)
            {
                _applier.ClearClone();
                _applier.ApplyClone();

                _faderoutine.Stop(this);
                _faderoutine = Yield.ValueTo(0.0f, 1.0f, _alpha.SetValue, Yield.TimeNormalized(0.15f))
                    .Start(this);
            }
        }

        public void FadeOut()
        {
            if (--_highlights == 0 && gameObject.activeInHierarchy)
            {
                _faderoutine.Stop(this);
                _faderoutine = Yield.ValueTo(1.0f, 0.0f, _alpha.SetValue, Yield.TimeNormalized(0.15f))
                    .Then(_applier.ClearClone)
                    .Start(this);
            }
        }
    }
}