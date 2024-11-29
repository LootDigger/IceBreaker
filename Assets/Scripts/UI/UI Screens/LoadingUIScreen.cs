using UnityEngine.UIElements;
using UnityEngine;

namespace UI
{
    public class LoadingUIScreen : UIScreen
    {
        private const float ANIM_DURATION = 0.25f;
        private VisualElement _gameLogo;
        
        public LoadingUIScreen(VisualElement root) : base(root)
        {
            _gameLogo = _rootElement.Q<TextElement>("GAME-LOGO");
        }

        public override void ShowInstantly()
        {
            base.ShowInstantly();
            GameLogoAnimation animation = new(_gameLogo, ANIM_DURATION);
            animation.Play();
        }
    }
}