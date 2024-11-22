using Common.Mathematics;
using UnityEngine;
using UnityEngine.Video;

namespace Game
{
    [CreateAssetMenu(menuName = nameof(Game) + "/" + nameof(Video), fileName = nameof(Video))]
    public class Video : ScriptableObject
    {
        public VideoClip clip;
        public RenderTexture texture;
        public Range volume = new Range(1.0f, 1.0f);
        public bool loop;
    }
}