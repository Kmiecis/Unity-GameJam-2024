using Common.Extensions;
using Common.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using Common.Coroutines;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Video;

namespace Game
{
    public class VideosManager : MonoBehaviour
    {
        private class Sample
        {
            public VideoPlayer player;
            public Video data;
            public float volume;
            public Action onFinish;
            public float muter;
        }

        [SerializeField] private AudioListener _listener;
        [SerializeField] private ComponentPool<VideoPlayer> _sources = new();
        [SerializeField, Range(0.0f, 1.0f)] private float _volume = 1.0f;
        [SerializeField] private float _volumeDistance = 80.0f;
        [SerializeField] private AnimationCurve _volumeCurve = AnimationCurve.EaseInOut(0.0f, 1.0f, 1.0f, 0.0f);

        private List<Sample> _playing = new();
        private bool _muted = false;

        public float Volume
        {
            get => _volume;
            set { _volume = value; }
        }

        public AudioListener Listener
        {
            get => _listener == null ? (_listener = FindObjectOfType<AudioListener>()) : _listener;
        }

        public VideoPlayer PlayVideo(Video data, Action onFinish = null)
        {
            var player = _sources.Borrow();
            var sample = new Sample {
                player = player,
                data = data,
                volume = Random.Range(data.volume.min, data.volume.max),
                onFinish = onFinish
            };

#if UNITY_WEBGL && !UNITY_EDITOR
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, data.clip.name + ".mp4");
#else
            player.clip = data.clip;
            player.time = data.clip.length * Random.Range(data.time.min, data.time.max);
#endif
            player.SetDirectAudioVolume(0, sample.volume * _volume);
            player.isLooping = data.loop;
            player.SetDirectAudioMute(0, _muted);
            player.targetTexture = data.texture;
            player.Play();

            
            _playing.Add(sample);

            return player;
        }

        public void StopVideo(Video data)
        {
            if (_playing.TryFindIndex(s => Equals(s.data, data), out int i))
            {
                var player = _playing[i].player;

                if (player.isPlaying)
                {
                    player.Stop();

                    _playing.RemoveAt(i);

                    _sources.Return(player);
                }
            }
        }

        public void StopVideo(VideoPlayer source)
        {
            if (source.isPlaying && _playing.TryFindIndex(s => Equals(s.player, source), out int i))
            {
                source.Stop();

                _playing.RemoveAt(i);

                _sources.Return(source);
            }
        }

        public void SetVolume(float value)
        {
            foreach (var sample in _playing)
            {
                var volume = Random.Range(sample.data.volume.min, sample.data.volume.max);
                sample.player.SetDirectAudioVolume(0, volume * value);
            }
        }

        public IEnumerator FadeVolume(float target, float duration)
        {
            var timer = Yield.TimeNormalized(duration);

            var current = _volume;
            while (timer.MoveNext())
            {
                var time = timer.Current;
                current = target + (_volume - target) * (1.0f - time);
                SetVolume(current);

                yield return null;
            }
            _volume = current;
        }

        public void SetMuted(bool muted)
        {
            foreach (var sample in _playing)
            {
                sample.player.SetDirectAudioMute(0, muted);
            }
        }

        public void MuteIncoming()
        {
            _muted = true;
        }

        public void UnmuteIncoming()
        {
            _muted = false;

            SetMuted(_muted);
        }

        public void Clear()
        {
            foreach (var sample in _playing)
            {
                var player = sample.player;

                if (player != null)
                {
                    player.Stop();

                    _sources.Return(player);
                }
            }
            _playing.Clear();
        }

        private void ApplyVolume()
        {
            SetVolume(_volume);
        }

        private void UpdateSamples()
        {
            for (int i = 0; i < _playing.Count; ++i)
            {
                var sample = _playing[i];

                if (!sample.player.isPlaying)
                {
                    _playing.RemoveAt(i);
                    i -= 1;

                    _sources.Return(sample.player);

                    if (sample.onFinish != null)
                    {
                        sample.onFinish();
                    }
                }
            }
        }

        private void UpdateVolume()
        {
            var listener = Listener;
            if (listener != null)
            {
                for (int i = 0; i < _playing.Count; ++i)
                {
                    var sample = _playing[i];

                    var distance = Vector3.Distance(sample.player.transform.position, listener.transform.position);
                    var volume = _volumeCurve.Evaluate(Mathf.Clamp01(distance / _volumeDistance)) * _volume;

                    sample.player.SetDirectAudioVolume(0, volume);
                }
            }

        }

        #region Unity methods

        private void Awake()
        {
            ApplyVolume();
        }

        private void Update()
        {
            // Sometimes is not palying on Start...
            // UpdateSamples();
            UpdateVolume();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ApplyVolume();
        }
#endif

        private void OnDestroy()
        {
            Clear();
        }

        #endregion
    }
}