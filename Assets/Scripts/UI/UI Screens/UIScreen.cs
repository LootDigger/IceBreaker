using System;
using UnityEngine.UIElements;

namespace UI
{
   public class UIScreen
   {
      private VisualElement _rootElement;
      public bool IsHidden => _rootElement.style.display == DisplayStyle.None;
      
      public UIScreen(VisualElement root)
      {
         _rootElement = root ?? throw new ArgumentNullException();
      }

      public void HideInstantly()
      {
         _rootElement.style.display = DisplayStyle.None;
      }

      public void ShowInstantly()
      {
         _rootElement.style.display = DisplayStyle.Flex;
      }
   }
}
