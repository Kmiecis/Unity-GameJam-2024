using Common.Extensions;
using Common.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using Common.Coroutines;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class SoundsManager : MonoBehaviour
    {
        private class Sample
        {
            public AudioSource source;
            public Sound data;
            public float volume;
            public float pitch;
            public Action onFinish;
            public float muter;
        }

        [SerializeField] private AudioListener _listener;
        [SerializeField] private ComponentPool<AudioSource> _sources = new ();
        [SerializeField, Range(0.0f, 1.0f)] private float _volume = 1.0f;
        [SerializeField] private float _volumeDistance = 80.0f;
        [SerializeField] private AnimationCurve _volumeCurve = AnimationCurve.EaseInOut(0.0f, 1.0f, 1.0f, 0.0f);

        private List<Sample> _playing = new ();
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

        public AudioSource PlaySound(Sound data, Action onFinish = null)
        {
            var source = _sources.Borrow();
            var sample = new Sample
            {
                source = source,
                data = data,
                volume = Random.Range(data.volume.min, data.volume.max),
                pitch = Random.Range(data.pitch.min, data.pitch.max),
                onFinish = onFinish
            };

            
            source.clip = data.clip;
            source.volume = sample.volume * _volume;
            source.pitch = sample.pitch;
            source.loop = data.loop;
            source.mute = _muted;
            source.Play();

            _playing.Add(sample);

            return source;
        }

        public void StopSound(Sound data)
        {
            if (_playing.TryFindIndex(s => Equals(s.data, data), out int i))
            {
                var source = _playing[i].source;
                
                if (source.isPlaying)
                {
                    source.Stop();

                    _playing.RemoveAt(i);

                    _sources.Return(source);
                }
            }
        }

        public void StopSound(AudioSource source)
        {
            if (source.isPlaying && _playing.TryFindIndex(s => Equals(s.source, source), out int i))
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
                sample.source.volume = Random.Range(sample.data.volume.min, sample.data.volume.max) * value;
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
                sample.source.mute = muted;
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
                var source = sample.source;

                if (source != null)
                {
                    source.Stop();

                    _sources.Return(source);
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

                if (!sample.source.isPlaying)
                {
                    _playing.RemoveAt(i);
                    i -= 1;

                    _sources.Return(sample.source);

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

                    var distance = Vector3.Distance(sample.source.transform.position, listener.transform.position);
                    var volume = _volumeCurve.Evaluate(Mathf.Clamp01(distance / _volumeDistance)) * sample.volume * _volume;

                    sample.source.volume = volume;
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
            UpdateSamples();
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