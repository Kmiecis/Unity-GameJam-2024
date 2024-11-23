using Common.Injection;
using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private GameObject _timeObject;
        [SerializeField] private TextMeshProUGUI _timeText;

        [DI_Inject] private GameController _gameController;

        private int _timeleft;

        public void SetTime(float time)
        {
            var timeleft = (int)time;
            if (_timeleft != timeleft)
            {
                var timespan = TimeSpan.FromSeconds(timeleft);
                var text = $"{timespan.Minutes}:{timespan.Seconds}";
                _timeText.text = text;

                _timeleft = timeleft;
            }
        }

        private void Awake()
        {
            DI_Binder.Bind(this);
        }

        private void Start()
        {
            _timeObject.SetActive(true);
        }

        private void Update()
        {
            SetTime(_gameController.Timeleft);
        }
    }
}