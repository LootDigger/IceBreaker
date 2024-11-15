using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UI
{
    public class ToolkitButton : MonoBehaviour
    {
        [SerializeField] private string _targetButtonName;
        [SerializeField] private UnityEvent _onButtonClick;
    
        private Button _targetButton;

        private void Start()
        {
            InitButton();
            SubscribeButtonEvent();
        }

        private void OnDestroy() => UnsubscribeButtonEvent();

        private void InitButton()
        {
            var root = FindObjectOfType<UIDocument>().rootVisualElement;
            _targetButton = root.Q<Button>(_targetButtonName);
        }

        private void SubscribeButtonEvent()
        {
            if(_targetButton == null){return;}
            _targetButton.RegisterCallback<ClickEvent>(ButtonClickHandler);
        }
        
        private void UnsubscribeButtonEvent()
        {
            if(_targetButton == null){return;}
            _targetButton.UnregisterCallback<ClickEvent>(ButtonClickHandler);
        }

        private void ButtonClickHandler(ClickEvent clickEvent)
        {
            if (_onButtonClick == null) {return;}
            _onButtonClick.Invoke();
        }
    }
}
