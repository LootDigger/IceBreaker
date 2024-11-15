using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;

namespace UI
{
    public class VisualElementPresenter : MonoBehaviour, IVisualElementPresenter
    {
        [SerializeField] private string _elementName;
        
        private VisualElement _rootElement;
        private VisualElement _targetElement;
        public string Name => _elementName;
        
        public void SetRootElement(VisualElement rootElement)
        {
            _rootElement = rootElement;
            GetTargetElement();
        }
    
        [Button]
        public void ShowElement()
        {
            Debug.Log("ShowElement " + gameObject.name);
            _targetElement.style.display = DisplayStyle.Flex;
        }
        
        [Button]
        public void HideElement()
        {
            Debug.Log("HideElement " + gameObject.name);
            _targetElement.style.display = DisplayStyle.None;
        }
        
        private void GetTargetElement()
        {
            _targetElement = _rootElement.Q<VisualElement>(Name);
        }
    }
}
