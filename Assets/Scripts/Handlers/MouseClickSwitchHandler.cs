using Common.Inputs;
using UnityEngine.Events;
using UnityEngine;

namespace Game
{
    public class MouseClickSwitchHandler : MouseClickHandler
    {
        [SerializeField] protected UnityEvent<MouseEventData> _onUnclicked = new UnityEvent<MouseEventData>();
        [SerializeField] private bool _clicked;

        public UnityEvent<MouseEventData> OnUnclicked
            => _onUnclicked;

        public override void OnClick(MouseEventData data)
        {
            data.source = transform;

            if (!_clicked)
            {
                _onClicked.Invoke(data);
                _clicked = true;
            }
            else
            {
                _onUnclicked.Invoke(data);
                _clicked = false;
            }
        }
    }
}