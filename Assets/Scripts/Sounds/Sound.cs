using Common.Mathematics;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = nameof(Game) + "/" + nameof(Sound), fileName = nameof(Sound))]
    public class Sound : ScriptableObject
    {
        public AudioClip clip;
        public Range volume = new Range(1.0f, 1.0f);
        public Range pitch = new Range(1.0f, 1.0f);
        public bool loop;
    }
}