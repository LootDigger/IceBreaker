using System;
using UnityEngine.UIElements;

namespace UI
{
   public abstract class UIScreen
   {
      protected VisualElement _rootElement;
      public bool IsHidden => _rootElement.style.display == DisplayStyle.None;
      protected EventRegister _eventRegister;
      
      public UIScreen(VisualElement root, bool hideOnInit = true)
      {
         _rootElement = root ?? throw new ArgumentNullException();
         _eventRegister = new EventRegister();

         if (!hideOnInit) {return;}
         HideInstantly();
      }
      
      public virtual void ShowInstantly()
      {
         _rootElement.style.display = DisplayStyle.Flex;
      }

      public virtual void HideInstantly()
      {
         _rootElement.style.display = DisplayStyle.None;
      }
   }
}
